using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Regular : Property
	{
		public Regular(string name, int price, Colours group) : base(name, price)
		{
			Name = name;
			Price = price;
			Group = group;
			Houses = 0;
		}

		public override bool CanPlayerPerformLandOnProcedure(Player player)
		{
			throw new NotImplementedException();
		}

		public override void PerformLandOnProcedure(Player player)
		{
			throw new NotImplementedException();
		}
	}
}
