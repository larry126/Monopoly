using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public abstract class Space
	{
		public string Name { get; protected set; }

		public Space(string name)
		{
			Name = name;
		}

		public abstract bool CanPlayerPerformLandOnProcedure(Player player);

		public abstract void PerformLandOnProcedure(Player player);
	}
}
