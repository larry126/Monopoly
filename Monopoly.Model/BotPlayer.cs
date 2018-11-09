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

		public override int MainPhaseDecision()
		{
			throw new NotImplementedException();
		}

		public override Property MortgagePropertyDecision()
		{
			throw new NotImplementedException();
		}

		public override Colours? SellHousesDecision()
		{
			throw new NotImplementedException();
		}



		public override int SellAndPayDecision()
		{
			throw new NotImplementedException();
		}

		public override int BetweenTurnsDecision()
		{
			throw new NotImplementedException();
		}

		public override bool LiftMortgageDecision(Property prop)
		{
			throw new NotImplementedException();
		}

		public override void sellAndPay(Space space, LandOnActions action)
		{
			throw new NotImplementedException();
		}

		public override LandOnActions LandOnDecision(List<LandOnActions> actions)
		{
			throw new NotImplementedException();
		}
	}
}
