using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Railway : Property
	{
		public Railway(GameState gameState, string name, int price = 200) : base(gameState, name, price)
		{
			_gameState = gameState;
			_name = name;
			Price = price;
		}

		public override int GetRent()
		{
			int railwaysOwned = _owner.OwnedProperties.FindAll(p => p.GetType() == typeof(Railway)).Count;
			return 25 * (int)Math.Pow(2, railwaysOwned - 1);
		}

		public override bool CanPlayerPerformAction(Player player, LandOnActions action)
		{
			if (action == LandOnActions.Buy)
			{
				return player.IsAbleToAfford(Price);
			}
			else if (action == LandOnActions.Rent)
			{
				return player.IsAbleToAfford(GetRent());
			}
			return false;
		}

		public override bool CanBeMortgaged()
		{
			return this.MortgageState == false;
		}
	}
}