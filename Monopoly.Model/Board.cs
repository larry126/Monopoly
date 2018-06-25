using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Board
	{
		private Space[] spaces { get; set; }

		public Board(ISpaces ispaces)
		{
			spaces = ispaces.GetSpaces();
		}



		public void MovePlayerBy(Player player, int squares)
		{
			int previousLocation = player.Location;
			player.Location = (player.Location + squares) % (spaces.Count() + 1);
			if (squares > 0 && previousLocation > player.Location)
			{

			}
			//spaces[player.Location].LandOn(player)
		}

		public void MovePlayerTo(Player player, int destination, bool passGO = false, bool GoToJail = false)
		{
			player.Location = destination;
		}
	}
}
