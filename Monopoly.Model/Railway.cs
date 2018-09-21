using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Railway : Property
	{
		public Railway(string name, int price) : base(name, price)
		{
			Name = name;
			Price = 200;
		}

		public override int GetRent()
		{
			int railwaysOwned = Owner.Properties.FindAll(p => p.GetType() == typeof(Railway)).Count;
			return 25 * (int)Math.Pow(2, railwaysOwned - 1);
		}

		public override bool[] CanPlayerPerformAction(Player player, int action)
		{
			if (action == 0)
			{
				return player.IsAbleToPay(Price);
			}
			else if (action == 1)
			{
				return player.IsAbleToPay(GetRent());
			}
			return new bool[] { false, false };
		}

		public override void PerformAction(Player player, int action)
		{
			if (action == 0)
			{
				player.Money = player.Money - Price;
				Owner = player;
				player.Properties.Add(this);
			}
			else if (action == 1)
			{
				player.Money = player.Money - GetRent();
			}
		}
	}
}