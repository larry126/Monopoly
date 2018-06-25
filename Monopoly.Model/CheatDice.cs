using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class CheatDice : IDice
	{
		private int i=-1;

		public int Roll()
		{
			i++;
			return i % 6 + 1;
		}
	}
}
