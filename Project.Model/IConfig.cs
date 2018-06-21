using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
	public interface ISpaces
	{
		Space[] Spaces();
	}
	public interface IRules
	{
		bool AuctionIfNotBuied { get; set; }
	}
}
