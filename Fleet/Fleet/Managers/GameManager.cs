using Fleet.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Managers
{
	public sealed class GameManager
	{
		private static readonly GameManager instance = new GameManager();

		public List<Entity.Entity> entityList = new List<Entity.Entity>();
		public Player activePlayer;

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static GameManager() { }

		private GameManager() {}

		public static GameManager Instance
		{
			get
			{
				return instance;
			}
		}
	}
}
