<Query Kind="Program">
  <NuGetReference>NodaTime</NuGetReference>
  <NuGetReference>NodaTime.Testing</NuGetReference>
  <Namespace>NodaTime</Namespace>
  <Namespace>NodaTime.Testing</Namespace>
</Query>

void Main()
{
	var fakeClock = FakeClock.FromUtc(2014, 10, 20, 15, 0, 0);
	var timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("UTC");
	var openingPeriod = new OpeningPeriod(
	IsoDayOfWeek.Monday,
	new LocalTime(9, 0),
	new LocalTime(17, 0));
	
	var dateTime = fakeClock.Now.InZone(timeZone);
	
	System.Console.WriteLine(dateTime.Hour);
	System.Console.WriteLine(openingPeriod.OpeningTime.Hour);

	System.Console.WriteLine(openingPeriod.OpeningTime < dateTime.TimeOfDay );
}

public class OpeningPeriod
    {
		public TimeZone TimeZone{get;set;}
        public IsoDayOfWeek OpeningDayOfWeek { get; set; }
        public LocalTime OpeningTime { get; set; }
        public LocalTime ClosingTime { get; set; }

        public OpeningPeriod(IsoDayOfWeek openingDayOfWeek) : this(openingDayOfWeek, new LocalTime(0, 0), new LocalTime(23, 59))
        {
        }

        public OpeningPeriod(IsoDayOfWeek openingDayOfWeek, LocalTime openingTime)
        {
            OpeningTime = openingTime;
            OpeningDayOfWeek = openingDayOfWeek;
        }

        public OpeningPeriod(IsoDayOfWeek openingDayOfWeek, LocalTime openingTime, LocalTime closingTime)
        {
            OpeningTime = openingTime;
            ClosingTime = closingTime;
            OpeningDayOfWeek = openingDayOfWeek;
        }
    }
