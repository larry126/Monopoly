using System;
using System.Collections.Generic;

namespace Monopoly.Model
{
	public class BotPlayer : Player
	{
		public BotPlayer(string name, int money) : base(name, money)
		{
			Name = name;
			Money = money;
		}

		public override Colours? BuildHousesDecision()
		{
			throw new NotImplementedException();
		}

		public override int IncomeTaxDecision()
		{
			throw new NotImplementedException();
		}

		public override Property LiftMortgagedPropertyDecision()
		{
			throw new NotImplementedException();
		}

		public override OpenGameStateActions MainPhaseDecision()
		{
			HashSet<OpenGameStateActions> availableActions = GetAvailableMoves();
			if (availableActions.Contains(OpenGameStateActions.Roll))
			{
				return OpenGameStateActions.Roll;
			}
			return OpenGameStateActions.End;
		}

		public override Property MortgagePropertyDecision()
		{
			throw new NotImplementedException();
		}

		public override Colours? SellHousesDecision()
		{
			throw new NotImplementedException();
		}

		public override OpenGameStateActions SellAndPayDecision()
		{
			return 0;
		}

		public override OpenGameStateActions BetweenTurnsDecision()
		{
			HashSet<OpenGameStateActions> availableActions = GetAvailableMoves();
			return OpenGameStateActions.End;
		}

		public override bool LiftMortgageDecision(Property prop)
		{
			throw new NotImplementedException();
		}

		public override LandOnActions LandOnDecision(List<LandOnActions> actions)
		{
			return actions[0];
		}
	}
}
