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
			if (actions == null || actions.Count == 0)
			{
				return LandOnActions.NoAction;
			}
			Console.WriteLine(" ");
			foreach (LandOnActions action in actions)
			{
				Console.WriteLine(actions.IndexOf(action).ToString() + ". " + action);
			}
			while (true)
			{
				Console.Write("Please choose 1 action to proceed\t");
				string actionChoice = Console.ReadLine();
				if (int.TryParse(actionChoice, out int input) && (input < actions.Count))
				{
					return actions[input];
				}
				else
				{
					Console.WriteLine("\nIncorrect input");
				}
			}
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
			Console.WriteLine("You have moved to " + gameState.CurrentBoard[LocSeq].Name);
		}

		public override void MoveTo(int destination, bool passGO = true, bool GoToJail = false)
		{
			base.MoveTo(destination, passGO, GoToJail);
			Console.WriteLine("You have moved to " + gameState.CurrentBoard[LocSeq].Name + "\n");
			if (GoToJail)
			{
				Console.WriteLine("You are now in jail\n");
			}
		}

		public override OpenGameStateActions MainPhaseDecision()
		{
			while (true)
			{
				OpenGameStateActions[] availableActions = GetAvailableMoves().ToArray();
				Console.WriteLine(" ");
				foreach (OpenGameStateActions act in availableActions)
				{
					Console.WriteLine(Array.IndexOf(availableActions, act).ToString() + ". " + act);
				}
				Console.Write("Player " + Name + ", pick your move:\t");
				string input = Console.ReadLine();
				if (int.TryParse(input, out int move) && move < availableActions.Length)
				{
					return availableActions[move];
				}
				else
				{
					Console.WriteLine("\nIncorrect input");
				}
			}
		}

		public override OpenGameStateActions SellAndPayDecision()
		{
			while (true)
			{
				OpenGameStateActions[] availableActions = GetAvailableMoves().ToArray();
				Console.WriteLine("Player " + Name + ",\n");
				foreach (OpenGameStateActions act in availableActions)
				{
					Console.WriteLine(Array.IndexOf(availableActions, act).ToString() + ". " + act);
				}
				Console.Write("\nPick your move:\t");
				string input = Console.ReadLine();
				if (int.TryParse(input, out int move) && move < availableActions.Count())
				{
					return availableActions[move];
				}
			}
		}

		public override OpenGameStateActions BetweenTurnsDecision()
		{
			while (true)
			{
				OpenGameStateActions[] availableActions = GetAvailableMoves().ToArray();
				Console.WriteLine(" ");
				foreach (OpenGameStateActions act in availableActions)
				{
					Console.WriteLine(Array.IndexOf(availableActions, act).ToString() + ". " + act);
				}
				Console.Write("Player " + Name + ", pick your move:\t");
				string input = Console.ReadLine();
				if (int.TryParse(input, out int move) && move < availableActions.Length)
				{
					return availableActions[move];
				}
				else
				{
					Console.WriteLine("\nIncorrect input");
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
		}
	}
}
