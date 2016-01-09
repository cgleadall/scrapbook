<Query Kind="Program" />

void Main()
{
	var filename = @"\\vmware-host\Shared Folders\Downloads\Numbers.csv";
	var outfilename = @"\\vmware-host\Shared Folders\Downloads\NumbersSorted.csv";
	var lines = System.IO.File.ReadAllLines(filename);
	
	var outputData = new List<RowData>();
	
	foreach (var line in lines)
	{
		var rd = new RowData(line);
		//rd.ToString().Dump();
		outputData.Add(rd);
	}
	
	
	using( var fos = System.IO.File.CreateText(outfilename))
	{
		foreach (var row in outputData)
		{
			fos.WriteLine(row.ToString());
		}
	}
	
}

// Define other methods and classes here
public class RowData
{
	public string date { get; set; }
	public List<int> Values { get; set; }
	
	
	public RowData()
	{
		Values = new List<int>();
	}
	
	public RowData(string rowData)
	:this()
	{
		var elements = rowData.Split(",".ToCharArray());
		date = elements[0];
		
		for (int i = 2; i < 7; i++)
		{
			Values.Add( Int32.Parse(elements[i]));
		}
	}
	
	public override string ToString(){
		var sb = new StringBuilder();
		Values.OrderBy (v => v).ToList().ForEach(v => sb.Append(string.Format("{0},",v)));
		
		sb.Length -= 1;
		
		return string.Format("{0},,{1}", date, sb.ToString());
		
	}
}