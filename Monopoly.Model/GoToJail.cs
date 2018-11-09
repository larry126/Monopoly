using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class GoToJail : Space
	{

		public GoToJail(GameState gameState, string name) : base(gameState, name)
		{
			_gameState = gameState;
			_name = name;
		}

		public override List<LandOnActions> GetLandOnActions(Player player)
		{
			return new List<LandOnActions> { LandOnActions.GoToJail };
		}

		public override bool CanPlayerPerformAction(Player player, LandOnActions action)
		{
			if (action == LandOnActions.GoToJail)
			{
				return true;
			}
			return false;
		}

		public override void PerformAction(Player player, LandOnActions action)
		{
			if (action == LandOnActions.GoToJail)
			{
				player.MoveTo(_gameState.CurrentBoard.ToList().FindIndex(s => s.Name == "Jail"),false,true);
			}
		}
	}
}