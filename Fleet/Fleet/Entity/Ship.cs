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
			this.Acceleration = 0.2f;
            this.TurnAcceleration = 0.1f;
		}

		public override void Update(GameTime gameTime)
		{
			var currentKBState = Keyboard.GetState();

            Velocity = Velocity + Acceleration * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            


            if (currentKBState.IsKeyDown(Keys.W))
            {
                Acceleration += 0.2f;
            }
            else
                Acceleration -= 0.2f;

            if (currentKBState.IsKeyDown(Keys.S))
			{
                Acceleration -= 0.2f;
            }

			if (currentKBState.IsKeyDown(Keys.A))
			{
                Rotation += TurnAcceleration;
			}

			if (currentKBState.IsKeyDown(Keys.D))
			{
                Rotation -= TurnAcceleration;
            }

            

            Velocity.X = Velocity.X + Acceleration * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            
            Position.X = Position.X + Velocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            Position.Y = Position.Y + Velocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);

           
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
            spriteBatch.Draw(this.Texture, Position, null, Color.White, Rotation, new Vector2(Texture.Width/2.0f, Texture.Height/2.0f), 1, SpriteEffects.None, 1);

        }
    }
}
