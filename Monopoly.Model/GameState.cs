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
		public Player[] AllPlayers { get; private set; }

		private int _realTurn = 0;
		public int RealTurn { get => _realTurn; set => _realTurn = value; }
		private int _lastRoll;
		public int LastRoll { get => _lastRoll; set => _lastRoll = value; }

		public Phase CurrentPhase { get; private set; }

		public Dictionary<Colours, List<Regular>> PropertiesInGroups { get; private set; }
		public Space[] CurrentBoard { get; private set; }

		public GameState(Player[] participatePlayers, Space[] board = null)
		{
			AllPlayers = participatePlayers;
			foreach (Player player in AllPlayers)
			{
				player.gameState = this;
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
			return AllPlayers[_realTurn % (AllPlayers.Length)];
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

		public bool gameEnd()
		{
			return AllPlayers.Count(p => !p.Bankruptcy) == 1;
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

		public void MainPhase(IDice dice)
		{
			CurrentPhase = Phase.MainPhase;
			Player currentPlayer = GetTurnPlayer();
			OpenGameStateActions moveMade = 0;
			while (moveMade != OpenGameStateActions.End || currentPlayer.IsAbleToRoll())
			{
				moveMade = currentPlayer.MainPhaseDecision();
				switch (moveMade)
				{
					case OpenGameStateActions.Roll:
						if (currentPlayer.IsAbleToRoll())
						{
							currentPlayer.Rolled = true;
							RollDiceProcedure(currentPlayer, dice);
						}
						break;
					case OpenGameStateActions.Trade:
						break;
					case OpenGameStateActions.Build:
						Colours? groupBuild = currentPlayer.BuildHousesDecision();
						if (groupBuild != null)
						{
							currentPlayer.BuildHouses((Colours)groupBuild);
						}
						break;
					case OpenGameStateActions.Sell:
						Colours? groupSell = currentPlayer.SellHousesDecision();
						if (groupSell != null)
						{
							currentPlayer.SellHouses((Colours)groupSell);
						}
						break;
					case OpenGameStateActions.Mortgage:
						Property propMortgage = currentPlayer.MortgagePropertyDecision();
						if (propMortgage != null)
						{
							propMortgage.Mortgage();
						}
						break;
					case OpenGameStateActions.LiftMortgage:
						Property propLift = currentPlayer.LiftMortgagedPropertyDecision();
						if (propLift != null)
						{
							propLift.LiftMortgage();
						}
						break;
					default:
						break;
				}
			}
		}

		public void RollDiceProcedure(Player currentPlayer, IDice dice)
		{
			CurrentPhase = Phase.DiceRolledPhase;
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
			if (action != LandOnActions.NoAction)
			{
				sellAndPay(currentPlayer, currentPlayerSpace, action);
				currentPlayerSpace.PerformAction(currentPlayer, action);
			}
		}

		public void sellAndPay(Player player, Space space, LandOnActions action)
		{
			while (!space.CanPlayerPerformAction(player, action))
			{
				OpenGameStateActions moveMade = player.SellAndPayDecision();
				switch (moveMade)
				{
					case OpenGameStateActions.Build:
						Colours? groupBuild = player.BuildHousesDecision();
						if (groupBuild != null)
						{
							player.BuildHouses((Colours)groupBuild);
						}
						break;
					case OpenGameStateActions.Sell:
						Colours? groupSell = player.SellHousesDecision();
						if (groupSell != null)
						{
							player.SellHouses((Colours)groupSell);
						}
						break;
					case OpenGameStateActions.Mortgage:
						Property propMortgage = player.MortgagePropertyDecision();
						if (propMortgage != null)
						{
							propMortgage.Mortgage();
						}
						break;
					case OpenGameStateActions.LiftMortgage:
						Property propLift = player.LiftMortgagedPropertyDecision();
						if (propLift != null)
						{
							propLift.LiftMortgage();
						}
						break;
					default:
						break;
				}
			}
		}

		public void BetweenTurns()
		{
			CurrentPhase = Phase.BetweenTurnsPhase;
			foreach (Player p in AllPlayers.Where(p => !p.Bankruptcy && p != GetTurnPlayer()))
			{
				OpenGameStateActions moveMade = 0;
				while (moveMade != OpenGameStateActions.End)
				{
					moveMade = p.BetweenTurnsDecision();
					switch (moveMade)
					{
						case OpenGameStateActions.End:
							break;
						case OpenGameStateActions.Trade:
							break;
						case OpenGameStateActions.Build:
							Colours? groupBuild = p.BuildHousesDecision();
							if (groupBuild != null)
							{
								p.BuildHouses((Colours)groupBuild);
							}
							break;
						case OpenGameStateActions.Sell:
							Colours? groupSell = p.SellHousesDecision();
							if (groupSell != null)
							{
								p.SellHouses((Colours)groupSell);
							}
							break;
						case OpenGameStateActions.Mortgage:
							Property propMortgage = p.MortgagePropertyDecision();
							if (propMortgage != null)
							{
								propMortgage.Mortgage();
							}
							break;
						case OpenGameStateActions.LiftMortgage:
							Property propLift = p.LiftMortgagedPropertyDecision();
							if (propLift != null)
							{
								propLift.LiftMortgage();
							}
							break;
						default:
							break;
					}
				}
			}
			RealTurn++;
			GetTurnPlayer().Rolled = false;
		}
	}
}