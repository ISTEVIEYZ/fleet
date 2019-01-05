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
		Vector2 position;
		float speed;

		public Ship(Texture2D texture) : base(texture)
		{
			this.speed = 0.5f;
		}

		public override void Update(GameTime gameTime)
		{
			var currentKBState = Keyboard.GetState();

			if (currentKBState.IsKeyDown(Keys.W))
			{
				this.position.Y -= this.speed * gameTime.ElapsedGameTime.Milliseconds;
			}

			if (currentKBState.IsKeyDown(Keys.S))
			{
				this.position.Y += this.speed * gameTime.ElapsedGameTime.Milliseconds;
			}

			if (currentKBState.IsKeyDown(Keys.A))
			{
				this.position.X -= this.speed * gameTime.ElapsedGameTime.Milliseconds;
			}

			if (currentKBState.IsKeyDown(Keys.D))
			{
				this.position.X += this.speed * gameTime.ElapsedGameTime.Milliseconds;
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(texture: this.Texture, position: this.position, color: Color.White);
		}
	}
}
