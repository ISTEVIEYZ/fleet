using Fleet.Entities.Base;
using Fleet.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Entities.Ships
{
	public class Titan : Ship
	{
		public Titan(EntityType type) : base(Sprites.SHIP_TITAN, type) { }

		public override void CheckCollision(Entity other)
		{
			collisionComponent.CollidesWith(other);
		}

		public override void Update(GameTime gameTime)
		{
			// Move ship
			movementComponent.Move(gameTime, this);

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
