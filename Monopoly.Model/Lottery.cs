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

		public override bool[] CanPlayerPerformAction(Player player, int action)
		{
			throw new NotImplementedException();
		}

		public override List<int> GetLandOnActions(Player player)
		{
			return new List<int> { };
		}

		public override void PerformAction(Player player, int action)
		{
			throw new NotImplementedException();
		}
	}
}