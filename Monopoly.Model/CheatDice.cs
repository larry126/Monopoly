using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class CheatDice
	{
		private int i = -1;

		public int Roll()
		{
			i++;
			Globals.LastRoll = i % 6 + 1;
			return Globals.LastRoll;
		}

		public int RollTwice()
		{
			Globals.LastRoll = Roll() + Roll();
			return Globals.LastRoll;
		}
	}
}
