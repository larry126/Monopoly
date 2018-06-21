using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
	public class Regular : Property
	{
		int HousePrice { get; set; }
		int Houses { get; set; } = 0;
		Colours Group { get; }

		public Regular(string name, int price, Colours group) : base(name, price)
		{
			Name = name;
			Price = price;
			Group = group;
		}

		public void PayRent(Player payer)
		{

		}
	}
}
