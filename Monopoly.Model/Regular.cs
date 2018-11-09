using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Regular : Property
	{
		private int[] Rents = new int[6];
		private int _housePrice;
		public int HousePrice { get => _housePrice; set => _housePrice = value; }
		private int _houseCount;
		public int HouseCount { get => _houseCount; }
		private Colours _group;
		public Colours Group { get => _group; set => _group = value; }

		public Regular(GameState gameState, string name, int price, Colours group, int baseRent, int house1, int house2, int house3, int house4, int hotel) : base(gameState, name, price)
		{
			_gameState = gameState;
			_name = name;
			Price = price;
			_group = group;
			_houseCount = 0;
			HousePrice = GetHousePrices()[group];
			Rents[0] = baseRent;
			Rents[1] = house1;
			Rents[2] = house2;
			Rents[3] = house3;
			Rents[4] = house4;
			Rents[5] = hotel;
		}

		private Dictionary<Colours, int> GetHousePrices()
		{
			Dictionary<Colours, int> housePrices = new Dictionary<Colours, int>();
			housePrices.Add(Colours.Brown, 50);
			housePrices.Add(Colours.LightBlue, 50);
			housePrices.Add(Colours.Orange, 100);
			housePrices.Add(Colours.Purple, 100);
			housePrices.Add(Colours.Yellow, 150);
			housePrices.Add(Colours.Red, 150);
			housePrices.Add(Colours.Green, 200);
			housePrices.Add(Colours.DarkBlue, 200);
			return housePrices;
		}

		public void BuildHouses(int value)
		{
			_owner.PayMoney(_housePrice * value);
			_houseCount = _houseCount + value;
		}

		public void SellHouses(int value)
		{
			_owner.GainMoney(_housePrice / 2 * value);
			_houseCount = _houseCount - value;
		}

		public override int GetRent()
		{
			return Rents[HouseCount];
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
			return this.MortgageState == false && this._houseCount == 0;
		}
	}
}
