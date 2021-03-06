﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public interface ISpaces
	{
		Space[] GetSpaces();
	}
	public interface IRules
	{
		bool AuctionIfNotBuied();

		int[] GetHousePrices();

		int GetTotalHouseTokens();
	}
}
