using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Board
	{
		public static Space[] spaces { get; private set; }

		public Board()
		{
			spaces = Globals.DefaultBoard;
		}

		public void MovePlayer(Player player, int squares)
		{
			player.Location = (player.Location + squares) % (spaces.Count() + 1);
		}

		public void MovePlayerBy(Player player, int squares)
		{
			int previousLocation = player.Location;
			player.Location = (player.Location + squares) % (spaces.Count() + 1);
			if (squares > 0 && previousLocation > player.Location)
			{

			}
		}

		public void MovePlayerTo(Player player, int destination, bool passGO = false, bool GoToJail = false)
		{
			player.Location = destination;
		}
	}
}
