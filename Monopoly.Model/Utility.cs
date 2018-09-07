using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Utility : Property
	{
		public Utility(string name, int price) : base(name, price)
		{
			Name = name;
			Price = price;
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
