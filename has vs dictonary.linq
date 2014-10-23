<Query Kind="Program" />

void Main()
{
	var loops = 1000000;
	var hashtable = new Hashtable();
	var dictionary = new Dictionary<int, int>();
	
	for (int i = 0; i < loops; i++)
	{
		hashtable.Add(i, i);
		dictionary.Add(i, i);
	}
	
	
	long dictonaryTotal = 0;
	long hashtableTotal = 0;
	
	for (int j = 0; j < 100; j++)
	{
		Stopwatch dictonaryStopwatch = new Stopwatch();
		dictonaryStopwatch.Start();
		for (int i = 0; i < loops; i++)
		{
			var t = dictionary[i];
		}
		
		dictonaryStopwatch.Stop();
		dictonaryTotal += dictonaryStopwatch.ElapsedMilliseconds;
		
		Stopwatch hashtableStopwatch = new Stopwatch();
		hashtableStopwatch.Start();
		for (int i = 0; i < loops; i++)
		{
			var t = hashtable[i];
		}
		
		hashtableStopwatch.Stop();
		hashtableTotal += hashtableStopwatch.ElapsedMilliseconds;
	}
	
	Console.WriteLine("dictonary: " + dictonaryTotal);
	Console.WriteLine("hashtable:" + hashtableTotal);
}

// Define other methods and classes here