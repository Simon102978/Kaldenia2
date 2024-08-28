using Server.Multis;
using Server;
using System;

public class BoatRental
{
	public Mobile Renter { get; set; }
	public MediumBoatDeed Deed { get; set; }
	public DateTime EndTime { get; set; }
	public Timer RentalTimer { get; set; }

	public BoatRental(Mobile renter, MediumBoatDeed deed, TimeSpan duration)
	{
		Renter = renter;
		Deed = deed;
		EndTime = DateTime.Now + duration;
	}
	public TimeSpan GetRemainingTime()
	{
		return EndTime - DateTime.Now;
	}

public BoatRental(GenericReader reader)
	{
		Deserialize(reader);
	}

	public void Serialize(GenericWriter writer)
	{
		writer.Write(Renter);
		writer.Write(Deed);
		writer.Write(EndTime);
	}

	public void Deserialize(GenericReader reader)
	{
		Renter = reader.ReadMobile();
		Deed = reader.ReadItem() as MediumBoatDeed;
		EndTime = reader.ReadDateTime();
	}
}
