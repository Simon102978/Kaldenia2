using Server.Custom;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreAnarchiste : LivreClasse
	{
        [Constructable]
        public LivreAnarchiste() : this(Classe.GetClasse(19))
        {
        }

        [Constructable]
        public LivreAnarchiste(Classe classe) : base(classe)
        {
            Name = "livre d'Anarchiste";
        }

        public LivreAnarchiste(Serial serial) : base(serial)
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