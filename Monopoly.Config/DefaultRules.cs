using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Model;

namespace Monopoly.Config
{
	public class Rules : IRules
	{
		public bool AuctionIfNotBuied()
		{
			return true;
		}

		public int[] GetHousePrices()
		{
			return new int[] { 50, 100, 150, 200 };
		}

		public int GetTotalHouseTokens()
		{
			return 32;
		}
	}
}