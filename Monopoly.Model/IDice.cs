﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public interface IDice
	{
		int Roll(int noOfDice = 2);
	}
}
