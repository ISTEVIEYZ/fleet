using System;
using System.Collections.Generic;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Player : Entity
	{
		public List<Projectile> projectileList = new List<Projectile>();

		public Player(Texture2D texture) : base(texture)
		{
			// this.Acceleration = 0.2f;
			this.TurnAcceleration = 0.1f;
		}

		public override void Update(GameTime gameTime)
		{
			var currentKBState = Keyboard.GetState();
			var currentMouseState = Mouse.GetState();

			// Accelerate
			if (currentKBState.IsKeyDown(Keys.W))
			{
				Acceleration.X = (float)Math.Cos(Rotation);
				Acceleration.Y = (float)Math.Sin(Rotation);
			}
			else if (gameTime.ElapsedGameTime.Milliseconds > 0)
			{
				Acceleration.X = -1f * (Velocity.X / (gameTime.ElapsedGameTime.Milliseconds / 1000.0f));
				Acceleration.Y = -1f * (Velocity.Y / (gameTime.ElapsedGameTime.Milliseconds / 1000.0f));
			}

			if (currentKBState.IsKeyDown(Keys.S))
			{
				Acceleration.X = (float)Math.Cos(Rotation) * -1f;
				Acceleration.Y = (float)Math.Sin(Rotation) * -1f;
			}

			// Rotate
			if (currentKBState.IsKeyDown(Keys.A))
			{
				Rotation -= 0.2f;
			}
			if (currentKBState.IsKeyDown(Keys.D))
			{
				Rotation += 0.2f;
			}

			if (currentKBState.IsKeyDown(Keys.Space))
			{
				this.Acceleration = Vector2.Zero;
				this.Velocity = Vector2.Zero;
			}
			
			while (this.Rotation > Math.PI * 2.0f)
			{
				this.Rotation -= (float)Math.PI * 2.0f;
			}

			while (this.Rotation < Math.PI * 2.0f * -1.0f)
			{
				this.Rotation += (float)Math.PI * 2.0f;
			}

			this.Acceleration.X = MathHelper.Clamp(this.Acceleration.X, -0.5f, 0.5f);
			this.Acceleration.Y = MathHelper.Clamp(this.Acceleration.Y, -0.5f, 0.5f);

			this.Velocity.X = this.Velocity.X + this.Acceleration.X * (gameTime.ElapsedGameTime.Milliseconds);
			this.Velocity.Y = this.Velocity.Y + this.Acceleration.Y * (gameTime.ElapsedGameTime.Milliseconds);

			Position.X = Position.X + Velocity.X * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
			Position.Y = Position.Y + Velocity.Y * (gameTime.ElapsedGameTime.Milliseconds / 1000f);

			if (currentMouseState.LeftButton == ButtonState.Pressed)
			{
				projectileList.Add(new Projectile(GameManager.Instance.content.Load<Texture2D>("ship2"), this.Position));
			}

			for (int i = 0; i < projectileList.Count; i++)
			{
				projectileList[i].Update(gameTime);
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(this.Texture, Position, null, Color.White, this.Rotation, new Vector2(Texture.Width/2.0f, Texture.Height/2.0f), 1, SpriteEffects.None, 1);

			for (int i = 0; i < projectileList.Count; i++)
			{
				projectileList[i].Draw(spriteBatch, gameTime);
			}
		}
	}
}
