﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class StaticFunctions
	{
		public static void MovePlayer(Player player, int squares)
		{
			player.Location = (player.Location + squares) % (spaces.Count() + 1);
		}

		public void MovePlayerTo(Player player, int destination, bool GoToJail = false)
		{

		}
	}
}