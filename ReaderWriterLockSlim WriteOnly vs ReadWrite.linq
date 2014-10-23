<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>


ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
static Dictionary<string, int> test = new Dictionary<string, int>();

void Main()
{
	test.Clear();

	for (int i = 0; i < 10000; i++)
	{
		test.Add(i.ToString(), i);
	}
	
		Thread t1 = new System.Threading.Thread(() => DoSomeStuff("A"));
		t1.Start();
		Thread t2 = new System.Threading.Thread(() => DoSomeStuff("B"));
		t2.Start();
		Thread t3 = new System.Threading.Thread(() => DoSomeStuff("C"));
		t3.Start();
		Thread t4 = new System.Threading.Thread(() => DoSomeStuff("D"));
		t4.Start();
		
		Thread t5 = new System.Threading.Thread(() => DoSomeStuffNoReadLock("E"));
		t5.Start();
		Thread t6 = new System.Threading.Thread(() => DoSomeStuffNoReadLock("F"));
		t6.Start();
		Thread t7 = new System.Threading.Thread(() => DoSomeStuffNoReadLock("G"));
		t7.Start();
		Thread t8 = new System.Threading.Thread(() => DoSomeStuffNoReadLock("H"));
		t8.Start();

	
}
#region DoSomeStuff
public void DoSomeStuff(string key)
{
	try
	{
		if (locker.TryEnterReadLock(10000))
		{
			for (int i = 0; i < 10000; i++)
			{
				var t = test[i.ToString()];
			}
		}
	}
	finally
	{
		locker.ExitReadLock();
	}
	string keya = String.Empty;
	try
	{
		if (locker.TryEnterWriteLock(10000))
		{
			
			for (int i = 0; i < 100; i++)
			{
				keya = key + i.ToString();
				test.Add(keya, 9999);
			}
		}
	}
	catch
	{
		Console.WriteLine(keya);
	}
	finally
	{
		locker.ExitWriteLock();
	}
}
#endregion

#region DoSomeStuffNoReadLock
public void DoSomeStuffNoReadLock(string key)
{
	try
	{
		for (int i = 0; i < 10000; i++)
		{
			var t = test[i.ToString()];
		}
	}
	catch(Exception ex)
	{
		Console.WriteLine(ex.Message + " in DoSomeStuffNoWriteLock");
	}
	string keya = String.Empty;
	try
	{
		if (locker.TryEnterWriteLock(10000))
		{
			for (int i = 0; i < 100; i++)
			{
				keya = key + i.ToString();
				test.Add(keya, 9999);
			}
		}
	}
	catch
	{
		Console.WriteLine(keya);
	}
	finally
	{
		locker.ExitWriteLock();
	}
}
#endregion

