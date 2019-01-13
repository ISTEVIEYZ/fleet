using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;
using Fleet.Entity;

namespace Fleet.Screen
{
	public class Minimap
	{
		private bool _enabled = true;
		private float _scale = 0.4f;
		private float _sectorWidth = 10000;
		private float _sectorHeight = 10000;
		private float _tempX = 0f;
		private float _tempY = 0f;
		private string _colorKey;
		private Vector2 _playerPosition;
		private Vector2 _origin;
		private Texture2D _backgroundTexture;
		private Vector2 _position = new Vector2(1150, 620);
		private Rectangle _entityDetination = new Rectangle();
		private List<Entity.Entity> _entityList;

		private Dictionary<string, Color> _entityColors = new Dictionary<string, Color>()
		{
			{ "default", new Color(255, 0, 0) },
			{ "player", new Color(255, 255, 0) },
			{ "enemy", new Color(0, 0, 255) }
		};

		public Minimap(string spriteName)
		{
			_backgroundTexture = ResourceManager.Instance.GetTexture(spriteName);
			_origin = new Vector2(_backgroundTexture.Width / 2, _backgroundTexture.Height / 2);
		}

		public void Update(GameTime gameTime, List<Entity.Entity> entityList)
		{
			this._entityList = entityList;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (_enabled)
			{
				spriteBatch.Draw(_backgroundTexture, _position, null, Color.White, 0, _origin, _scale, SpriteEffects.None, 0);

				for (int i = 0; i < _entityList.Count; i++)
				{
					if (_entityList[i] is Projectile)
						return;

					_colorKey = "default";

					if (_entityList[i] is Player)
					{
						_playerPosition = _entityList[i].position;
						_colorKey = "player";
					}

					if (_entityList[i] is Enemy)
					{
						_colorKey = "enemy";
					}

					_tempX = _entityList[i].position.X / _sectorWidth * _backgroundTexture.Width / 2 * _scale;
					_tempY = _entityList[i].position.Y / _sectorHeight * _backgroundTexture.Height / 2 * _scale;

					_entityDetination.Width = 20;
					_entityDetination.Height = 20;
					_entityDetination.X = (int)(_position.X + _tempX);
					_entityDetination.Y = (int)(_position.Y + _tempY);

					spriteBatch.Draw(_entityList[i].Texture, _entityDetination, null, _entityColors[_colorKey], _entityList[i].rotation, _entityList[i].origin, SpriteEffects.None, 1);
				}
			}
		}
	}
}
