<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.ServiceModel.Syndication</Namespace>
</Query>

//This example take an RSS feed and parses it with linq

void Main()
{
XmlReader reader = XmlReader.Create("");
SyndicationFeed feed = SyndicationFeed.Load(reader);
var sotcPosts = from item in feed.Items
		select new
		{
				Title = item.Title.Text,
				Date = item.PublishDate,
				Link = item.Links.First()
		};

foreach(var i in sotcPosts)
{
Console.WriteLine("Title: " + i.Title + " Date:" + i.Date.ToString() + "Link: " + i.Link.Uri);
}

}

// Define other methods and classes here