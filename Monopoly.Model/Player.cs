using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Player
	{
		public string Name { get; set; }
		public int Money { get; set; }
		public int Location = 0;
		public bool Bankruptcy = false;
		public bool Jailed = false;
		public List<Property> Properties = new List<Property>();

		public Player(string name, int money)
		{
			Name = name;
			Money = money;
		}

		public int GetTotalMoney()
		{
			int value = Money;

			foreach (var prop in Properties)
			{
//				int? numberOfHouses = prop.HouseCount;

//				if (numberOfHouses != null)
//				{
//					value = value + (int)numberOfHouses * (int)prop.HousePrice / 2;
//				}

				value = value + prop.Price / 2;
			}

			return value;
		}

		public bool[] IsAbleToPay(int cost)
		{
			bool[] ableToPay = new bool[2] { false, false};
			if (Money > 0)
			{
				ableToPay[0] = true;
			}
			if (GetTotalMoney() > cost)
			{
				ableToPay[1] = true;
			}
			return ableToPay;
		}
	}
}
