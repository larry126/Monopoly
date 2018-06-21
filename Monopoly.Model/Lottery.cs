using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Lottery : Space
	{

		public Lottery(string name,TypeOfLottery type) : base(name)
		{
			Name = name;
		}

	}
}