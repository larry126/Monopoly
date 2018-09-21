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

		public abstract List<int> GetLandOnActions(Player player);

		public abstract bool[] CanPlayerPerformAction(Player player, int action);

		public abstract void PerformAction(Player player, int action);
	}
}
