#region References
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Server.Network;
#endregion

namespace Server.Items
{
	public delegate void OnItemConsumed(Item item, int amount);
	public delegate int CheckItemGroup(Item a, Item b);
	public delegate bool ResValidator(Item item);
	public delegate void ContainerSnoopHandler(Container cont, Mobile from);

	public class Container : Item
	{
		#region Enhanced Client Support
		public virtual void ValidateGridLocation(Item item)
		{
			var pos = item.GridLocation;

			if (!IsFreePosition(pos))
			{
				item.GridLocation = GetNewPosition(pos);
			}
		}

		public virtual bool IsFreePosition(byte pos)
		{
			if (pos < 0 || pos > 0x7C)
			{
				return false;
			}

			return Items.All(i => i.GridLocation != pos);
		}

		public virtual byte GetNewPosition(byte current)
		{
			var index = 0;
			var next = (byte)(current + 1);

			while (++index < 0x7D)
			{
				if (!IsFreePosition(next))
				{
					if (next == 0x7C)
					{
						next = 0;

						if (IsFreePosition(next))
						{
							return next;
						}
					}
				}
				else
				{
					return next;
				}

				next++;
			}

			return 0;
		}

		public virtual void ValidatePositions()
		{
			foreach (var item in Items)
			{
				if (IsFreePosition(item.GridLocation))
				{
					item.GridLocation = GetNewPosition(item.GridLocation);
				}
			}
		}
		#endregion

		private static ContainerSnoopHandler m_SnoopHandler;

		public static ContainerSnoopHandler SnoopHandler { get => m_SnoopHandler; set => m_SnoopHandler = value; }

		private ContainerData m_ContainerData;

		private int m_DropSound;
		private int m_GumpID;
		private int m_MaxItems;

		private int m_TotalItems;
		private int m_TotalWeight;
		private int m_TotalGold;

		private bool m_LiftOverride;

		internal List<Item> m_Items;

		public ContainerData ContainerData
		{
			get
			{
				if (m_ContainerData == null)
				{
					UpdateContainerData();
				}

				return m_ContainerData;
			}
			set => m_ContainerData = value;
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public override int ItemID
		{
			get => base.ItemID;
			set
			{
				var oldID = ItemID;

				base.ItemID = value;

				if (ItemID != oldID)
				{
					UpdateContainerData();
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int GumpID { get => m_GumpID == -1 ? DefaultGumpID : m_GumpID; set => m_GumpID = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public int DropSound { get => m_DropSound == -1 ? DefaultDropSound : m_DropSound; set => m_DropSound = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public int MaxItems
		{
			get => m_MaxItems == -1 ? DefaultMaxItems : m_MaxItems;
			set
			{
				m_MaxItems = value;
				InvalidateProperties();
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual int MaxWeight
		{
			get
			{
				if (Parent is Container && ((Container)Parent).MaxWeight == 0)
				{
					return 0;
				}
				else
				{
					return DefaultMaxWeight;
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool LiftOverride { get => m_LiftOverride; set => m_LiftOverride = value; }

		public virtual void UpdateContainerData()
		{
			ContainerData = ContainerData.GetData(ItemID);
		}

		public virtual Rectangle2D Bounds => ContainerData.Bounds;
		public virtual int DefaultGumpID => ContainerData.GumpID;
		public virtual int DefaultDropSound => ContainerData.DropSound;

		public virtual int DefaultMaxItems => GlobalMaxItems;
		public virtual int DefaultMaxWeight => GlobalMaxWeight;

		public virtual bool IsDecoContainer => !Movable && !IsLockedDown && !IsSecure && Parent == null && !m_LiftOverride;

		public virtual int GetDroppedSound(Item item)
		{
			var dropSound = item.GetDropSound();

			return dropSound != -1 ? dropSound : DropSound;
		}

		public override void OnSnoop(Mobile from)
		{
			if (m_SnoopHandler != null)
			{
				m_SnoopHandler(this, from);
			}
		}

		public override bool CheckLift(Mobile from, Item item, ref LRReason reject)
		{
			if (!from.IsStaff() && IsDecoContainer)
			{
				reject = LRReason.CannotLift;
				return false;
			}

			return base.CheckLift(from, item, ref reject);
		}

		public override bool CheckItemUse(Mobile from, Item item)
		{
			if (item != this && from.AccessLevel < AccessLevel.GameMaster && IsDecoContainer)
			{
				from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
				return false;
			}

			return base.CheckItemUse(from, item);
		}

		public bool CheckHold(Mobile m, Item item, bool message)
		{
			return CheckHold(m, item, message, true, 0, 0);
		}

		public bool CheckHold(Mobile m, Item item, bool message, bool checkItems)
		{
			return CheckHold(m, item, message, checkItems, 0, 0);
		}

		public virtual bool CheckHold(Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight)
		{
			if (!m.IsStaff())
			{
				if (this.Public)
				{
					return true;
				}
				if (IsDecoContainer)
				{
					if (message)
					{
						SendCantStoreMessage(m, item);
					}

					return false;
				}

				var maxItems = MaxItems;

				if (checkItems && maxItems != 0 &&
					(TotalItems + plusItems + item.TotalItems + (item.IsVirtualItem ? 0 : 1)) > maxItems)
				{
					if (message)
					{
						SendFullItemsMessage(m, item);
					}

					return false;
				}
				else
				{
					var maxWeight = MaxWeight;

					if (maxWeight != 0 && (TotalWeight + plusWeight + item.TotalWeight + item.PileWeight) > maxWeight)
					{
						if (message)
						{
							SendFullWeightMessage(m, item);
						}

						return false;
					}
				}
			}

			var parent = Parent;

			while (parent != null)
			{
				if (parent is Container)
				{
					return ((Container)parent).CheckHold(m, item, message, checkItems, plusItems, plusWeight);
				}
				else if (parent is Item)
				{
					parent = ((Item)parent).Parent;
				}
				else
				{
					break;
				}
			}

			return true;
		}

		public virtual bool CheckStack(Mobile from, Item item)
		{
			if (item == null || item.Deleted || !item.Stackable)
			{
				return false;
			}

			foreach (var i in Items)
			{
				if (i.WillStack(from, item))
				{
					return true;
				}
			}

			return false;
		}

		public virtual void SendFullItemsMessage(Mobile to, Item item)
		{
			to.SendLocalizedMessage(1080017); // That container cannot hold more items.
		}

		public virtual void SendFullWeightMessage(Mobile to, Item item)
		{
			to.SendLocalizedMessage(1080016); // That container cannot hold more weight.
		}

		public virtual void SendCantStoreMessage(Mobile to, Item item)
		{
			to.SendLocalizedMessage(500176); // That is not your container, you can't store things here.
		}

		public virtual bool OnDragDropInto(Mobile from, Item item, Point3D p)
		{
			if (!CheckHold(from, item, true, true))
			{
				return false;
			}

			item.Location = new Point3D(p.m_X, p.m_Y, 0);
			AddItem(item);

			from.SendSound(GetDroppedSound(item), GetWorldLocation());

			return true;
		}

		private class GroupComparer : IComparer
		{
			private readonly CheckItemGroup m_Grouper;

			public GroupComparer(CheckItemGroup grouper)
			{
				m_Grouper = grouper;
			}

			public int Compare(object x, object y)
			{
				var a = (Item)x;
				var b = (Item)y;

				return m_Grouper(a, b);
			}
		}

		#region Consume[...]
		public bool ConsumeTotalGrouped(Type type, int amount, bool recurse, OnItemConsumed callback, CheckItemGroup grouper)
		{
			return ConsumeTotalGrouped(type, amount, recurse, null, callback, grouper);
		}

		public bool ConsumeTotalGrouped(Type type, int amount, bool recurse, ResValidator validator, OnItemConsumed callback, CheckItemGroup grouper)
		{
			if (grouper == null)
			{
				throw new ArgumentNullException();
			}

			var typedItems = FindItemsByType(type, recurse);

			var groups = new List<List<Item>>();
			var idx = 0;

			while (idx < typedItems.Length)
			{
				var a = typedItems[idx++];

				if (validator != null && !validator(a))
					continue;

				var group = new List<Item>
				{
					a
				};

				while (idx < typedItems.Length)
				{
					var b = typedItems[idx];

					if (validator != null && !validator(b))
						continue;

					var v = grouper(a, b);

					if (v == 0)
					{
						group.Add(b);
					}
					else
					{
						break;
					}

					++idx;
				}

				groups.Add(group);
			}

			var items = new Item[groups.Count][];
			var totals = new int[groups.Count];

			var hasEnough = false;

			for (var i = 0; i < groups.Count; ++i)
			{
				items[i] = groups[i].ToArray();
				//items[i] = (Item[])(((ArrayList)groups[i]).ToArray( typeof( Item ) ));

				for (var j = 0; j < items[i].Length; ++j)
				{
					totals[i] += items[i][j].Amount;
				}

				if (totals[i] >= amount)
				{
					hasEnough = true;
				}
			}

			if (!hasEnough)
			{
				return false;
			}

			for (var i = 0; i < items.Length; ++i)
			{
				if (totals[i] >= amount)
				{
					var need = amount;

					for (var j = 0; j < items[i].Length; ++j)
					{
						var item = items[i][j];

						var theirAmount = item.Amount;

						if (theirAmount < need)
						{
							if (callback != null)
							{
								callback(item, theirAmount);
							}

							item.Delete();
							need -= theirAmount;
						}
						else
						{
							if (callback != null)
							{
								callback(item, need);
							}

							item.Consume(need);
							break;
						}
					}

					break;
				}
			}

			return true;
		}

		public int ConsumeTotalGrouped(
			Type[] types, int[] amounts, bool recurse, OnItemConsumed callback, CheckItemGroup grouper)
		{
			return ConsumeTotalGrouped(types, amounts, recurse, null, callback, grouper);
		}

		public int ConsumeTotalGrouped(
			Type[] types, int[] amounts, bool recurse, ResValidator validator, OnItemConsumed callback, CheckItemGroup grouper)
		{
			if (types.Length != amounts.Length)
			{
				throw new ArgumentException();
			}
			else if (grouper == null)
			{
				throw new ArgumentNullException();
			}

			var items = new Item[types.Length][][];
			var totals = new int[types.Length][];

			for (var i = 0; i < types.Length; ++i)
			{
				var typedItems = FindItemsByType(types[i], recurse);

				var groups = new List<List<Item>>();
				var idx = 0;

				while (idx < typedItems.Length)
				{
					var a = typedItems[idx++];

					if (validator != null && !validator(a))
						continue;

					var group = new List<Item>
					{
						a
					};

					while (idx < typedItems.Length)
					{
						var b = typedItems[idx];

						if (validator != null && !validator(b))
							continue;

						var v = grouper(a, b);

						if (v == 0)
						{
							group.Add(b);
						}
						else
						{
							break;
						}

						++idx;
					}

					groups.Add(group);
				}

				items[i] = new Item[groups.Count][];
				totals[i] = new int[groups.Count];

				var hasEnough = false;

				for (var j = 0; j < groups.Count; ++j)
				{
					items[i][j] = groups[j].ToArray();
					//items[i][j] = (Item[])(((ArrayList)groups[j]).ToArray( typeof( Item ) ));

					for (var k = 0; k < items[i][j].Length; ++k)
					{
						totals[i][j] += items[i][j][k].Amount;
					}

					if (totals[i][j] >= amounts[i])
					{
						hasEnough = true;
					}
				}

				if (!hasEnough)
				{
					return i;
				}
			}

			for (var i = 0; i < items.Length; ++i)
			{
				for (var j = 0; j < items[i].Length; ++j)
				{
					if (totals[i][j] >= amounts[i])
					{
						var need = amounts[i];

						for (var k = 0; k < items[i][j].Length; ++k)
						{
							var item = items[i][j][k];

							var theirAmount = item.Amount;

							if (theirAmount < need)
							{
								if (callback != null)
								{
									callback(item, theirAmount);
								}

								item.Delete();
								need -= theirAmount;
							}
							else
							{
								if (callback != null)
								{
									callback(item, need);
								}

								item.Consume(need);
								break;
							}
						}

						break;
					}
				}
			}

			return -1;
		}

		public int ConsumeTotalGrouped(
			Type[][] types, int[] amounts, bool recurse, OnItemConsumed callback, CheckItemGroup grouper)
		{
			return ConsumeTotalGrouped(types, amounts, recurse, null, callback, grouper);
		}

		public int ConsumeTotalGrouped(
			Type[][] types, int[] amounts, bool recurse, ResValidator validator, OnItemConsumed callback, CheckItemGroup grouper)
		{
			if (types.Length != amounts.Length)
			{
				throw new ArgumentException();
			}
			else if (grouper == null)
			{
				throw new ArgumentNullException();
			}

			var items = new Item[types.Length][][];
			var totals = new int[types.Length][];

			for (var i = 0; i < types.Length; ++i)
			{
				var typedItems = FindItemsByType(types[i], recurse);

				var groups = new List<List<Item>>();
				var idx = 0;

				while (idx < typedItems.Length)
				{
					var a = typedItems[idx++];

					if (validator != null && !validator(a))
						continue;

					var group = new List<Item>
					{
						a
					};

					while (idx < typedItems.Length)
					{
						var b = typedItems[idx];

						if (validator != null && !validator(b))
							continue;

						var v = grouper(a, b);

						if (v == 0)
						{
							group.Add(b);
						}
						else
						{
							break;
						}

						++idx;
					}

					groups.Add(group);
				}

				items[i] = new Item[groups.Count][];
				totals[i] = new int[groups.Count];

				var hasEnough = false;

				for (var j = 0; j < groups.Count; ++j)
				{
					items[i][j] = groups[j].ToArray();

					for (var k = 0; k < items[i][j].Length; ++k)
					{
						totals[i][j] += items[i][j][k].Amount;
					}

					if (totals[i][j] >= amounts[i])
					{
						hasEnough = true;
					}
				}

				if (!hasEnough)
				{
					return i;
				}
			}

			for (var i = 0; i < items.Length; ++i)
			{
				for (var j = 0; j < items[i].Length; ++j)
				{
					if (totals[i][j] >= amounts[i])
					{
						var need = amounts[i];

						for (var k = 0; k < items[i][j].Length; ++k)
						{
							var item = items[i][j][k];

							var theirAmount = item.Amount;

							if (theirAmount < need)
							{
								if (callback != null)
								{
									callback(item, theirAmount);
								}

								item.Delete();
								need -= theirAmount;
							}
							else
							{
								if (callback != null)
								{
									callback(item, need);
								}

								item.Consume(need);
								break;
							}
						}

						break;
					}
				}
			}

			return -1;
		}

		public int ConsumeTotal(Type[][] types, int[] amounts)
		{
			return ConsumeTotal(types, amounts, true, null);
		}

		public int ConsumeTotal(Type[][] types, int[] amounts, bool recurse)
		{
			return ConsumeTotal(types, amounts, recurse, null);
		}

		public int ConsumeTotal(Type[][] types, int[] amounts, bool recurse, OnItemConsumed callback)
		{
			if (types.Length != amounts.Length)
			{
				throw new ArgumentException();
			}

			var items = new Item[types.Length][];
			var totals = new int[types.Length];

			for (var i = 0; i < types.Length; ++i)
			{
				items[i] = FindItemsByType(types[i], recurse);

				for (var j = 0; j < items[i].Length; ++j)
				{
					totals[i] += items[i][j].Amount;
				}

				if (totals[i] < amounts[i])
				{
					return i;
				}
			}

			for (var i = 0; i < types.Length; ++i)
			{
				var need = amounts[i];

				for (var j = 0; j < items[i].Length; ++j)
				{
					var item = items[i][j];

					var theirAmount = item.Amount;

					if (theirAmount < need)
					{
						if (callback != null)
						{
							callback(item, theirAmount);
						}

						item.Delete();
						need -= theirAmount;
					}
					else
					{
						if (callback != null)
						{
							callback(item, need);
						}

						item.Consume(need);
						break;
					}
				}
			}

			return -1;
		}

		public int ConsumeTotal(Type[] types, int[] amounts)
		{
			return ConsumeTotal(types, amounts, true, null);
		}

		public int ConsumeTotal(Type[] types, int[] amounts, bool recurse)
		{
			return ConsumeTotal(types, amounts, recurse, null);
		}

		public int ConsumeTotal(Type[] types, int[] amounts, bool recurse, OnItemConsumed callback)
		{
			if (types.Length != amounts.Length)
			{
				throw new ArgumentException();
			}

			var items = new Item[types.Length][];
			var totals = new int[types.Length];

			for (var i = 0; i < types.Length; ++i)
			{
				items[i] = FindItemsByType(types[i], recurse);

				for (var j = 0; j < items[i].Length; ++j)
				{
					totals[i] += items[i][j].Amount;
				}

				if (totals[i] < amounts[i])
				{
					return i;
				}
			}

			for (var i = 0; i < types.Length; ++i)
			{
				var need = amounts[i];

				for (var j = 0; j < items[i].Length; ++j)
				{
					var item = items[i][j];

					var theirAmount = item.Amount;

					if (theirAmount < need)
					{
						if (callback != null)
						{
							callback(item, theirAmount);
						}

						item.Delete();
						need -= theirAmount;
					}
					else
					{
						if (callback != null)
						{
							callback(item, need);
						}

						item.Consume(need);
						break;
					}
				}
			}

			return -1;
		}

		public bool ConsumeTotal(Type type, int amount)
		{
			return ConsumeTotal(type, amount, true, null);
		}

		public bool ConsumeTotal(Type type, int amount, bool recurse)
		{
			return ConsumeTotal(type, amount, recurse, null);
		}

		public bool ConsumeTotal(Type type, int amount, bool recurse, OnItemConsumed callback)
		{
			var items = FindItemsByType(type, recurse);

			// First pass, compute total
			var total = 0;

			for (var i = 0; i < items.Length; ++i)
			{
				total += items[i].Amount;
			}

			if (total >= amount)
			{
				// We've enough, so consume it

				var need = amount;

				for (var i = 0; i < items.Length; ++i)
				{
					var item = items[i];

					var theirAmount = item.Amount;

					if (theirAmount < need)
					{
						if (callback != null)
						{
							callback(item, theirAmount);
						}

						item.Delete();
						need -= theirAmount;
					}
					else
					{
						if (callback != null)
						{
							callback(item, need);
						}

						item.Consume(need);

						return true;
					}
				}
			}

			return false;
		}

		public int ConsumeUpTo(Type type, int amount)
		{
			return ConsumeUpTo(type, amount, true);
		}

		public int ConsumeUpTo(Type type, int amount, bool recurse)
		{
			var consumed = 0;

			var toDelete = new Queue<Item>();

			RecurseConsumeUpTo(this, type, amount, recurse, ref consumed, toDelete);

			while (toDelete.Count > 0)
			{
				toDelete.Dequeue().Delete();
			}

			return consumed;
		}

		private static void RecurseConsumeUpTo(
			Item current, Type type, int amount, bool recurse, ref int consumed, Queue<Item> toDelete)
		{
			if (current != null && current.Items.Count > 0)
			{
				var list = current.Items;

				for (var i = 0; i < list.Count; ++i)
				{
					var item = list[i];

					if (type.IsAssignableFrom(item.GetType()))
					{
						var need = amount - consumed;
						var theirAmount = item.Amount;

						if (theirAmount <= need)
						{
							toDelete.Enqueue(item);
							consumed += theirAmount;
						}
						else
						{
							item.Amount -= need;
							consumed += need;

							return;
						}
					}
					else if (recurse && item is Container)
					{
						RecurseConsumeUpTo(item, type, amount, recurse, ref consumed, toDelete);
					}
				}
			}
		}
		#endregion

		#region Get[BestGroup]Amount
		public int GetBestGroupAmount(Type type, bool recurse, CheckItemGroup grouper)
		{
			if (grouper == null)
			{
				throw new ArgumentNullException();
			}

			var best = 0;

			var typedItems = FindItemsByType(type, recurse);

			var groups = new List<List<Item>>();
			var idx = 0;

			while (idx < typedItems.Length)
			{
				var a = typedItems[idx++];
				var group = new List<Item>
				{
					a
				};

				while (idx < typedItems.Length)
				{
					var b = typedItems[idx];
					var v = grouper(a, b);

					if (v == 0)
					{
						group.Add(b);
					}
					else
					{
						break;
					}

					++idx;
				}

				groups.Add(group);
			}

			for (var i = 0; i < groups.Count; ++i)
			{
				var items = groups[i].ToArray();

				//Item[] items = (Item[])(((ArrayList)groups[i]).ToArray( typeof( Item ) ));
				var total = 0;

				for (var j = 0; j < items.Length; ++j)
				{
					total += items[j].Amount;
				}

				if (total >= best)
				{
					best = total;
				}
			}

			return best;
		}

		public int GetBestGroupAmount(Type[] types, bool recurse, CheckItemGroup grouper)
		{
			if (grouper == null)
			{
				throw new ArgumentNullException();
			}

			var best = 0;

			var typedItems = FindItemsByType(types, recurse);

			var groups = new List<List<Item>>();
			var idx = 0;

			while (idx < typedItems.Length)
			{
				var a = typedItems[idx++];
				var group = new List<Item>
				{
					a
				};

				while (idx < typedItems.Length)
				{
					var b = typedItems[idx];
					var v = grouper(a, b);

					if (v == 0)
					{
						group.Add(b);
					}
					else
					{
						break;
					}

					++idx;
				}

				groups.Add(group);
			}

			for (var j = 0; j < groups.Count; ++j)
			{
				var items = groups[j].ToArray();
				//Item[] items = (Item[])(((ArrayList)groups[j]).ToArray( typeof( Item ) ));
				var total = 0;

				for (var k = 0; k < items.Length; ++k)
				{
					total += items[k].Amount;
				}

				if (total >= best)
				{
					best = total;
				}
			}

			return best;
		}

		public int GetBestGroupAmount(Type[][] types, bool recurse, CheckItemGroup grouper)
		{
			if (grouper == null)
			{
				throw new ArgumentNullException();
			}

			var best = 0;

			for (var i = 0; i < types.Length; ++i)
			{
				var typedItems = FindItemsByType(types[i], recurse);

				var groups = new List<List<Item>>();
				var idx = 0;

				while (idx < typedItems.Length)
				{
					var a = typedItems[idx++];
					var group = new List<Item>
					{
						a
					};

					while (idx < typedItems.Length)
					{
						var b = typedItems[idx];
						var v = grouper(a, b);

						if (v == 0)
						{
							group.Add(b);
						}
						else
						{
							break;
						}

						++idx;
					}

					groups.Add(group);
				}

				for (var j = 0; j < groups.Count; ++j)
				{
					var items = groups[j].ToArray();
					//Item[] items = (Item[])(((ArrayList)groups[j]).ToArray( typeof( Item ) ));
					var total = 0;

					for (var k = 0; k < items.Length; ++k)
					{
						total += items[k].Amount;
					}

					if (total >= best)
					{
						best = total;
					}
				}
			}

			return best;
		}

		public int GetAmount(Type type)
		{
			return GetAmount(type, true);
		}

		public int GetAmount(Type type, bool recurse)
		{
			var items = FindItemsByType(type, recurse);

			var amount = 0;

			for (var i = 0; i < items.Length; ++i)
			{
				amount += items[i].Amount;
			}

			return amount;
		}

		public int GetAmount(Type[] types)
		{
			return GetAmount(types, true);
		}

		public int GetAmount(Type[] types, bool recurse)
		{
			var items = FindItemsByType(types, recurse);

			var amount = 0;

			for (var i = 0; i < items.Length; ++i)
			{
				amount += items[i].Amount;
			}

			return amount;
		}
		#endregion

		private static readonly List<Item> m_FindItemsList = new List<Item>();

		#region Non-Generic FindItem[s] by Type
		public Item[] FindItemsByType(Type type)
		{
			return FindItemsByType(type, true);
		}

		public Item[] FindItemsByType(Type type, bool recurse)
		{
			if (m_FindItemsList.Count > 0)
			{
				m_FindItemsList.Clear();
			}

			RecurseFindItemsByType(this, type, recurse, m_FindItemsList);

			return m_FindItemsList.ToArray();
		}

		private static void RecurseFindItemsByType(Item current, Type type, bool recurse, List<Item> list)
		{
			if (current != null && current.Items.Count > 0)
			{
				var items = current.Items;

				for (var i = 0; i < items.Count; ++i)
				{
					var item = items[i];

					if (type.IsAssignableFrom(item.GetType())) // item.GetType().IsAssignableFrom( type ) )
					{
						list.Add(item);
					}

					if (recurse && item is Container)
					{
						RecurseFindItemsByType(item, type, recurse, list);
					}
				}
			}
		}

		public Item[] FindItemsByType(Type[] types)
		{
			return FindItemsByType(types, true);
		}

		public Item[] FindItemsByType(Type[] types, bool recurse)
		{
			if (m_FindItemsList.Count > 0)
			{
				m_FindItemsList.Clear();
			}

			RecurseFindItemsByType(this, types, recurse, m_FindItemsList);

			return m_FindItemsList.ToArray();
		}

		private static void RecurseFindItemsByType(Item current, Type[] types, bool recurse, List<Item> list)
		{
			if (current != null && current.Items.Count > 0)
			{
				var items = current.Items;

				for (var i = 0; i < items.Count; ++i)
				{
					var item = items[i];

					if (InTypeList(item, types))
					{
						list.Add(item);
					}

					if (recurse && item is Container)
					{
						RecurseFindItemsByType(item, types, recurse, list);
					}
				}
			}
		}

		public Item FindItemByType(Type type)
		{
			return FindItemByType(type, true);
		}

		public Item FindItemByType(Type type, bool recurse)
		{
			return RecurseFindItemByType(this, type, recurse);
		}

		private static Item RecurseFindItemByType(Item current, Type type, bool recurse)
		{
			if (current != null && current.Items.Count > 0)
			{
				var list = current.Items;

				for (var i = 0; i < list.Count; ++i)
				{
					var item = list[i];

					if (type.IsAssignableFrom(item.GetType()))
					{
						return item;
					}
					else if (recurse && item is Container)
					{
						var check = RecurseFindItemByType(item, type, recurse);

						if (check != null)
						{
							return check;
						}
					}
				}
			}

			return null;
		}

		public Item FindItemByType(Type[] types)
		{
			return FindItemByType(types, true);
		}

		public Item FindItemByType(Type[] types, bool recurse)
		{
			return RecurseFindItemByType(this, types, recurse);
		}

		private static Item RecurseFindItemByType(Item current, Type[] types, bool recurse)
		{
			if (current != null && current.Items.Count > 0)
			{
				var list = current.Items;

				for (var i = 0; i < list.Count; ++i)
				{
					var item = list[i];

					if (InTypeList(item, types))
					{
						return item;
					}
					else if (recurse && item is Container)
					{
						var check = RecurseFindItemByType(item, types, recurse);

						if (check != null)
						{
							return check;
						}
					}
				}
			}

			return null;
		}
		#endregion

		#region Generic FindItem[s] by Type
		public List<T> FindItemsByType<T>() where T : Item
		{
			return FindItemsByType<T>(true, null);
		}

		public List<T> FindItemsByType<T>(bool recurse) where T : Item
		{
			return FindItemsByType<T>(recurse, null);
		}

		public List<T> FindItemsByType<T>(Predicate<T> predicate) where T : Item
		{
			return FindItemsByType(true, predicate);
		}

		public List<T> FindItemsByType<T>(bool recurse, Predicate<T> predicate) where T : Item
		{
			if (m_FindItemsList.Count > 0)
			{
				m_FindItemsList.Clear();
			}

			var list = new List<T>();

			RecurseFindItemsByType(this, recurse, list, predicate);

			return list;
		}

		private static void RecurseFindItemsByType<T>(Item current, bool recurse, List<T> list, Predicate<T> predicate)
			where T : Item
		{
			if (current != null && current.Items.Count > 0)
			{
				var items = current.Items;

				for (var i = 0; i < items.Count; ++i)
				{
					var item = items[i];

					if (typeof(T).IsAssignableFrom(item.GetType()))
					{
						var typedItem = (T)item;

						if (predicate == null || predicate(typedItem))
						{
							list.Add(typedItem);
						}
					}

					if (recurse && item is Container)
					{
						RecurseFindItemsByType(item, recurse, list, predicate);
					}
				}
			}
		}

		public T FindItemByType<T>() where T : Item
		{
			return FindItemByType<T>(true);
		}

		public T FindItemByType<T>(Predicate<T> predicate) where T : Item
		{
			return FindItemByType(true, predicate);
		}

		public T FindItemByType<T>(bool recurse) where T : Item
		{
			return FindItemByType<T>(recurse, null);
		}

		public T FindItemByType<T>(bool recurse, Predicate<T> predicate) where T : Item
		{
			return RecurseFindItemByType(this, recurse, predicate);
		}

		private static T RecurseFindItemByType<T>(Item current, bool recurse, Predicate<T> predicate) where T : Item
		{
			if (current != null && current.Items.Count > 0)
			{
				var list = current.Items;

				for (var i = 0; i < list.Count; ++i)
				{
					var item = list[i];

					if (typeof(T).IsAssignableFrom(item.GetType()))
					{
						var typedItem = (T)item;

						if (predicate == null || predicate(typedItem))
						{
							return typedItem;
						}
					}
					else if (recurse && item is Container)
					{
						var check = RecurseFindItemByType(item, recurse, predicate);

						if (check != null)
						{
							return check;
						}
					}
				}
			}

			return null;
		}
		#endregion

		private static bool InTypeList(Item item, Type[] types)
		{
			var t = item.GetType();

			for (var i = 0; i < types.Length; ++i)
			{
				if (types[i].IsAssignableFrom(t))
				{
					return true;
				}
			}

			return false;
		}

		private static void SetSaveFlag(ref SaveFlag flags, SaveFlag toSet, bool setIf)
		{
			if (setIf)
			{
				flags |= toSet;
			}
		}

		private static bool GetSaveFlag(SaveFlag flags, SaveFlag toGet)
		{
			return (flags & toGet) != 0;
		}

		[Flags]
		private enum SaveFlag : byte
		{
			None = 0x00000000,
			MaxItems = 0x00000001,
			GumpID = 0x00000002,
			DropSound = 0x00000004,
			LiftOverride = 0x00000008,
			GridPositions = 0x00000010
		}

		public override void Serialize(GenericWriter writer)
		{

			base.Serialize(writer);

			writer.Write(3); // version

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag(ref flags, SaveFlag.MaxItems, m_MaxItems != -1);
			SetSaveFlag(ref flags, SaveFlag.GumpID, m_GumpID != -1);
			SetSaveFlag(ref flags, SaveFlag.DropSound, m_DropSound != -1);
			SetSaveFlag(ref flags, SaveFlag.LiftOverride, m_LiftOverride);

			writer.Write((byte)flags);

			/*if (GetSaveFlag(flags, SaveFlag.GridPositions))
            {
                writer.Write(_Positions.Count);

                foreach (var kvp in _Positions)
                {
                    writer.Write(kvp.Key);
                    writer.Write(kvp.Value);
                }
            }*/

			if (GetSaveFlag(flags, SaveFlag.MaxItems))
			{
				writer.WriteEncodedInt(m_MaxItems);
			}

			if (GetSaveFlag(flags, SaveFlag.GumpID))
			{
				writer.WriteEncodedInt(m_GumpID);
			}

			if (GetSaveFlag(flags, SaveFlag.DropSound))
			{
				writer.WriteEncodedInt(m_DropSound);
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 4:
					{
						// Lire et ignorer la qualit�
						reader.ReadInt();
						goto case 3;
					}
				case 3:
				case 2:
					{
						var flags = (SaveFlag)reader.ReadByte();

						if (GetSaveFlag(flags, SaveFlag.MaxItems))
						{
							m_MaxItems = reader.ReadEncodedInt();
						}
						else
						{
							m_MaxItems = -1;
						}

						if (GetSaveFlag(flags, SaveFlag.GumpID))
						{
							m_GumpID = reader.ReadEncodedInt();
						}
						else
						{
							m_GumpID = -1;
						}

						if (GetSaveFlag(flags, SaveFlag.DropSound))
						{
							m_DropSound = reader.ReadEncodedInt();
						}
						else
						{
							m_DropSound = -1;
						}

						m_LiftOverride = GetSaveFlag(flags, SaveFlag.LiftOverride);

						break;
					}
				case 1:
					{
						m_MaxItems = reader.ReadInt();
						goto case 0;
					}
				case 0:
					{
						if (version < 1)
						{
							m_MaxItems = GlobalMaxItems;
						}

						m_GumpID = reader.ReadInt();
						m_DropSound = reader.ReadInt();

						if (m_GumpID == DefaultGumpID)
						{
							m_GumpID = -1;
						}

						if (m_DropSound == DefaultDropSound)
						{
							m_DropSound = -1;
						}

						if (m_MaxItems == DefaultMaxItems)
						{
							m_MaxItems = -1;
						}

						//m_Bounds = new Rectangle2D( reader.ReadPoint2D(), reader.ReadPoint2D() );
						reader.ReadPoint2D();
						reader.ReadPoint2D();

						break;
					}
			}

			UpdateContainerData();
		}

		private static int? m_GlobalMaxItems;
		private static int? m_GlobalMaxWeight;

		public static int GlobalMaxItems 
		{
			get
			{
				if (!m_GlobalMaxItems.HasValue)
				{
					m_GlobalMaxItems = Config.Get("CarryWeight.GlobalMaxItems", 125);
				}

				return m_GlobalMaxItems.Value;
			}
			set
			{
				m_GlobalMaxItems = value;
			}
		}

		public static int GlobalMaxWeight
		{
			get
			{
				if (!m_GlobalMaxWeight.HasValue)
				{
					m_GlobalMaxWeight = Config.Get("CarryWeight.GlobalMaxWeight", 400);
				}

				return m_GlobalMaxWeight.Value;
			}
			set
			{
				m_GlobalMaxWeight = value;
			}
		}

		public Container(int itemID)
			: base(itemID)
		{
			m_GumpID = -1;
			m_DropSound = -1;
			m_MaxItems = -1;

			UpdateContainerData();
		}

		public override int GetTotal(TotalType type)
		{
			switch (type)
			{
				case TotalType.Gold:
				return m_TotalGold;

				case TotalType.Items:
				return m_TotalItems;

				case TotalType.Weight:
				return m_TotalWeight;
			}

			return base.GetTotal(type);
		}

		public override void UpdateTotal(Item sender, TotalType type, int delta)
		{
			if (sender != this && delta != 0 && !sender.IsVirtualItem)
			{
				switch (type)
				{
					case TotalType.Gold:
					m_TotalGold += delta;
					break;

					case TotalType.Items:
					m_TotalItems += delta;
					InvalidateProperties();
					break;

					case TotalType.Weight:
					m_TotalWeight += delta;
					InvalidateProperties();
					break;
				}
			}

			base.UpdateTotal(sender, type, delta);
		}

		public override void UpdateTotals()
		{
			m_TotalGold = 0;
			m_TotalItems = 0;
			m_TotalWeight = 0;

			var items = m_Items;

			if (items == null)
			{
				return;
			}

			for (var i = 0; i < items.Count; ++i)
			{
				var item = items[i];

				item.UpdateTotals();

				if (item.IsVirtualItem)
				{
					continue;
				}

				m_TotalGold += item.TotalGold;
				m_TotalItems += item.TotalItems + 1;
				m_TotalWeight += item.TotalWeight + item.PileWeight;
			}
		}

		public Container(Serial serial)
			: base(serial)
		{ }

		public virtual bool OnStackAttempt(Mobile from, Item stack, Item dropped)
		{
			if (!CheckHold(from, dropped, true, false))
			{
				return false;
			}

			return stack.StackWith(from, dropped);
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			if (TryDropItem(from, dropped, true))
			{
				from.SendSound(GetDroppedSound(dropped), GetWorldLocation());

				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual bool TryDropItem(Mobile from, Item dropped, bool sendFullMessage)
		{
			if (CheckStack(from, dropped))
			{
				if (!CheckHold(from, dropped, sendFullMessage, false))
				{
					return false;
				}

				var list = Items;

				for (var i = 0; i < list.Count; ++i)
				{
					var item = list[i];

					if (!(item is Container) && item.StackWith(from, dropped, false))
					{
						return true;
					}
				}
			}

			if (!CheckHold(from, dropped, sendFullMessage, true))
			{
				return false;
			}

			DropItem(dropped);

			return true;
		}

		public override void AddItem(Item item)
		{
			ValidateGridLocation(item);

			base.AddItem(item);
		}

		public virtual void Destroy()
		{
			var loc = GetWorldLocation();
			var map = Map;

			for (var i = Items.Count - 1; i >= 0; --i)
			{
				if (i < Items.Count)
				{
					Items[i].SetLastMoved();
					Items[i].MoveToWorld(loc, map);
				}
			}

			Delete();
		}

		public virtual void DropItem(Item dropped)
		{
			if (dropped == null)
			{
				return;
			}

			AddItem(dropped);

			var bounds = dropped.GetGraphicBounds();
			var ourBounds = Bounds;

			int x, y;

			if (bounds.Width >= ourBounds.Width)
			{
				x = (ourBounds.Width - bounds.Width) / 2;
			}
			else
			{
				x = Utility.Random(ourBounds.Width - bounds.Width);
			}

			if (bounds.Height >= ourBounds.Height)
			{
				y = (ourBounds.Height - bounds.Height) / 2;
			}
			else
			{
				y = Utility.Random(ourBounds.Height - bounds.Height);
			}

			x += ourBounds.X;
			x -= bounds.X;

			y += ourBounds.Y;
			y -= bounds.Y;

			dropped.Location = new Point3D(x, y, 0);
		}

		public override void OnDoubleClickSecureTrade(Mobile from)
		{
			if (from.InRange(GetWorldLocation(), 2))
			{
				DisplayTo(from);

				var cont = GetSecureTradeCont();

				if (cont != null)
				{
					var trade = cont.Trade;

					if (trade != null && trade.From.Mobile == from)
					{
						DisplayTo(trade.To.Mobile);
					}
					else if (trade != null && trade.To.Mobile == from)
					{
						DisplayTo(trade.From.Mobile);
					}
				}
			}
			else
			{
				from.SendLocalizedMessage(500446); // That is too far away.
			}
		}

		public virtual bool DisplaysContent => true;

		public virtual bool CheckContentDisplay(Mobile from)
		{
			if (!DisplaysContent)
			{
				return false;
			}

			var root = RootParent;

			if (root == null || root is Item || root == from || from.IsStaff())
			{
				return true;
			}

			return false;
		}

		public List<Mobile> Openers { get; set; }

		public virtual bool IsPublicContainer => false;

		public override void OnDelete()
		{
			base.OnDelete();

			Openers = null;
		}

		public virtual void DisplayTo(Mobile to)
		{
			ProcessOpeners(to);

			var ns = to.NetState;

			if (ns == null)
			{
				return;
			}

			ValidatePositions();

			to.Send(new ContainerDisplay(this));

			to.Send(new ContainerContent(to, this));

			foreach (var o in Items)
			{
				to.Send(o.OPLPacket);
			}
		}

		public void ProcessOpeners(Mobile opener)
		{
			if (!IsPublicContainer)
			{
				var contains = false;

				if (Openers != null)
				{
					var worldLoc = GetWorldLocation();
					var map = Map;

					for (var i = 0; i < Openers.Count; ++i)
					{
						var mob = Openers[i];

						if (mob == opener)
						{
							contains = true;
						}
						else
						{
							var range = GetUpdateRange(mob);

							if (mob.Map != map || !mob.InRange(worldLoc, range))
							{
								Openers.RemoveAt(i--);
							}
						}
					}
				}

				if (!contains)
				{
					if (Openers == null)
					{
						Openers = new List<Mobile>();
					}

					Openers.Add(opener);
				}
				else if (Openers != null && Openers.Count == 0)
				{
					Openers = null;
				}
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (DisplaysContent) //CheckContentDisplay( from ) )
			{
				if (ParentsContain<Item>() || IsLockedDown || IsSecure) //Root Parent is the Mobile.  Parent could be another containter.
				{
					list.Add(1073841, "{0}\t{1}\t{2}", TotalItems, MaxItems, TotalWeight);
					// Contents: ~1_COUNT~/~2_MAXCOUNT~ items, ~3_WEIGHT~ stones
				}
				else
				{
					list.Add(1072241, "{0}\t{1}\t{2}\t{3}", TotalItems, MaxItems, TotalWeight, MaxWeight);
					// Contents: ~1_COUNT~/~2_MAXCOUNT~ items, ~3_WEIGHT~/~4_MAXWEIGHT~ stones
				}
			}
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.IsStaff() || from.InRange(GetWorldLocation(), 2))
			{
				DisplayTo(from);
			}
			else
			{
				from.SendLocalizedMessage(500446); // That is too far away.
			}
		}
	}

	public class ContainerData
	{
		static ContainerData()
		{
			m_Table = new Dictionary<int, ContainerData>();

			var path = Path.Combine(Core.BaseDirectory, "Data/containers.cfg");

			if (!File.Exists(path))
			{
				m_Default = new ContainerData(0x3C, new Rectangle2D(44, 65, 142, 94), 0x48);
				return;
			}

			using (var reader = new StreamReader(path))
			{
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					line = line.Trim();

					if (line.Length == 0 || line.StartsWith("#"))
					{
						continue;
					}

					try
					{
						var split = line.Split('\t');

						if (split.Length >= 3)
						{
							var gumpID = Utility.ToInt32(split[0]);

							var aRect = split[1].Split(' ');
							if (aRect.Length < 4)
							{
								continue;
							}

							var x = Utility.ToInt32(aRect[0]);
							var y = Utility.ToInt32(aRect[1]);
							var width = Utility.ToInt32(aRect[2]);
							var height = Utility.ToInt32(aRect[3]);

							var bounds = new Rectangle2D(x, y, width, height);

							var dropSound = Utility.ToInt32(split[2]);

							var data = new ContainerData(gumpID, bounds, dropSound);

							if (m_Default == null)
							{
								m_Default = data;
							}

							if (split.Length >= 4)
							{
								var aIDs = split[3].Split(',');

								for (var i = 0; i < aIDs.Length; i++)
								{
									var id = Utility.ToInt32(aIDs[i]);

									if (m_Table.ContainsKey(id))
									{
										Console.WriteLine(@"Warning: double ItemID entry in Data\containers.cfg");
									}
									else
									{
										m_Table[id] = data;
									}
								}
							}
						}
					}
					catch
					{ }
				}
			}

			if (m_Default == null)
			{
				m_Default = new ContainerData(0x3C, new Rectangle2D(44, 65, 142, 94), 0x48);
			}
		}

		private static ContainerData m_Default;
		private static readonly Dictionary<int, ContainerData> m_Table;

		public static ContainerData Default { get => m_Default; set => m_Default = value; }

		public static ContainerData GetData(int itemID)
		{
			m_Table.TryGetValue(itemID, out var data);

			if (data != null)
			{
				return data;
			}
			else
			{
				return m_Default;
			}
		}

		private readonly int m_GumpID;
		private readonly Rectangle2D m_Bounds;
		private readonly int m_DropSound;

		public int GumpID => m_GumpID;
		public Rectangle2D Bounds => m_Bounds;
		public int DropSound => m_DropSound;

		public ContainerData(int gumpID, Rectangle2D bounds, int dropSound)
		{
			m_GumpID = gumpID;
			m_Bounds = bounds;
			m_DropSound = dropSound;
		}
	}
}
