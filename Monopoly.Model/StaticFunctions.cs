using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class StaticFunctions
	{

		public void MovePlayerTo(Player player, int destination, bool GoToJail = false)
		{

		}

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

		public Space[] GetSpaces()
		{
			Space[] board = new Space[40];
			board[0] = new Mechanic("Start");
			board[1] = new Regular("Mediterranean Avenue", 60, Colours.Brown);
			board[2] = new Lottery("Community Chest", TypeOfLottery.CommunityChest);
			board[3] = new Regular("Baltic Avenue", 60, Colours.Brown);
			board[4] = new Mechanic("Income Tax");
			board[5] = new Railway("Reading Railroad", 200);
			board[6] = new Regular("Oriental Avenue", 100, Colours.LightBlue);
			board[7] = new Lottery("Chance", TypeOfLottery.Chance);
			board[8] = new Regular("Vermont Avenue", 100, Colours.LightBlue);
			board[9] = new Regular("Connecticut Avenue", 120, Colours.LightBlue);
			board[10] = new Mechanic("Jail");
			board[11] = new Regular("St. Charles Place", 140, Colours.Purple);
			board[12] = new Utility("Electric Company", 150);
			board[13] = new Regular("States Avenue", 140, Colours.Purple);
			board[14] = new Regular("Virginia Avenue", 160, Colours.Purple);
			board[15] = new Railway("Pennsylvanla Railroad", 200);
			board[16] = new Regular("St. James Place", 180, Colours.Orange);
			board[17] = new Lottery("Community Chest", TypeOfLottery.CommunityChest);
			board[18] = new Regular("Tennessee Avenue", 180, Colours.Orange);
			board[19] = new Regular("New York Avenue", 200, Colours.Orange);
			board[20] = new Mechanic("Free Parking");
			board[21] = new Regular("Kentucky Avenue", 220, Colours.Red);
			board[22] = new Lottery("Chance", TypeOfLottery.Chance);
			board[23] = new Regular("Indiana Avenue", 220, Colours.Red);
			board[24] = new Regular("Illinois Avenue", 240, Colours.Red);
			board[25] = new Railway("B. & O. Railroad", 200);
			board[26] = new Regular("Atlantic Avenue", 260, Colours.Yellow);
			board[27] = new Regular("Ventnor Avenue", 260, Colours.Yellow);
			board[28] = new Utility("Water Work", 150);
			board[29] = new Regular("Marvin Gardens", 280, Colours.Yellow);
			board[30] = new Mechanic("Go To Jail");
			board[31] = new Regular("Pacific Avenue", 300, Colours.Green);
			board[32] = new Regular("North Carolina Avenue", 300, Colours.Green);
			board[33] = new Lottery("Community Chest", TypeOfLottery.CommunityChest);
			board[34] = new Regular("North Carolina Avenue", 320, Colours.Green);
			board[35] = new Railway("Short Line", 200);
			board[36] = new Lottery("Chance", TypeOfLottery.Chance);
			board[37] = new Regular("Park Place", 350, Colours.DarkBlue);
			board[38] = new Mechanic("Luxury Tax");
			board[39] = new Regular("BoardWork", 400, Colours.DarkBlue);
			return board;
		}
	}
}