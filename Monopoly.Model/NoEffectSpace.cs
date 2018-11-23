using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class NoEffectSpace : Space
	{
		public NoEffectSpace(GameState gameState, string name) : base(gameState, name)
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
			return new List<LandOnActions> { };
		}

		public override void PerformAction(Player player, LandOnActions action){}
	}
}