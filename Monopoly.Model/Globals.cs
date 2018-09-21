using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public static class Globals
	{
		public static int LastRoll { get; set; }

		public static Dictionary<int, string> Actions = new Dictionary<int, string> { { 0, "Buy properties" } , { 1, "Pay rent" } , { 2, "Pass start" }, { 3, "Go to jail" }, { 4, "Free parking" } };

		private static Space[] _defaultGameboard = new Space[40];

		public static Space[] DefaultBoard {
			get
			{
				return _defaultGameboard;
			}

			private set {
				Space[] board = new Space[40];
				board[0] = new Mechanic("Start");
				board[1] = new Regular("Mediterranean Avenue", 60, Colours.Brown, 2, 10, 30, 90, 160, 250);
				board[2] = new Lottery("Community Chest", TypeOfLottery.CommunityChest);
				board[3] = new Regular("Baltic Avenue", 60, Colours.Brown, 4, 20, 60, 180, 320, 450);
				board[4] = new Mechanic("Income Tax");
				board[5] = new Railway("Reading Railroad", 200);
				board[6] = new Regular("Oriental Avenue", 100, Colours.LightBlue, 6, 30, 90, 270, 400, 550);
				board[7] = new Lottery("Chance", TypeOfLottery.Chance);
				board[8] = new Regular("Vermont Avenue", 100, Colours.LightBlue, 6, 30, 90, 270, 400, 550);
				board[9] = new Regular("Connecticut Avenue", 120, Colours.LightBlue, 8, 40, 100, 300, 450, 600);
				board[10] = new Mechanic("Jail");
				board[11] = new Regular("St. Charles Place", 140, Colours.Purple, 10, 50, 150, 450, 625, 750);
				board[12] = new Utility("Electric Company", 150);
				board[13] = new Regular("States Avenue", 140, Colours.Purple, 10, 50, 150, 450, 625, 750);
				board[14] = new Regular("Virginia Avenue", 160, Colours.Purple, 12, 60, 180, 500, 700, 900);
				board[15] = new Railway("Pennsylvanla Railroad", 200);
				board[16] = new Regular("St. James Place", 180, Colours.Orange, 14, 70, 200, 550, 750, 950);
				board[17] = new Lottery("Community Chest", TypeOfLottery.CommunityChest);
				board[18] = new Regular("Tennessee Avenue", 180, Colours.Orange, 14, 70, 200, 550, 750, 950);
				board[19] = new Regular("New York Avenue", 200, Colours.Orange, 16, 80, 220, 600, 800, 1000);
				board[20] = new Mechanic("Free Parking");
				board[21] = new Regular("Kentucky Avenue", 220, Colours.Red, 18, 90, 250, 700, 875, 1050);
				board[22] = new Lottery("Chance", TypeOfLottery.Chance);
				board[23] = new Regular("Indiana Avenue", 220, Colours.Red, 18, 90, 250, 700, 875, 1050);
				board[24] = new Regular("Illinois Avenue", 240, Colours.Red, 20, 100, 300, 750, 925, 1100);
				board[25] = new Railway("B. & O. Railroad", 200);
				board[26] = new Regular("Atlantic Avenue", 260, Colours.Yellow, 22, 110, 330, 800, 975, 1150);
				board[27] = new Regular("Ventnor Avenue", 260, Colours.Yellow, 22, 110, 330, 800, 975, 1150);
				board[28] = new Utility("Water Work", 150);
				board[29] = new Regular("Marvin Gardens", 280, Colours.Yellow, 24, 120, 360, 850, 1025, 1200);
				board[30] = new Mechanic("Go To Jail");
				board[31] = new Regular("Pacific Avenue", 300, Colours.Green, 26, 130, 390, 900, 1100, 1275);
				board[32] = new Regular("North Carolina Avenue", 300, Colours.Green, 26, 130, 390, 900, 1100, 1275);
				board[33] = new Lottery("Community Chest", TypeOfLottery.CommunityChest);
				board[34] = new Regular("North Carolina Avenue", 320, Colours.Green, 28, 150, 450, 1000, 1200, 1400);
				board[35] = new Railway("Short Line", 200);
				board[36] = new Lottery("Chance", TypeOfLottery.Chance);
				board[37] = new Regular("Park Place", 350, Colours.DarkBlue, 35, 175, 500, 1100, 1300, 1500);
				board[38] = new Mechanic("Luxury Tax");
				board[39] = new Regular("BoardWork", 400, Colours.DarkBlue, 50, 200, 600, 1400, 1700, 2000);
				_defaultGameboard = board;
			}
		}
	}
}