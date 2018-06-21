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
			return dice.Next(1, 7);
		}
	}
}
