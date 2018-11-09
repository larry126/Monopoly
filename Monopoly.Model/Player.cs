using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model
{
	public abstract class Player
	{
		public string Name { get; protected set; }
		public int Money { get; protected set; }
		protected int LocSeq;
		public bool Rolled { protected get; set; }

		public GameState GameState { set; get; }
		public bool Bankruptcy { get; protected set; }
		public bool InJail { get; set; }

		public List<Property> OwnedProperties { get => GameState.CurrentBoard.OfType<Property>().Where(p => p.IsOwner(this)).ToList(); }

		public Player(string name, int money)
		{
			Name = name;
			Money = money;
		}

		private HashSet<Property> regularProperties { get { return new HashSet<Property>(from r in OwnedProperties.OfType<Regular>() select r); } }
		private Dictionary<Colours, int> ownedPropertiesCountInGroups
		{
			get
			{
				Dictionary<Colours, int> _ownedPropertiesCountInGroups = new Dictionary<Colours, int> { };
				foreach (Regular rp in regularProperties)
				{
					if (_ownedPropertiesCountInGroups.ContainsKey(rp.Group))
					{
						_ownedPropertiesCountInGroups[rp.Group]++;
					}
					else
					{
						_ownedPropertiesCountInGroups.Add(rp.Group, 1);
					}
				}
				return _ownedPropertiesCountInGroups;
			}
		}

		public int GetTotalMoney()
		{
			int value = Money;

			foreach (Property prop in OwnedProperties.FindAll(p => p.MortgageState == false))
			{
				if (regularProperties.Contains(prop))
				{
					value = ((Regular)prop).HouseCount * ((Regular)prop).HousePrice / 2;
				}
				value = value + prop.Price / 2;
			}
			return value;
		}

		public int GetTotalAssests()
		{
			int value = Money;

			foreach (Property prop in OwnedProperties)
			{
				if (regularProperties.Contains(prop))
				{
					value = ((Regular)prop).HouseCount * ((Regular)prop).HousePrice;
				}
				value = value + (prop.MortgageState == false ? prop.Price : prop.MortgageState == true ? prop.Price / 2 : 0);
			}

			return value;
		}

		public Space GetLocationSpace()
		{
			return GameState.CurrentBoard[LocSeq];
		}

		public virtual int RollDice(IDice dice, int noOfDice = 2)
		{
			return dice.Roll(noOfDice);
		}

		public virtual void MoveTo(int destination, bool passGO = true, bool GoToJail = false)
		{
			LocSeq = destination;
			if (GoToJail)
			{
				InJail = true;
			}
		}

		public virtual void MoveBy(int squares)
		{
			int previousLocation = LocSeq;
			LocSeq = (LocSeq + squares) % (GameState.CurrentBoard.Count() + 1);
			if (LocSeq < previousLocation && squares > 0)
			{
				Money = Money + 200;
			}
		}

		public void GainMoney(int value)
		{
			Money = Money + value;
		}

		public void PayMoney(int value)
		{
			Money = Money - value;
		}

		public void BuildHouses(Colours colour)
		{
			foreach (Regular r in GameState.PropertiesInGroups[colour])
			{
				r.BuildHouses(1);
			}
		}

		public bool CanBuild()
		{
			foreach (KeyValuePair<Colours, int> pair in ownedPropertiesCountInGroups)
			{
				List<Regular> regularGroup = GameState.PropertiesInGroups[pair.Key];
				if (regularGroup.Count() == pair.Value)
				{
					if (IsAbleToAfford(regularGroup.Sum(rp => rp.HousePrice)) && regularGroup[0].HouseCount < 5)
					{
						return true;
					}
				}
			}
			return false;
		}

		public List<Colours> GetBuildableGroups()
		{
			List<Colours> buildableGroups = new List<Colours> { };
			foreach (KeyValuePair<Colours, int> pair in ownedPropertiesCountInGroups)
			{
				List<Regular> regularGroup = GameState.PropertiesInGroups[pair.Key];
				if (regularGroup.Count() == pair.Value)
				{
					if (IsAbleToAfford(regularGroup.Sum(rp => rp.HousePrice)) && regularGroup[0].HouseCount < 5)
					{
						buildableGroups.Add(pair.Key);
					}
				}
			}
			return buildableGroups;
		}

		public void SellHouses(Colours colour)
		{
			foreach (Regular r in GameState.PropertiesInGroups[colour])
			{
				r.SellHouses(1);
			}
		}

		public List<Colours> GetHousesSellableGroups()
		{
			List<Colours> houseSellableGroups = new List<Colours> { };
			foreach (KeyValuePair<Colours, int> pair in ownedPropertiesCountInGroups)
			{
				List<Regular> regularGroup = GameState.PropertiesInGroups[pair.Key];
				if (regularGroup[0].HouseCount > 0)
				{
					houseSellableGroups.Add(pair.Key);
				}
			}
			return houseSellableGroups;
		}

		public List<Regular> GetHousesSellableProperties()
		{
			List<Regular> houseSellableProperties = new List<Regular> { };
			foreach (Regular r in regularProperties)
			{
				if (r.HouseCount > 0)
				{
					houseSellableProperties.Add(r);
				}
			}
			return houseSellableProperties;
		}

		public List<Property> GetLiftableMortgagedProperties()
		{
			List<Property> liftableMortgagedProperties = new List<Property> { };
			foreach (Property prop in OwnedProperties)
			{
				if (prop.MortgageState == true && this.IsAbleToAfford(((int)(prop.Price / 2 * 1.1))))
				{
					liftableMortgagedProperties.Add(prop);
				}
			}
			return liftableMortgagedProperties;
		}

		public abstract int SellAndPayDecision();

		public bool CanSellHouses()
		{
			return GetHousesSellableProperties().Count > 0;
		}

		public bool CanMortgageProperties()
		{
			return OwnedProperties.Exists(p => p.CanBeMortgaged() == true);
		}

		public bool CanLiftMortgagedProperties()
		{
			return GetLiftableMortgagedProperties().Count > 0;
		}

		public bool IsAbleToAfford(int cost)
		{
			return GetTotalMoney() > cost;
		}

		public bool IsAbleToPay(int cost)
		{
			return Money > cost;
		}

		public bool IsAbleToRoll()
		{
			return !Rolled;
		}

		public void Bankrupt(Player creditor = null)
		{
			Bankruptcy = true;
			foreach (Property prop in OwnedProperties)
			{
				prop.ChangeOwner(creditor);
			}
		}

		//Decision Maker
		public abstract LandOnActions LandOnDecision(List<LandOnActions> actions);
		public abstract int MainPhaseDecision();
		public abstract int IncomeTaxDecision();
		public abstract Colours? BuildHousesDecision();
		public abstract Colours? SellHousesDecision();
		public abstract Property MortgagePropertyDecision();
		public abstract Property LiftMortgagedPropertyDecision();
		public abstract bool LiftMortgageDecision(Property prop);
		public abstract int BetweenTurnsDecision();
		public abstract void sellAndPay(Space space,LandOnActions action);
	}
}