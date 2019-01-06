using Fleet.Entity;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Fleet.Managers
{
	public sealed class GameManager
	{
		private static readonly GameManager instance = new GameManager();

		public List<Entity.Entity> entityList = new List<Entity.Entity>();
		public Player activePlayer;
		public ContentManager content;
		public Camera camera;

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static GameManager() { }

		private GameManager() { }

		public static GameManager Instance
		{
			get
			{
				return instance;
			}
		}
	}
}
