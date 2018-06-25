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
			}
			int turn = 0;
			while (players.SkipWhile(p => p.Bankruptcy == true).ToArray().Length <= 1)
			{
				Player currentPlayer = players[turn % (players.Length + 1)];
				if (currentPlayer.Bankruptcy == false)
				{

				}
			}
		}
	}
}
