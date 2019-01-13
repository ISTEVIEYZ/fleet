using Fleet.Entities.Base;
using Fleet.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Entities.Ships
{
	public class Titan : Ship
	{
		public Titan(EntityType type, Vector2? playerPosition = null) : base(Sprites.SHIP_TITAN, type, playerPosition) { }

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
			DrawBoundingBox(spriteBatch);
			spriteBatch.Draw(Texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);
		}
	}
}
