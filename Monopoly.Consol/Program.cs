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
			Console.Write("Monopoly");
			while (true)
			{
				Console.Write("\n\nPlease enter the number of players:\t");
				string strNumber = Console.ReadLine();
				int numberOfPlayers;
				if (int.TryParse(strNumber, out numberOfPlayers) && numberOfPlayers >= 2 && numberOfPlayers <= 8)
				{
					Player[] players = new Player[numberOfPlayers];
					for (int i = 0; i < players.Length; i++)
					{
						Console.Write("\nIs Player " + (i + 1).ToString() + " a bot player? \nY/N\t");
						string botCheck = Console.ReadLine();
						Console.Write("\nPlease enter Player " + (i + 1).ToString() + "'s name:\t");
						if (botCheck == "Y")
						{
							players[i] = new BotPlayer(Console.ReadLine(), 5000);
						}
						else
						{
							players[i] = new ConsolePlayer(Console.ReadLine(), 5000);
						}
					}
					Player winner = PlayGame(players);
					Console.WriteLine("\n\nPlayer " + winner.Name + "is the winner!");
					Console.WriteLine("\nThank to all of you for playing");
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
					ShowPlayerStatus(currentPlayer);
					gameState.MainPhase(realDice);
					ShowPlayerStatus(currentPlayer);
					Console.WriteLine("\n" + currentPlayer.Name + "'s turn ends");
					ShowAllPlayersStatus(gameState);
					Console.WriteLine("\nBetween turns");
					gameState.BetweenTurns();
					ShowAllPlayersStatus(gameState);
					turn++;
				}
			}
			return players.First(p => !p.Bankruptcy);
		}

		static void ShowAllPlayersStatus(GameState gameState)
		{
			foreach (Player p in gameState.AllPlayers)
			{
				ShowPlayerStatus(p);
			}
		}

		static void ShowPlayerStatus(Player player)
		{
			Console.WriteLine("\nPlayer " + player.Name + ":");
			if (player.Bankruptcy)
			{
				Console.WriteLine("Bankrupted");
				return;
			}
			Console.WriteLine("Money:" + player.Money.ToString());
			Console.WriteLine("Properties No. : " + player.OwnedProperties.Count);
			Console.WriteLine("Properties:");
			if (player.OwnedProperties.Count == 0)
			{
				Console.WriteLine("N/A");
			}
			foreach (Property prop in player.OwnedProperties)
			{
				Console.WriteLine(prop.Name);
			}
		}
	}
}
