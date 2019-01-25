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

		public void AddTexture(string name, Texture2D texture)
		{
			if (!_textures.ContainsKey(name))
				_textures.Add(name, texture);
		}

		public Texture2D GetTexture(string name)
		{
			if (_textures.ContainsKey(name))
				return _textures[name];

			return null;
		}

		public Texture2D GetTextureSprite(string name)
		{
			if (IsContentManagerSet())
			{
				// Add texture if it doesn't exist
				if (!_textures.ContainsKey(name))
					_textures.Add(name, _content.Load<Texture2D>(name));

				return _textures[name];
			}

			return null;
		}

		public SpriteFont GetFont(string name)
		{
			if (IsContentManagerSet())
			{
				// Add font if it doesn't exist
				if (!_fonts.ContainsKey(name))
					_fonts.Add(name, _content.Load<SpriteFont>(name));

				return _fonts[name];
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
