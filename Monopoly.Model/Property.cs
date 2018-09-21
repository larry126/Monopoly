using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public abstract class Property : Space
	{
		private int _price;
		public int Price { get => _price; set => _price = value; }
		private Player _owner;
		public Player Owner { get => _owner; set => _owner = value; }

		public Property(string name, int price) : base(name)
		{
			Name = name;
			Price = price;
		}

		public abstract int GetRent();

		public override List<int> GetLandOnActions(Player player)
		{
			List<int> actions = new List<int> { };
			if (Owner == null)
			{
				actions.Add(0);
			}
			else if (Owner != player)
			{
				actions.Add(1);
			}
			return actions;
		}
	}
}