using Server.Custom;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreDevoue : LivreClasse
	{
        [Constructable]
        public LivreDevoue() : this(Classe.GetClasse(20))
        {
        }

        [Constructable]
        public LivreDevoue(Classe classe) : base(classe)
        {
            Name = "livre de Dévoué";
        }

        public LivreDevoue(Serial serial) : base(serial)
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