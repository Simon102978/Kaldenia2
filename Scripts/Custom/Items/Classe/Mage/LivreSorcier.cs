using Server.Custom;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSorcier : LivreClasse
	{
        [Constructable]
        public LivreSorcier() : this(Classe.GetClasse(6))
        {
        }

        [Constructable]
        public LivreSorcier(Classe classe) : base(classe)
        {
            Name = "livre de sorcier";
        }

        public LivreSorcier(Serial serial) : base(serial)
        {
        }

		public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
        }
	}
}