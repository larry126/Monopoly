using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Lottery : Space
	{
		public Lottery(GameState gameState, string name, TypeOfLottery type) : base(gameState, name)
		{
			_gameState = gameState;
			_name = name;
		}

		public override bool CanPlayerPerformAction(Player player, LandOnActions action)
		{
			return false;
		}

		public override List<LandOnActions> GetLandOnActions(Player player)
		{
			return new List<LandOnActions> { LandOnActions.NoAction };
		}

		public override void PerformAction(Player player, LandOnActions action)
		{
			throw new NotImplementedException();
		}
	}
}