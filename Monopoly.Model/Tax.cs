using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Tax : Space
	{
		private TypeOfTax typeOfTax;
		private int tax = 0;

		public Tax(GameState gameState, string name, TypeOfTax type, int taxValue) : base(gameState, name)
		{
			typeOfTax = type;
			tax = taxValue;
		}

		public override List<LandOnActions> GetLandOnActions(Player player)
		{
			return new List<LandOnActions> { LandOnActions.Tax };
		}

		public override bool CanPlayerPerformAction(Player player, LandOnActions action)
		{
			if (action == LandOnActions.Tax)
			{
				return player.IsAbleToAfford(tax) || player.IsAbleToAfford((int)(player.GetTotalAssests() * 0.1));
			}
			else
			{
				return player.IsAbleToAfford(tax);
			}
		}

		public override void PerformAction(Player player, LandOnActions action)
		{
			if (action == LandOnActions.Rent)
			{
				player.PayMoney(tax);
			}
			else
			{
				player.PayMoney(tax);
			}
		}
	}
}