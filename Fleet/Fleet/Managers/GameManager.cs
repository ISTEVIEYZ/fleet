using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Entities.Base;
using Fleet.Screen;

namespace Fleet.Managers
{
	public sealed class GameManager
	{
		private static readonly GameManager instance = new GameManager();

		public Camera camera;
		public Ship player;
		public Minimap minimap;
		public GraphicsDevice graphicsDevice;

		public List<Entity> Entities { get; } = new List<Entity>();

		// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
		static GameManager() { }

		private GameManager() { }

		public static GameManager Instance { get { return instance; } }
	}
}
