using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public abstract class Space
	{
		protected string _name;
		public string Name { get => _name; set => _name = value; }

		protected GameState _gameState;
		public GameState GameState { get => _gameState; set => _gameState = value; }

		public Space(GameState gameState, string name)
		{
			_gameState = gameState;
			_name = name;
		}

		public abstract List<LandOnActions> GetLandOnActions(Player player);

		public abstract bool CanPlayerPerformAction(Player player, LandOnActions action);

		public abstract void PerformAction(Player player, LandOnActions action);
	}
}
