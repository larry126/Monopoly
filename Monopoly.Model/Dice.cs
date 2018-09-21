using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Dice
	{
		private static Random dice = new Random();

		public int Roll()
		{
			Globals.LastRoll = dice.Next(1, 7);
			return Globals.LastRoll;
		}

		public int RollTwice()
		{
			Globals.LastRoll = Roll() + Roll();
			return Globals.LastRoll;
		}
	}
}
