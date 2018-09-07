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
		List<Property> Properties = new List<Property>();

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
				int? numberOfHouses = prop.Houses;
				Colours? groupOfProperty = prop.Group;

				StaticFunctions f = new StaticFunctions();

				if (numberOfHouses != null && groupOfProperty != null )
				{
					value = value + (int)numberOfHouses *  f.GetHousePrices()[(int)groupOfProperty]/2;
				}

				value = value + prop.Price / 2;		}

			return 0;
		}
	}
}
