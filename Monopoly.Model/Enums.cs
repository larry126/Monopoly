﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public enum Colours
	{
		Brown, LightBlue, Purple, Orange, Red, Yellow, Green, DarkBlue
	}

	public enum TypeOfLottery
	{
		CommunityChest, Chance
	}

	public enum TypeOfTax
	{
		IncomeTax, LuxuryTax
	}

	public enum LandOnActions
	{
		Buy, Rent, GoToJail, Tax, NoAction
	}

	public enum OpenGameStateActions
	{
		Roll,End,Trade,Build,Sell,Mortgage,LiftMortgage
	}

	public enum Phase
	{
		MainPhase,DiceRolledPhase,BetweenTurnsPhase
	}
}