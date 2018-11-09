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


		static void Main(string[] args)
		{
			while (true)
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
						players[i] = new ConsolePlayer(Console.ReadLine(), 5000);
					}
					Player winner = PlayGame(players);
					Console.WriteLine("Player " + winner.Name + "is the winner!");
					Console.WriteLine("Thank to all of you for playing");
					Console.ReadKey();
					break;
				}
			}
		}

		static Player PlayGame(Player[] players)
		{
			GameState gameState = new GameState(players);
			Dice realDice = new Dice(gameState);
			int turn = 1;
			while (!gameState.gameEnd())
			{
				Player currentPlayer = gameState.GetTurnPlayer();
				if (!currentPlayer.Bankruptcy)
				{
					Console.WriteLine("\nTurn " + turn);
					turn++;
					while (true)
					{
						ShowPlayerStatus(currentPlayer);
						int phaseState = gameState.MainPhase(realDice);
						if (phaseState == 1)
						{
							gameState.RollDiceProcedure(currentPlayer, realDice);
						}
						else if (phaseState == 2)
						{
							break;
						}
					}
					Console.WriteLine("\n" + currentPlayer.Name + "'s turn ends");
					Console.WriteLine("Between turns\n");
					gameState.BetweenTurns();
				}
			}
			return players.First(p => !p.Bankruptcy);
		}

		static void ShowPlayerStatus(Player player)
		{
			Console.WriteLine("\nPlayer " + player.Name + "'s money:" + player.Money.ToString());
			Console.WriteLine("Player " + player.Name + "'s properties:");
			if (player.OwnedProperties.Count == 0)
			{
				Console.WriteLine("N/A");
			}
			foreach (Property prop in player.OwnedProperties)
			{
				Console.WriteLine(prop.Name);
			}
			Console.WriteLine("");
		}
	}
}
