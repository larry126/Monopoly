using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class CheatDice : IDice
	{
		int value;
		private GameState _gameState;

		public CheatDice(GameState gameState, int v1)
		{
			_gameState = gameState;
			value = v1;
		}

		public int Roll(int times = 0)
		{
			_gameState.LastRoll = value;
			return _gameState.LastRoll;
		}
	}
}