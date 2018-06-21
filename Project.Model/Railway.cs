using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
	public class Railway : Property
	{
		public Railway(string name, int price) : base(name, price)
		{
			Name = name;
			Price = price;
		}
	}
}
