using System;
using Fleet.Managers;
using Fleet.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Player : Entity
	{
		private Projectile _projectile = new Projectile(Sprites.BULLET);

		public Player(string filePath) : base(filePath) { }

		public override void Update(GameTime gameTime)
		{
			currentKeyboardState = Keyboard.GetState();
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

			// Rotate
			if (currentKeyboardState.IsKeyDown(Keys.A))
			{
				rotation -= MathHelper.ToRadians(3f);
			}
			if (currentKeyboardState.IsKeyDown(Keys.D))
			{
				rotation += MathHelper.ToRadians(3f);
			}

			// Accelerate
			if (currentKeyboardState.IsKeyDown(Keys.W))
			{
				acceleration.X = (float)Math.Cos(rotation);
				acceleration.Y = (float)Math.Sin(rotation);
			}
			else if (gameTime.ElapsedGameTime.Milliseconds > 0)
			{
				acceleration.X = -1f * (velocity.X / deltaTime);
				acceleration.Y = -1f * (velocity.Y / deltaTime);
			}

			if (currentKeyboardState.IsKeyDown(Keys.S))
			{
				acceleration.X = (float)Math.Cos(rotation) * -1f;
				acceleration.Y = (float)Math.Sin(rotation) * -1f;
			}

			// Update values
			acceleration.X = MathHelper.Clamp(acceleration.X, -0.5f, 0.5f);
			acceleration.Y = MathHelper.Clamp(acceleration.Y, -0.5f, 0.5f);

			velocity.X = velocity.X + acceleration.X * gameTime.ElapsedGameTime.Milliseconds;
			velocity.Y = velocity.Y + acceleration.Y * gameTime.ElapsedGameTime.Milliseconds;

			position.X = position.X + velocity.X * deltaTime;
			position.Y = position.Y + velocity.Y * deltaTime;

			// Create new projectile
			if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
			{
				CreateProjectile();
			}
		}

		private void CreateProjectile()
		{
			GameManager.Instance.Entities.Add(new Projectile(Sprites.BULLET, position));
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(this.Texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);
		}
	}
}
