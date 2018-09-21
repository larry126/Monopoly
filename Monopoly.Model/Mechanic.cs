using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Mechanic : Space
	{

		public Mechanic(string name) : base(name)
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