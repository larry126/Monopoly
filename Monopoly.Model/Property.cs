using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public abstract class Property : Space
	{
		protected Player _owner;
		public int Price { get; protected set; }
		public bool? MortgageState { get; protected set; } 

		public Property(GameState gameState, string name, int price) : base(gameState, name)
		{
			_gameState = gameState;
			_name = name;
			Price = price;
		}

		public void Mortgage()
		{
			MortgageState = true;
			_owner.GainMoney(Price / 2);
		}

		public bool CanMortgageBeLifted()
		{
			return this.MortgageState == true && _owner.IsAbleToAfford((int)(Price / 2 * 1.1));
		}

		public void LiftMortgage()
		{
			MortgageState = false;
			_owner.PayMoney((int)(Price / 2 * 1.1));
		}

		public bool IsOwner(Player player)
		{
			return _owner == player;
		}

		public void ChangeOwner(Player player)
		{
			_owner = player;
			if (player != null && MortgageState == true)
			{
				player.PayMoney((int)(Price * (player.LiftMortgageDecision(this) ? 1 : 0 + 0.1)));
			}
		}

		public abstract bool CanBeMortgaged();

		public abstract int GetRent();

		public override void PerformAction(Player player, LandOnActions action)
		{
			if (action == LandOnActions.Buy)
			{
				player.PayMoney(Price);
				_owner = player;
			}
			else if (action == LandOnActions.Rent)
			{
				player.PayMoney(GetRent());
			}
		}

		public override List<LandOnActions> GetLandOnActions(Player player)
		{
			List<LandOnActions> actions = new List<LandOnActions> { };
			if (_owner == null)
			{
				actions.Add(LandOnActions.Buy);
				actions.Add(LandOnActions.NoAction);
			}
			else if (_owner != player)
			{
				actions.Add(LandOnActions.Rent);
			}
			return actions;
		}
	}
}