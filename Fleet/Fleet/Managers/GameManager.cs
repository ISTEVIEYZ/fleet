using Fleet.Entity;
using Fleet.Screen;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Managers
{
	public sealed class GameManager
	{
		private static readonly GameManager instance = new GameManager();

		public Camera camera;
		public Player player;
		public Minimap minimap;
		public GraphicsDevice graphicsDevice;

		public List<Entity.Entity> Entities { get; } = new List<Entity.Entity>();

		// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
		static GameManager() { }

		private GameManager() { }

		public static GameManager Instance { get { return instance; } }
	}
}
