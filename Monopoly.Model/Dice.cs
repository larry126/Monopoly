using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public class Dice : IDice
	{
		private static Random dice = new Random();
		private GameState _gameState;

		public Dice(GameState gameState)
		{
			_gameState = gameState;
		}

		public int Roll(int noOfDice = 2)
		{
			_gameState.LastRoll = noOfDice == 1 ? dice.Next(1, 7) : (dice.Next(1, 7) + Roll(noOfDice - 1));
			return _gameState.LastRoll;
		}
	}
}
