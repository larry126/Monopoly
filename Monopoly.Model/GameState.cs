using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Monopoly.Model
{
	public class GameState
	{
		private Player[] players;

		private int _realTurn = 0;
		public int RealTurn { get => _realTurn; set => _realTurn = value; }
		private int _lastRoll;
		public int LastRoll { get => _lastRoll; set => _lastRoll = value; }

		public Dictionary<Colours, List<Regular>> PropertiesInGroups { get; private set; }
		public Space[] CurrentBoard { get; private set; }

		public GameState(Player[] participatePlayers, Space[] board = null)
		{
			players = participatePlayers;
			foreach (Player player in players)
			{
				player.GameState = this;
			}
			if (board != null)
			{
				CurrentBoard = board;
			}
			else
			{
				CurrentBoard = GetDefaultBoard();
			}
			Regular[] regularProperties = CurrentBoard.OfType<Regular>().ToArray();
			PropertiesInGroups = new Dictionary<Colours, List<Regular>> { };
			foreach (Regular rp in regularProperties)
			{
				if (PropertiesInGroups.ContainsKey(rp.Group))
				{
					PropertiesInGroups[rp.Group].Add(rp);
				}
				else
				{
					PropertiesInGroups.Add(rp.Group, new List<Regular> { rp });
				}
			}
		}

		public Player GetTurnPlayer()
		{
			return players[_realTurn % (players.Length)];
		}

		public void RollDiceStart(IDice dice)
		{
			Player turnPlayer = GetTurnPlayer();
			turnPlayer.RollDice(dice);
			if (!turnPlayer.InJail)
			{
				GetTurnPlayer().MoveBy(LastRoll);
			}
			else
			{
				if (LastRoll % 2 == 0)
				{
					GetTurnPlayer().MoveBy(LastRoll);
				}
			}
		}

		public int MainPhase(IDice dice)
		{
			Player currentPlayer = GetTurnPlayer();
			int moveMade = currentPlayer.MainPhaseDecision();
			if (moveMade == 1 && currentPlayer.IsAbleToRoll())
			{
				//				RollDiceProcedure(currentPlayer,dice);
				currentPlayer.Rolled = true;
				return 1;
			}
			else if (moveMade == 1)
			{
				return 2;
			}
			else if (moveMade == 2)
			{
			}
			else if (moveMade == 3)
			{
				Colours? group = currentPlayer.BuildHousesDecision();
				if (group != null)
				{
					currentPlayer.BuildHouses((Colours)group);
				}
			}
			else if (moveMade == 4)
			{
				Colours? group = currentPlayer.SellHousesDecision();
				if (group != null)
				{
					currentPlayer.SellHouses((Colours)group);
				}
			}
			else if (moveMade == 5)
			{
				Property prop = currentPlayer.MortgagePropertyDecision();
				if (prop != null)
				{
					prop.Mortgage();
				}
			}
			else if (moveMade == 6)
			{
				Property prop = currentPlayer.LiftMortgagedPropertyDecision();
				if (prop != null)
				{
					prop.LiftMortgage();
				}
			}
			return 0;
		}

		public void RollDiceProcedure(Player currentPlayer, IDice dice)
		{
			RollDiceStart(dice);
			Space currentPlayerSpace = currentPlayer.GetLocationSpace();
			List<LandOnActions> availableActions = currentPlayerSpace.GetLandOnActions(currentPlayer);
			if (availableActions.Exists(at => (at == LandOnActions.Rent || at == LandOnActions.Tax) && !currentPlayerSpace.CanPlayerPerformAction(currentPlayer, at)))
			{
				currentPlayer.Bankrupt();
				return;
			}
			List<LandOnActions> performableActions = availableActions.Where(at => currentPlayerSpace.CanPlayerPerformAction(currentPlayer, at)).ToList();
			LandOnActions action = currentPlayer.LandOnDecision(performableActions);
			currentPlayer.sellAndPay(currentPlayerSpace, action);
			currentPlayerSpace.PerformAction(currentPlayer, action);
		}

		public bool gameEnd()
		{
			return players.Count(p => !p.Bankruptcy) == 1;
		}

		private Space[] GetDefaultBoard()
		{
			Space[] board = new Space[40];
			board[0] = new NoEffectSpace(this, "Start");
			board[1] = new Regular(this, "Mediterranean Avenue", 60, Colours.Brown, 2, 10, 30, 90, 160, 250);
			board[2] = new Lottery(this, "Community Chest", TypeOfLottery.CommunityChest);
			board[3] = new Regular(this, "Baltic Avenue", 60, Colours.Brown, 4, 20, 60, 180, 320, 450);
			board[4] = new Tax(this, "Income Tax", TypeOfTax.IncomeTax, 100);
			board[5] = new Railway(this, "Reading Railroad", 200);
			board[6] = new Regular(this, "Oriental Avenue", 100, Colours.LightBlue, 6, 30, 90, 270, 400, 550);
			board[7] = new Lottery(this, "Chance", TypeOfLottery.Chance);
			board[8] = new Regular(this, "Vermont Avenue", 100, Colours.LightBlue, 6, 30, 90, 270, 400, 550);
			board[9] = new Regular(this, "Connecticut Avenue", 120, Colours.LightBlue, 8, 40, 100, 300, 450, 600);
			board[10] = new NoEffectSpace(this, "Jail");
			board[11] = new Regular(this, "St. Charles Place", 140, Colours.Purple, 10, 50, 150, 450, 625, 750);
			board[12] = new Utility(this, "Electric Company", 150);
			board[13] = new Regular(this, "States Avenue", 140, Colours.Purple, 10, 50, 150, 450, 625, 750);
			board[14] = new Regular(this, "Virginia Avenue", 160, Colours.Purple, 12, 60, 180, 500, 700, 900);
			board[15] = new Railway(this, "Pennsylvanla Railroad", 200);
			board[16] = new Regular(this, "St. James Place", 180, Colours.Orange, 14, 70, 200, 550, 750, 950);
			board[17] = new Lottery(this, "Community Chest", TypeOfLottery.CommunityChest);
			board[18] = new Regular(this, "Tennessee Avenue", 180, Colours.Orange, 14, 70, 200, 550, 750, 950);
			board[19] = new Regular(this, "New York Avenue", 200, Colours.Orange, 16, 80, 220, 600, 800, 1000);
			board[20] = new NoEffectSpace(this, "Free Parking");
			board[21] = new Regular(this, "Kentucky Avenue", 220, Colours.Red, 18, 90, 250, 700, 875, 1050);
			board[22] = new Lottery(this, "Chance", TypeOfLottery.Chance);
			board[23] = new Regular(this, "Indiana Avenue", 220, Colours.Red, 18, 90, 250, 700, 875, 1050);
			board[24] = new Regular(this, "Illinois Avenue", 240, Colours.Red, 20, 100, 300, 750, 925, 1100);
			board[25] = new Railway(this, "B. & O. Railroad", 200);
			board[26] = new Regular(this, "Atlantic Avenue", 260, Colours.Yellow, 22, 110, 330, 800, 975, 1150);
			board[27] = new Regular(this, "Ventnor Avenue", 260, Colours.Yellow, 22, 110, 330, 800, 975, 1150);
			board[28] = new Utility(this, "Water Work", 150);
			board[29] = new Regular(this, "Marvin Gardens", 280, Colours.Yellow, 24, 120, 360, 850, 1025, 1200);
			board[30] = new GoToJail(this, "Go To Jail");
			board[31] = new Regular(this, "Pacific Avenue", 300, Colours.Green, 26, 130, 390, 900, 1100, 1275);
			board[32] = new Regular(this, "North Carolina Avenue", 300, Colours.Green, 26, 130, 390, 900, 1100, 1275);
			board[33] = new Lottery(this, "Community Chest", TypeOfLottery.CommunityChest);
			board[34] = new Regular(this, "North Carolina Avenue", 320, Colours.Green, 28, 150, 450, 1000, 1200, 1400);
			board[35] = new Railway(this, "Short Line", 200);
			board[36] = new Lottery(this, "Chance", TypeOfLottery.Chance);
			board[37] = new Regular(this, "Park Place", 350, Colours.DarkBlue, 35, 175, 500, 1100, 1300, 1500);
			board[38] = new Tax(this, "Luxury Tax", TypeOfTax.LuxuryTax, 100);
			board[39] = new Regular(this, "BoardWork", 400, Colours.DarkBlue, 50, 200, 600, 1400, 1700, 2000);
			return board;
		}

		public void BetweenTurns()
		{
			foreach (Player p in players.SkipWhile(p => p.Bankruptcy == true || p == GetTurnPlayer()))
			{
				while (true)
				{
					int moveMade = p.BetweenTurnsDecision();
					if (moveMade == 1)
					{
						break;
					}
					else if (moveMade == 2)
					{
					}
					else if (moveMade == 3)
					{
						Colours? group = p.BuildHousesDecision();
						if (group != null)
						{
							p.BuildHouses((Colours)group);
						}
					}
					else if (moveMade == 4)
					{
						Colours? group = p.SellHousesDecision();
						if (group != null)
						{
							p.SellHouses((Colours)group);
						}
					}
					else if (moveMade == 5)
					{
						Property prop = p.MortgagePropertyDecision();
						if (prop != null)
						{
							prop.Mortgage();
						}
					}
					else if (moveMade == 6)
					{
						Property prop = p.LiftMortgagedPropertyDecision();
						if (prop != null)
						{
							prop.LiftMortgage();
						}
					}
				}
			}
			RealTurn++;
			GetTurnPlayer().Rolled = false;
		}
	}
}