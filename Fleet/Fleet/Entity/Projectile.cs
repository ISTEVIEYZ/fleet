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

		public Projectile(string filePath) : base(filePath) { }

		public Projectile(string filePath, Vector2 playerPosition) : base(filePath)
		{
			var mouseState = Mouse.GetState();

			position = playerPosition;
			_mousePosition = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
		}

		public void UpdateMousePosition()
		{
			var mouseState = Mouse.GetState();
			_mousePosition = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
		}

		public override void Update(GameTime gameTime)
		{
			// Vector2 movement = _mousePosition - position;

			// movement.Normalize();
			// position += movement * 10f;

			rotation = (float)Math.Atan2(_mousePosition.Y, _mousePosition.X);

			velocity.X = (float)Math.Cos(rotation) * 1000f;
			velocity.Y = (float)Math.Sin(rotation) * 1000f;

			position.X = position.X + velocity.X * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
			position.Y = position.Y + velocity.Y * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(Texture, position, null, color, rotation, origin, 2, SpriteEffects.None, 1);
		}
	}
}
