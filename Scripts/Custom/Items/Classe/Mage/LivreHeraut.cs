using Server.Custom;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreHeraut : LivreClasse
	{
        [Constructable]
        public LivreHeraut() : this(Classe.GetClasse(6))
        {
        }

        [Constructable]
        public LivreHeraut(Classe classe) : base(classe)
        {
            Name = "livre d'héraut";
        }

        public LivreHeraut(Serial serial) : base(serial)
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