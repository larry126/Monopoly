using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Config;
using Monopoly.Model;

namespace Monopoly.Consol
{
	class Program
	{
		private static Dice dice = new Dice();

		static void Main(string[] args)
		{
			Console.WriteLine("Please enter the number of players");
			string strNumber = Console.ReadLine();
			int numberOfPlayers = Convert.ToInt16(strNumber);
			Player[] players = new Player[2];
			for (int i = 0; i < players.Length; i++)
			{
				players[i] = new Player(i.ToString(), 5000);
				Console.WriteLine("Please enter the name of Player " + (i + 1).ToString());
				players[i].Name = Console.ReadLine();
			}
			int turn = 0;
			while (players.SkipWhile(p => p.Bankruptcy == true).ToArray().Length > 1)
			{
				Player currentPlayer = players[turn % (players.Length + 1)];
				if (currentPlayer.Bankruptcy == false)
				{
					bool isTokenAbleToMove = true;
					while (true)
					{
						Console.WriteLine("\n" + currentPlayer.Name + ", pick your move:");
						if (isTokenAbleToMove)
						{
							Console.WriteLine("1. Roll dice");
						}
						else
						{
							Console.WriteLine("1. End");
						}
						Console.WriteLine("2. Trade");
						Console.WriteLine("3. Build\n");
						string moveMade = Console.ReadLine(); 
						if (moveMade == "1" && isTokenAbleToMove)
						{
							isTokenAbleToMove = false;
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
					}
					Console.WriteLine("Between turns");
					Console.ReadKey();
				}
			}
		}
	}
}
