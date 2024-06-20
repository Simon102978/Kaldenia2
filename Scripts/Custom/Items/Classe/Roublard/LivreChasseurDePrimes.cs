using Server.Custom;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreChasseurDePrimes  : LivreClasse
	{
        [Constructable]
        public LivreChasseurDePrimes() : this(Classe.GetClasse(10))
        {
        }

        [Constructable]
        public LivreChasseurDePrimes(Classe classe) : base(classe)
        {
            Name = "livre de Chasseur de primes";
        }

        public LivreChasseurDePrimes(Serial serial) : base(serial)
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