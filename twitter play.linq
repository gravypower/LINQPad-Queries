<Query Kind="Program" />

//This example take an RSS feed and parses it with linq

void Main()
{


	XmlReader reader = XmlReader.Create("");
	XmlDocument xmlDoc = new XmlDocument();
	xmlDoc.Load(reader);

	XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
	ns.AddNamespace("at", "http://www.w3.org/2005/Atom");
	ns.AddNamespace("twitter", "http://api.twitter.com/");

	var nodes = xmlDoc.DocumentElement.SelectNodes("//at:entry", ns);
	
	
	Console.WriteLine(xmlDoc.InnerXml);
	
	foreach(XmlNode i in nodes)
	{
		Console.WriteLine(i.SelectSingleNode("at:author/at:name", ns).InnerText);
		Console.WriteLine(i.SelectSingleNode("at:published", ns).InnerText);
		
		var s = i.SelectSingleNode("at:content", ns).InnerText.IndexOf(':');
		Console.WriteLine(i.SelectSingleNode("at:content", ns).InnerText.Substring(s+2));
		
		Console.WriteLine(i.SelectNodes("at:link", ns)[1].Attributes["href"].InnerText);
	}
		
	
}