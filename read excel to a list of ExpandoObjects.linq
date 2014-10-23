<Query Kind="Program">
  <Reference Relative="..\..\..\ajob\Downloads\EPPlus 3.0 sample\Binaries\EPPlus.dll">C:\Users\ajob\Downloads\EPPlus 3.0 sample\Binaries\EPPlus.dll</Reference>
  <Namespace>OfficeOpenXml</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

void Main()
{
	var expandoList = new List<dynamic>();
	
	FileInfo newFile = new FileInfo(@"");
	using (ExcelPackage package = new ExcelPackage(newFile))
	{
		// get the first worksheet in the workbook
		var worksheet = package.Workbook.Worksheets["resources (URLs only)"];
		
		var propertyMapping = new Dictionary<int, string>();
		var expando = new ExpandoObject() as IDictionary<string, Object>;
		
		for (int row = 1; row < worksheet.Dimension.End.Row + 1; row++)
		{ 
			var d = new ExpandoObject() as IDictionary<string, Object>;
			for (var col = 1; col < worksheet.Dimension.End.Column + 1; col++)
			{
				var text = String.Empty;
				var cell = worksheet.Cells[row, col];
			
				if(cell.Merge)
				{
					ExcelRange mergedRange = null;;
					foreach (var element in worksheet.MergedCells)
					{
						mergedRange = worksheet.Cells[element];
						if(mergedRange.Any(x=>x.Address == cell.Address))
						{
							break;
						}
					}
					
					if(mergedRange != null)
					{
						text = mergedRange.First().Text;
					}
				}
				else
				{
					text = worksheet.Cells[row, col].Text;
				}
			
				if(row == 1)
				{
					propertyMapping.Add(col, text);
					if(expando.ContainsKey(text))
					{
						if(expando[text].GetType() != typeof(List<object>))
						{
							expando.Remove(text);
							expando.Add(text, new List<object>());
						}
					}
					else
					{
						expando.Add(text, string.Empty);
					}
				}
				else
				{
				
					if(!string.IsNullOrEmpty(text))
					{
						if(expando[propertyMapping[col]].GetType() == typeof(List<object>))
						{
							if(!d.ContainsKey(propertyMapping[col]))
							{
								var list = new List<string>();
								list.Add(text);
								d.Add(propertyMapping[col], list);
							}
							else
							{
								var list = d[propertyMapping[col]] as List<string>;
								list.Add(text);
							}
						}
						else
						{
							d.Add(propertyMapping[col], text);
						}
						
					}
				}
			}
			if(row > 1)
			{
			if(d.Values.Count > 0)
				expandoList.Add(d);
			}
		}
	}
	
	Console.WriteLine(expandoList);
}