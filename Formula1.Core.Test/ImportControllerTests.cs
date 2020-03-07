using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Formula1.Core.Test
{
	[TestClass()]
	public class ImportControllerTests
	{
		/// <summary>
		/// Als erste Übung die Rennen aus der XML-Datei parsen
		/// </summary>
		[TestMethod()]
		public void T01_LoadRacesFromRacesXmlTest()
		{
			var races = ImportController.LoadRacesFromRacesXml().ToList();
			Assert.AreEqual(21, races.Count());
			Assert.AreEqual("Melbourne", races.First().City);
			Assert.AreEqual(1, races.First().Number);
			Assert.AreEqual("Abu Dhabi", races.Last().City);
			Assert.AreEqual(21, races.Last().Number);
		}

		/// <summary>
		/// Alle Results in Collections laden.
		/// </summary>
		[TestMethod()]
		public void T02_LoadResultsFromResultsXmlTest()
		{
			var results = ImportController.LoadResultsFromXmlIntoCollections().ToList();
			Assert.AreEqual(11, results.GroupBy(res => res.Team).Count());
			Assert.AreEqual(24, results.GroupBy(res => res.Driver).Count());
			Assert.AreEqual(462, results.Count);
		}

		[TestMethod()]
		public void T03_LoadRacesFromRacesXmlTest()
		{
			var races = ImportController.LoadRacesFromRacesXml().ToList();
			Assert.AreEqual("UAE", races.Last().Country);
			Assert.AreEqual("Australia", races.First().Country);
			Assert.AreEqual(20, races.Last().Number - 1);
		}

		/// <summary>
		/// Results von Verstappen
		/// </summary>
		[TestMethod()]
		public void T04_LoadVerstappenResults()
		{
			// Lade Verstappens Platzierungen in ein anonymes Objekt { City, Position }
			// Sortiert nach der Rennnummer
			var results = ImportController.LoadResultsFromXmlIntoCollections().ToArray();
			var endResults = results
				.Where(res => res.Driver.Name.Equals("Verstappen Max"))
				.OrderBy(res => res.Race.Number)
				.ToList();

			Assert.AreEqual(21, endResults.Count);

			Assert.AreEqual(10, endResults[0].Position);
			Assert.AreEqual("Melbourne", endResults[0].Race.City);

			Assert.AreEqual(6, endResults[14].Position);
			Assert.AreEqual("Marina Bay", endResults[14].Race.City);
		}
	}
}