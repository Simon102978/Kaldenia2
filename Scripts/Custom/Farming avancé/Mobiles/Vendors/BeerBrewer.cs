using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class BeerBrewer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public BeerBrewer() : base( "The Beer Brewer" )
		{
			SetSkill( SkillName.Parry, 85.0, 150.0 );
			SetSkill( SkillName.Swords, 60.0, 183.0 );
			SetSkill( SkillName.ArmsLore, 85.0, 150.0 );
			SetSkill( SkillName.Anatomy, 60.0, 183.0 );
			SetSkill( SkillName.Archery, 85.0, 150.0 );
			SetSkill( SkillName.Hiding, 60.0, 183.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBeerBrewer() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Shirt() );
			AddItem( new LongPants() );
			AddItem( new Boots() );
		}

		public BeerBrewer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}