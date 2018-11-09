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
		public Utility(GameState gameState, string name, int price) : base(gameState, name, price)
		{
			_gameState = gameState;
			_name = name;
			Price = price;
		}

		public override int GetRent()
		{
			int utilitiesOwned = _owner.OwnedProperties.FindAll(p => p.GetType() == typeof(Utility)).Count;
			return GameState.LastRoll * ((utilitiesOwned == 1) ? 4 : 10);
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
