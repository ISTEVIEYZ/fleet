using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Fleet.Entity
{
	public class Projectile : Entity
	{
		private Vector2 mousePosition;

		public Projectile(Texture2D texture) : base(texture) { }

		public Projectile(Texture2D texture, Vector2 position) : base(texture)
		{
			var mouseState = Mouse.GetState();

			this.Position = position;
			this.mousePosition = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
		}

		public override void Update(GameTime gameTime)
		{
			//Vector2 movement = this.mousePosition - this.playerPosition;

			//movement.Normalize();
			//this.Position += movement * 10f;

			this.Rotation = (float)Math.Atan2(mousePosition.Y, mousePosition.X);

			Velocity.X = (float)Math.Cos(Rotation) * 1000f;
			Velocity.Y = (float)Math.Sin(Rotation) * 1000f;

			Position.X = Position.X + Velocity.X * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
			Position.Y = Position.Y + Velocity.Y * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(this.Texture, this.Position, null, Color.White, this.Rotation, new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f), 2, SpriteEffects.None, 1);
		}
	}
}
