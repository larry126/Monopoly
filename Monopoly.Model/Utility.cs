using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Model;

namespace Monopoly.Model
{
	public class Utility : Property
	{
		public Utility(string name, int price) : base(name, price)
		{
			Name = name;
			Price = price;
		}

		public override int GetRent()
		{
			int utilitiesOwned = Owner.Properties.FindAll(p => p.GetType() == typeof(Utility)).Count;
			return Globals.LastRoll * ((utilitiesOwned == 1) ? 4 : 10);
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
	}
}
