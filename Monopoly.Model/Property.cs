using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public abstract class Property : Space
	{
		public int Price { get; set; }
		public Player Owner { get; set; }

		public Property(string name, int price) : base(name)
		{
			Name = name;
			Price = price;
		}

//		public abstract void PayRent();
	}
}