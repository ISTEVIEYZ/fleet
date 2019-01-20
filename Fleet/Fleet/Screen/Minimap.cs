using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;
using Fleet.Entities.Base;
using static Fleet.Entities.Base.Entity;

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
		private Vector2 _origin;
		private Texture2D _backgroundTexture;
		private Vector2 _position = new Vector2(1150, 620);
		private Rectangle _entityDetination = new Rectangle();
		private List<Entity> _entityList;

		private readonly Dictionary<EntityType, Color> _entityColors = new Dictionary<EntityType, Color>()
		{
			{ EntityType.NONE, new Color(255, 0, 0) },
			{ EntityType.PLAYER, new Color(255, 255, 0) },
			{ EntityType.COMPUTER, new Color(0, 0, 255) }
		};

		public Minimap(string spriteName)
		{
			_backgroundTexture = ResourceManager.Instance.GetTexture(spriteName);
			_origin = new Vector2(_backgroundTexture.Width / 2, _backgroundTexture.Height / 2);
		}

		public void Update(GameTime gameTime, List<Entity> entityList)
		{
			_entityList = entityList;
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

					_tempX = _entityList[i].position.X / _sectorWidth * _backgroundTexture.Width / 2 * _scale;
					_tempY = _entityList[i].position.Y / _sectorHeight * _backgroundTexture.Height / 2 * _scale;

					_entityDetination.Width = 20;
					_entityDetination.Height = 20;
					_entityDetination.X = (int)(_position.X + _tempX);
					_entityDetination.Y = (int)(_position.Y + _tempY);

					spriteBatch.Draw(_entityList[i].Texture, _entityDetination, null, _entityColors[_entityList[i].entityType], _entityList[i].rotation, _entityList[i].origin, SpriteEffects.None, 1);
				}
			}
		}
	}
}
