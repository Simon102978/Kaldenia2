using Server.Multis;
using Server;
using System;

public class BoatRental
{
	public Mobile Renter { get; set; }
	public BaseBoat Boat { get; set; }
	public DateTime EndTime { get; set; }
	public Timer RentalTimer { get; set; }

	public BoatRental(Mobile renter, BaseBoat boat, TimeSpan duration)
	{
		Renter = renter;
		Boat = boat;
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
		writer.Write(Boat);
		writer.Write(EndTime);
	}

	public void Deserialize(GenericReader reader)
	{
		Renter = reader.ReadMobile();
		Boat = reader.ReadItem() as BaseBoat;
		EndTime = reader.ReadDateTime();
	}
}