using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Model;

namespace Monopoly.Test
{
	[TestClass]
	public class BuyPropertyTests
	{
		private CheatDice dice = new CheatDice();
		Property[] properties = new Property[] { new Regular("Mediterranean Avenue", 60, Colours.Brown, 2, 10, 30, 90, 160, 250), new Railway("Reading Railroad", 200), new Utility("Water Work", 150) };
		Player A = new Player("A", 200);
		Player B = new Player("B", 200);
		Player C = new Player("C", 0);

		[TestMethod]
		public void BuyRegularTest()
		{
			properties[0].PerformAction(A, "Buy");
			Assert.AreEqual(A.Money, 140);
			Assert.AreEqual(A.Properties[0], properties[0]);
			Assert.AreEqual(properties[0].Owner, A);
		}
		[TestMethod]
		public void BuyRailwayTestRegular()
		{
			properties[1].PerformAction(A, "Buy");
			Assert.AreEqual(A.Money, 0);
			Assert.AreEqual(A.Properties[0], properties[1]);
			Assert.AreEqual(properties[1].Owner, A);
		}
		[TestMethod]
		public void BuyUtilityTestRegular()
		{
			properties[2].PerformAction(A, "Buy");
			Assert.AreEqual(A.Money, 50);
			Assert.AreEqual(A.Properties[0], properties[2]);
			Assert.AreEqual(properties[2].Owner, A);
		}
	}

	[TestClass]
	public class RailwaysRentTests
	{
		private CheatDice dice = new CheatDice();
		Property[] railways = new Property[4] { new Railway("Reading Railroad", 200), new Railway("Reading Railroad", 200), new Railway("Reading Railroad", 200), new Railway("Reading Railroad", 200) };
		Player A = new Player("A", 9999);
		Player B = new Player("B", 9999);
		Player C = new Player("C", 0);

		[TestMethod]
		public void RailwayRent1()
		{
			railways[0].PerformAction(A, "Buy");
			Assert.AreEqual(25, railways[0].GetRent());
		}
		[TestMethod]
		public void RailwayRent2()
		{
			railways[0].PerformAction(A, "Buy");
			railways[1].PerformAction(A, "Buy");
			Assert.AreEqual(50, railways[0].GetRent());
		}
		[TestMethod]
		public void RailwayRent3()
		{
			railways[0].PerformAction(A, "Buy");
			railways[1].PerformAction(A, "Buy");
			railways[2].PerformAction(A, "Buy");
			Assert.AreEqual(100, railways[0].GetRent());
		}
		[TestMethod]
		public void RailwayRent4()
		{
			railways[0].PerformAction(A, "Buy");
			railways[1].PerformAction(A, "Buy");
			railways[2].PerformAction(A, "Buy");
			railways[3].PerformAction(A, "Buy");
			Assert.AreEqual(200, railways[0].GetRent());
		}
	}

	[TestClass]
	public class UtilitiesRentTests
	{
		Property[] utilities = new Property[] { new Utility("Water Work", 150), new Utility("Electric Company", 150) };
		Player A = new Player("A", 500);
		Player B = new Player("B", 500);
		Player C = new Player("C", 0);

		[TestMethod]
		public void UtilitiesRent0()
		{
			CheatDice dice = new CheatDice();
			utilities[0].PerformAction(A, "Buy");
			Assert.AreEqual(0, utilities[0].GetRent());
		}
		[TestMethod]
		public void UtilitiesRent1()
		{
			CheatDice dice = new CheatDice();
			int lastRoll = dice.Roll();
			utilities[0].PerformAction(A, "Buy");
			Assert.AreEqual(4, utilities[0].GetRent());
		}
		[TestMethod]
		public void UtilitiesRent2()
		{
			CheatDice dice = new CheatDice();
			int lastRoll = dice.Roll();
			utilities[0].PerformAction(A, "Buy");
			utilities[1].PerformAction(A, "Buy");
			Assert.AreEqual(10, utilities[0].GetRent());
		}
		[TestMethod]
		public void UtilitiesRentTwice1()
		{
			CheatDice dice = new CheatDice();
			int lastRoll = dice.RollTwice();
			utilities[0].PerformAction(A, "Buy");
			Assert.AreEqual(12, utilities[0].GetRent());
		}
		[TestMethod]
		public void UtilitiesRentTwice2()
		{
			CheatDice dice = new CheatDice();
			int lastRoll = dice.RollTwice();
			utilities[0].PerformAction(A, "Buy");
			utilities[1].PerformAction(A, "Buy");
			Assert.AreEqual(30, utilities[0].GetRent());
		}
	}
}