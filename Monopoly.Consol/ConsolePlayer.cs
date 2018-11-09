using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class ConsolePlayer : Player
	{
		public ConsolePlayer(string name, int money) : base(name, money)
		{
			Name = name;
			Money = money;
		}

		public override LandOnActions LandOnDecision(List<LandOnActions> actions)
		{
			if (actions.Count > 0)
			{
				for (int i = 0; i < actions.Count; i++)
				{
					Console.WriteLine((i + 1) + ". " + actions[i]);
				}
				if (actions.Contains(0))
				{
					Console.WriteLine("9. No action");
				}
				while (true)
				{
					Console.Write("\nPlease choose 1 action to proceed\t");
					string actionChoice = Console.ReadLine();
					int input;
					if (int.TryParse(actionChoice, out input) && (input >= 1 && input <= actions.Count) || (input == 9))
					{
						if (input == 9 && actions.Contains(0))
						{
							return 0;
						}
						input--;
						return actions[input];
					}
					else
					{
						Console.WriteLine("\nIncorrect input");
					}
				}
			}
			return 0;
		}

		public override int RollDice(IDice dice, int noOfDice = 2)
		{
			int rollResult = dice.Roll(noOfDice);
			Console.WriteLine("You have rolled " + rollResult);
			return rollResult;
		}

		public override void MoveBy(int squares)
		{
			base.MoveBy(squares);
			Console.WriteLine("You have moved to " + GameState.CurrentBoard[LocSeq].Name + "\n");
		}

		public override void MoveTo(int destination, bool passGO = true, bool GoToJail = false)
		{
			base.MoveTo(destination, passGO, GoToJail);
			Console.WriteLine("You have moved to " + GameState.CurrentBoard[LocSeq].Name + "\n");
			if (GoToJail)
			{
				Console.WriteLine("You are now in jail\n");
			}
		}

		public override int MainPhaseDecision()
		{
			while (true)
			{
				HashSet<int> availableActions = new HashSet<int> { 1 };
				Console.WriteLine("Player " + this.Name + ",\n");
				if (this.IsAbleToRoll())
				{
					Console.WriteLine("1. Roll dice");
				}
				else
				{
					Console.WriteLine("1. End");
				}
				if (this.CanBuild())
				{
					Console.WriteLine("3. Build House");
					availableActions.Add(3);
				}
				if (this.CanSellHouses())
				{
					Console.WriteLine("4. Sell House");
					availableActions.Add(4);
				}
				if (this.CanMortgageProperties())
				{
					Console.WriteLine("5. Mortgage property");
					availableActions.Add(5);
				}
				if (this.CanLiftMortgagedProperties())
				{
					Console.WriteLine("6. Lift mortgaged property");
					availableActions.Add(6);
				}
				Console.Write("\nPick your move:\t");
				string input = Console.ReadLine();
				int move = 0;
				if (int.TryParse(input, out move) && availableActions.Contains(move))
				{
					return move;
				}
			}
		}

		public override int IncomeTaxDecision()
		{
			throw new NotImplementedException();
		}

		public override Colours? BuildHousesDecision()
		{
			while (true)
			{
				List<Colours> buildableGroups = this.GetBuildableGroups();
				Console.WriteLine("You can build on these groups");
				foreach (Colours g in buildableGroups)
				{
					Console.WriteLine(g);
				}
				Console.Write("\nPick the group you wish to build on:\t");
				string input = Console.ReadLine();
				if (input.Trim(' ').ToLower() == "cancel")
				{
					return null;
				}
				Colours group;
				if (Enum.TryParse(input, out group) && buildableGroups.Contains(group))
				{
					return group;
				}
				else
				{
					Console.Write("\nIncorrect input");
				}
			}
		}

		public override Colours? SellHousesDecision()
		{
			while (true)
			{
				List<Colours> houseSellableGroups = this.GetHousesSellableGroups();
				Console.WriteLine("You can build on these groups");
				foreach (Colours g in houseSellableGroups)
				{
					Console.WriteLine(g);
				}
				Console.Write("\nPick the group from which you wish to sell house:\t");
				string input = Console.ReadLine();
				if (input.Trim(' ').ToLower() == "cancel")
				{
					return null;
				}
				Colours group;
				if (Enum.TryParse(input, out group) && houseSellableGroups.Contains(group))
				{
					return group;
				}
				else
				{
					Console.Write("\nIncorrect input");
				}
			}
		}

		public override Property MortgagePropertyDecision()
		{
			while (true)
			{
				List<Property> mortgageableProperties = this.OwnedProperties.TakeWhile(p => p.CanBeMortgaged()).ToList();
				Console.WriteLine("You can build on these groups");
				foreach (Property p in mortgageableProperties)
				{
					Console.WriteLine(p.Name);
				}
				Console.Write("\nPick the property do you wish to mortgage:\t");
				string input = Console.ReadLine();
				if (input.Trim(' ').ToLower() == "cancel")
				{
					return null;
				}
				if (mortgageableProperties.Exists(p => p.Name.Trim(' ').ToLower() == input.Trim(' ').ToLower()))
				{
					return mortgageableProperties.First(p => p.Name.Trim(' ').ToLower() == input.Trim(' ').ToLower());
				}
			}
		}

		public override Property LiftMortgagedPropertyDecision()
		{
			while (true)
			{
				List<Property> liftableMortgagedProperties = this.OwnedProperties.TakeWhile(prop => prop.CanMortgageBeLifted()).ToList();
				Console.WriteLine("You can build on these groups");
				foreach (Property p in liftableMortgagedProperties)
				{
					Console.WriteLine(p.Name);
				}
				Console.Write("\nPick the property do you wish to lift mortgage:\t");
				string input = Console.ReadLine();
				if (input.Trim(' ').ToLower() == "cancel")
				{
					return null;
				}
				if (liftableMortgagedProperties.Exists(p => p.Name.Trim(' ').ToLower() == input.Trim(' ').ToLower()))
				{
					return liftableMortgagedProperties.First(p => p.Name.Trim(' ').ToLower() == input.Trim(' ').ToLower());
				}
			}
		}

		public override int SellAndPayDecision()
		{
			while (true)
			{
				HashSet<int> availableActions = new HashSet<int> { };
				Console.WriteLine("Player " + this.Name + ",\n");
				if (this.CanBuild())
				{
					Console.WriteLine("1. Build House");
					availableActions.Add(1);
				}
				if (this.CanSellHouses())
				{
					Console.WriteLine("2. Sell House");
					availableActions.Add(2);
				}
				if (this.CanMortgageProperties())
				{
					Console.WriteLine("3. Mortgage property");
					availableActions.Add(3);
				}
				if (this.CanLiftMortgagedProperties())
				{
					Console.WriteLine("4. Lift mortgaged property");
					availableActions.Add(4);
				}
				Console.Write("\nPick your move:\t");
				string input = Console.ReadLine();
				int move = 0;
				if (int.TryParse(input, out move) && availableActions.Contains(move))
				{
					return move;
				}
			}
		}

		public override int BetweenTurnsDecision()
		{
			while (true)
			{
				HashSet<int> availableActions = new HashSet<int> { 1 };
				Console.WriteLine("Player " + this.Name + ",\n");
				Console.WriteLine("1. End");
				if (this.CanBuild())
				{
					Console.WriteLine("3. Build House");
					availableActions.Add(3);
				}
				if (this.CanSellHouses())
				{
					Console.WriteLine("4. Sell House");
					availableActions.Add(4);
				}
				if (this.CanMortgageProperties())
				{
					Console.WriteLine("5. Mortgage property");
					availableActions.Add(5);
				}
				if (this.CanLiftMortgagedProperties())
				{
					Console.WriteLine("6. Lift mortgaged property");
					availableActions.Add(6);
				}
				Console.Write("\nPick your move:\t");
				string input = Console.ReadLine();
				int move = 0;
				if (int.TryParse(input, out move) && availableActions.Contains(move))
				{
					return move;
				}
			}
		}

		public override bool LiftMortgageDecision(Property prop)
		{
			if (prop.CanMortgageBeLifted())
			{
				Console.Write("\nWould you like to lift the mortgage of" + prop.Name + "\nEnter Y/N\t");
				string input = Console.ReadLine();
				switch (input)
				{
					case "Y":
						return true;
					case "N":
						return false;
					default:
						Console.WriteLine("Incorrect input");
						LiftMortgageDecision(prop);
						break;
				}
			}
			return false;
		}

		public void ShowPlayerStatus()
		{
			Console.WriteLine("\nPlayer " + Name + "'s money:" + Money.ToString());
			Console.WriteLine("Player " + Name + "'s properties:");
			if (OwnedProperties.Count == 0)
			{
				Console.WriteLine("N/A");
			}
			foreach (Property prop in OwnedProperties)
			{
				Console.WriteLine(prop.Name);
			}
			Console.WriteLine("");
		}

		public override void sellAndPay(Space space, LandOnActions action)
		{
			while (!space.CanPlayerPerformAction(this, action))
			{
				ShowPlayerStatus();
				int moveMade = SellAndPayDecision();
				if (moveMade == 1)
				{
					Colours? group = BuildHousesDecision();
					if (group != null)
					{
						BuildHouses((Colours)group);
					}
				}
				else if (moveMade == 2)
				{
					Colours? group = SellHousesDecision();
					if (group != null)
					{
						SellHouses((Colours)group);
					}
				}
				else if (moveMade == 3)
				{
					Property prop = MortgagePropertyDecision();
					if (prop != null)
					{
						prop.Mortgage();
					}
				}
				else if (moveMade == 4)
				{
					Property prop = LiftMortgagedPropertyDecision();
					if (prop != null)
					{
						prop.LiftMortgage();
					}
				}
			}
		}
	}
}
