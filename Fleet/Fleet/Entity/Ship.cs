using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Ship : Entity
	{

		public Ship(Texture2D texture) : base(texture)
		{
			// this.Acceleration = 0.2f;
			this.TurnAcceleration = 0.1f;
		}

		public override void Update(GameTime gameTime)
		{
			// TODO: Prevent rotation overflow

			var currentKBState = Keyboard.GetState();

			// Velocity = Velocity + Acceleration * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
			

			if (currentKBState.IsKeyDown(Keys.W))
			{
				Acceleration.X = (float)Math.Cos(Rotation);
				Acceleration.Y = (float)Math.Sin(Rotation);
			}

			if (currentKBState.IsKeyDown(Keys.S))
			{
				Acceleration.X = (float)Math.Cos(Rotation) * -1f;
				Acceleration.Y = (float)Math.Sin(Rotation) * -1f;
			}

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
		
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(this.Texture, Position, null, Color.White, this.Rotation, new Vector2(Texture.Width/2.0f, Texture.Height/2.0f), 1, SpriteEffects.None, 1);

		}
	}
}
