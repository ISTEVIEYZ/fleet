using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Managers
{
	public sealed class ResourceManager
	{
		private static readonly ResourceManager _instance = new ResourceManager();

		public static ResourceManager Instance { get { return _instance; } }

		private ContentManager _content;
		private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
		private Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();

		// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
		static ResourceManager() { }

		private ResourceManager() { }

		public void SetContentManager(ContentManager contentManager)
		{
			_content = contentManager;
		}

		private bool IsContentManagerSet()
		{
			return _content != null;
		}

		public Texture2D GetTexture(string filePath)
		{
			if (IsContentManagerSet())
			{
				// Check if texture already loaded
				if (_textures.ContainsKey(filePath))
					return _textures[filePath];

				// Add new texture and return it
				_textures.Add(filePath, _content.Load<Texture2D>(filePath));
				return _textures[filePath];
			}

			return null;
		}

		public SpriteFont GetFont(string filePath)
		{
			if (IsContentManagerSet())
			{
				// Check if texture already loaded
				if (_fonts.ContainsKey(filePath))
					return _fonts[filePath];

				// Add new texture and return it
				_fonts.Add(filePath, _content.Load<SpriteFont>(filePath));
				return _fonts[filePath];
			}

			return null;
		}

		public void UnloadTexture(string name)
		{
			if (_textures.ContainsKey(name))
				_textures.Remove(name);
		}

		public void UnloadFont(string name)
		{
			if (_fonts.ContainsKey(name))
				_fonts.Remove(name);
		}
	}
}
