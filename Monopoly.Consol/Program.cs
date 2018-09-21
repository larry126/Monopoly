using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Model;

namespace Monopoly.Consol
{
	class Program
	{
		static Dice dice = new Dice();

		static CheatDice cheatDice = new CheatDice();

		static void Main(string[] args)
		{
			Console.Write("Please enter the number of players:\t");
			string strNumber = Console.ReadLine();
			int numberOfPlayers;
			if (int.TryParse(strNumber, out numberOfPlayers) && numberOfPlayers >= 2 && numberOfPlayers <= 8)
			{
				Player[] players = new Player[numberOfPlayers];
				for (int i = 0; i < players.Length; i++)
				{
					Console.Write("Please enter the name of Player " + (i + 1).ToString() + ":\t");
					players[i] = new Player(Console.ReadLine(), 5000);
				}
				Player[] endGame = PlayGame(players);
				Player winner = players.First(p => p.Bankruptcy == false);
				Console.WriteLine("Player " + winner.Name + "is the winner!");
				Console.WriteLine("Thank to all of you for playing");
				Console.ReadKey();
			}
		}

		static void ShowPlayerStatus(Player player)
		{
			Console.WriteLine("\nPlayer " + player.Name + "'s money:" + player.Money);
			Console.WriteLine("Player " + player.Name + "'s properties:");
			if (player.Properties.Count == 0)
			{
				Console.WriteLine("N/A");
			}
			foreach (Property prop in player.Properties)
			{
				Console.WriteLine(prop.Name);
			}
			Console.WriteLine("");
		}

		static Player[] PlayGame(Player[] players)
		{
			Board gameBoard = new Board();
			int turn = 1;
			int realTurn = 0;
			while (players.SkipWhile(p => p.Bankruptcy == true).ToArray().Length > 1)
			{
				Player currentPlayer = players[realTurn % (players.Length + 1)];
				if (currentPlayer.Bankruptcy == false)
				{
					Console.WriteLine("\nTurn " + turn + "\n");
					bool isTokenAbleToMove = true;
					while (true)
					{
						ShowPlayerStatus(currentPlayer);
						Console.WriteLine("Player " + currentPlayer.Name + ",\n");
						if (isTokenAbleToMove)
						{
							Console.WriteLine("1. Roll dice");
						}
						else
						{
							Console.WriteLine("1. End");
						}
						Console.WriteLine("2. Trade");
						Console.WriteLine("3. Build");
						Console.Write("\nPick your move:\t");
						string moveMade = Console.ReadLine();
						if (moveMade == "1" && isTokenAbleToMove)
						{
							isTokenAbleToMove = false;
							RollDiceProcedure(gameBoard, currentPlayer);
						}
						else if (moveMade == "1")
						{
							Console.WriteLine("\n" + currentPlayer.Name + "'s turn ends");
							break;
						}
						else if (moveMade == "2")
						{
							Console.WriteLine("\nTrade made");
						}
						else if (moveMade == "3")
						{
							Console.WriteLine("\nBuilding");
						}
						else
						{
							Console.WriteLine("\nIncorrect input");
						}
						turn++;
					}
					Console.WriteLine("Between turns");
					realTurn++;
					Console.ReadKey();
				}
			}
			return players;
		}

		static void RollDiceProcedure(Board gameBoard,Player currentPlayer)
		{
			gameBoard.MovePlayer(currentPlayer, 1);
			Space currentPlayerLocation = Board.spaces[currentPlayer.Location];
			Console.WriteLine("\nYou rolled " + Globals.LastRoll);
			Console.WriteLine("You moved to " + currentPlayerLocation.Name + "\n");
			List<int> availableActions = currentPlayerLocation.GetLandOnActions(currentPlayer);
			int[] performableActions = new int[5];
			int actionCount = 1;
			foreach (int action in availableActions)
			{
				bool[] performable = currentPlayerLocation.CanPlayerPerformAction(currentPlayer, action);
				if (performable.Contains(true))
				{
					Console.WriteLine(actionCount + ". " + Globals.Actions[action]);
					performableActions[(actionCount - 1)] = action;
					actionCount++;
				}
				else if (action == 1)
				{
					currentPlayer.Bankruptcy = true;
					return;
				}
			}
			if (performableActions.Length > 0)
			{
				if (availableActions.Contains(0))
				{
					Console.WriteLine("9. No action");
				}
				while (true)
				{
					Console.Write("\nPlease choose 1 action to proceed\t");
					string actionChoice = Console.ReadLine();
					int input;
					if (int.TryParse(actionChoice, out input) && input >= 1 && input < actionCount)
					{
						if (input == 9 && availableActions.Contains(0))
						{
							return;
						}
						int action = availableActions[performableActions[input]];
						bool[] performable = currentPlayerLocation.CanPlayerPerformAction(currentPlayer, action);
						if (performable[0] == true)
						{
							currentPlayerLocation.PerformAction(currentPlayer, action);
							break;
						}
						else
						{

						}
					}
					else
					{
						Console.WriteLine("\nIncorrect input");
					}
				}
			}
		}
	}
}
