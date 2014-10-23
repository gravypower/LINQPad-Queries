<Query Kind="Program" />

void Main()
{
	var path = @"D:\Projects\Cricket\Dependency\Data\serialization\master\sitecore\content\cricketaustralia\news-list\_2A_\_2A_\_2A_";

	var pathArray = path.Split('\\');
	List<string> returnList = new List<string>();
	returnList.Add(pathArray[0]);

	var processArray = pathArray.Skip(1);
	foreach (var i in processArray)
	{
		var fixedPath = string.Empty;
		var fixedPathFlag = false;
		fixedPath = i;
		foreach (char someChar in Path.GetInvalidFileNameChars())
		{
			if (i.Contains(string.Format("_{0:X}_", (int)someChar)))
			{
				fixedPath = fixedPath.Replace("_", "");
				
				int decAgain = int.Parse(fixedPath, System.Globalization.NumberStyles.HexNumber);
				fixedPath = Char.ConvertFromUtf32(decAgain);
				fixedPathFlag = true;
			}
		}

		if (fixedPathFlag)
		{
			returnList.Add(fixedPath);
		}
		else
		{
			returnList.Add(i);
		}
	}
	
	foreach(var i in returnList)
	{
		Console.WriteLine(i);
	}
}

 
