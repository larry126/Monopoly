using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Model;
using System.Linq;
using System.Collections.Generic;

namespace Monopoly.Test
{
	public class functions
	{
		[TestClass]
		public class BuyPropertyTests
		{
			static Player[] players = StaticFunctions.CreatePlayers(new int[] { 200, 200, 200 });
			Player A = players[0];
			Player B = players[1];
			Player C = players[2];
			static GameState gameState = new GameState(players);

			[TestMethod]
			public void BuyRegularTest()
			{
				Player A = players[0];
				Property prop1 = (Property)gameState.CurrentBoard[1];
				prop1.PerformAction(A, LandOnActions.Buy);
				Assert.AreEqual(140, A.Money);
				Assert.AreEqual(prop1, A.OwnedProperties[0]);
				Assert.IsTrue(prop1.IsOwner(A));
			}
			[TestMethod]
			public void BuyRailwayTest()
			{
				Player B = players[1];
				Property prop2 = (Property)gameState.CurrentBoard[5];
				prop2.PerformAction(B, LandOnActions.Buy);
				Assert.AreEqual(0, B.Money);
				Assert.AreEqual(prop2, B.OwnedProperties[0]);
				Assert.IsTrue(prop2.IsOwner(B));
			}
			[TestMethod]
			public void BuyUtilityTest()
			{
				Player C = players[2];
				Property prop3 = (Property)gameState.CurrentBoard[12];
				prop3.PerformAction(C, LandOnActions.Buy);
				Assert.AreEqual(50, C.Money);
				Assert.AreEqual(prop3, C.OwnedProperties[0]);
				Assert.IsTrue(prop3.IsOwner(C));
			}
		}

		[TestClass]
		public class RailwaysRentTests
		{
			static Player[] players = StaticFunctions.CreatePlayers(new int[] { 5000 });
			Player A = players[0];
			static GameState gameState = new GameState(players);
			static Property[] railways = new Property[4] { (Property)gameState.CurrentBoard[5], (Property)gameState.CurrentBoard[15], (Property)gameState.CurrentBoard[25], (Property)gameState.CurrentBoard[35] };

			[TestMethod]
			public void RailwayRent1()
			{
				railways[0].PerformAction(A, 0);
				Assert.AreEqual(25, railways[0].GetRent());
			}
			[TestMethod]
			public void RailwayRent2()
			{
				railways[1].PerformAction(A, 0);
				Assert.AreEqual(50, railways[0].GetRent());
			}
			[TestMethod]
			public void RailwayRent3()
			{
				railways[2].PerformAction(A, 0);
				Assert.AreEqual(100, railways[0].GetRent());
			}
			[TestMethod]
			public void RailwayRent4()
			{
				railways[3].PerformAction(A, 0);
				Assert.AreEqual(200, railways[0].GetRent());
			}
		}

		[TestClass]
		public class UtilitiesRentTests
		{
			static Player[] players = StaticFunctions.CreatePlayers(new int[] { 5000, 5000 });
			static GameState gameState = new GameState(players);
			static Property[] utilities = new Property[] { (Property)gameState.CurrentBoard[12], (Property)gameState.CurrentBoard[28] };
			Player A = players[0];
			Player B = players[1];
			Dice dice = new Dice(gameState);

			[TestMethod]
			public void UtilitiesRent0()
			{
				utilities[0].PerformAction(A, 0);
				Assert.AreEqual(0, utilities[0].GetRent());
			}
			[TestMethod]
			public void UtilitiesRent1()
			{
				CheatDice cheatDice = new CheatDice(gameState, 1);
				int lastRoll = cheatDice.Roll();
				Assert.AreEqual(4, utilities[0].GetRent());
			}
			[TestMethod]
			public void UtilitiesRent2()
			{
				CheatDice cheatDice = new CheatDice(gameState, 1);
				int lastRoll = cheatDice.Roll();
				utilities[1].PerformAction(A, 0);
				Assert.AreEqual(10, utilities[0].GetRent());
			}
			[TestMethod]
			public void UtilitiesRentTwice1()
			{
				CheatDice cheatDice = new CheatDice(gameState, 2);
				int lastRoll = cheatDice.Roll();
				utilities[0].PerformAction(B, 0);
				Assert.AreEqual(8, utilities[0].GetRent());
			}
			[TestMethod]
			public void UtilitiesRentTwice2()
			{
				CheatDice cheatDice = new CheatDice(gameState, 2);
				int lastRoll = cheatDice.Roll();
				utilities[1].PerformAction(B, 0);
				Assert.AreEqual(20, utilities[0].GetRent());
			}
			[TestMethod]
			public void UtilitiesRentReal()
			{
				int lastRoll = dice.Roll();
				Assert.AreEqual(lastRoll * 10, utilities[0].GetRent());
			}
		}

		[TestClass]
		public class BuildHouseTests
		{
			static Player[] players = StaticFunctions.CreatePlayers(new int[] { 5000, 5000 });
			static GameState gameState = new GameState(players);
			Player A = players[0];
			Player B = players[1];
			CheatDice dice = new CheatDice(gameState, 1);

			[TestMethod]
			public void BuildHouseTest0()
			{
				gameState.CurrentBoard[1].PerformAction(A, 0);
				Assert.AreEqual(false, A.CanBuild());
			}
			[TestMethod]
			public void BuildHouseTest1()
			{
				gameState.CurrentBoard[3].PerformAction(A, 0);
				Assert.AreEqual(true, A.CanBuild());
			}
		}
	}
}