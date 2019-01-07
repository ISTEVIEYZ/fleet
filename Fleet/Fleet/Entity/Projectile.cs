using System;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Projectile : Entity
	{
		private Vector2 _mousePosition;
		private Vector2 _targetLocation;
		private Vector2 _playerPosition;

		public Projectile(string filePath) : base(filePath) { }

		public Projectile(string filePath, Vector2 playerPosition) : base(filePath)
		{
			var mouseState = Mouse.GetState();

			_playerPosition = playerPosition;
			_mousePosition = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
			_targetLocation = _mousePosition - _playerPosition;
		}

		public override void Update(GameTime gameTime)
		{
			_targetLocation.Normalize();
			position += _targetLocation * 100f;

			rotation = (float)Math.Atan2(_mousePosition.Y, _mousePosition.X);

			//velocity.X = (float)Math.Cos(rotation) * 1000f;
			//velocity.Y = (float)Math.Sin(rotation) * 1000f;

			//position.X = position.X + movement.X * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
			//position.Y = position.Y + movement.Y * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(Texture, position, null, color, rotation, origin, 2, SpriteEffects.None, 1);
		}
	}
}
