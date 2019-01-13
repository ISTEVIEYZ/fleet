using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Managers;
using Fleet.Entities.Base;
using Fleet.Globals;
using Fleet.Entities.Projectiles;

namespace Fleet.Entities.Ships
{
	public class Crusader : Ship
	{
		public Crusader(EntityType type) : base(Sprites.SHIP_CRUSADER, type) { }

		public override void CheckCollision(Entity other)
		{
			collisionComponent.CollidesWith(other);
		}

		public override void Update(GameTime gameTime)
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			// Move ship
			movementComponent.Move(gameTime, this);

			// Create new projectile
			if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
			{
				GameManager.Instance.Entities.Add(new Bullet(position, this));
			}

			// Update base
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(Texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);

			// Draw base
			base.Draw(spriteBatch, gameTime);
		}
	}
}
