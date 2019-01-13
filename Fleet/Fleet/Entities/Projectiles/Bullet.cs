using Fleet.Entities.Base;
using Fleet.Globals;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Entities.Projectiles
{
	public class Bullet : Projectile
	{
		public Bullet(Vector2 playerPosition, Entity projectileParent) : base(Sprites.PROJECTILE_BULLET, playerPosition, projectileParent) { }

		public override void CheckCollision(Entity other)
		{
			if (collisionComponent.CollidesWith(other))
			{
				isActive = false;
				GameManager.Instance.Entities.Remove(this);
			}
		}

		public override void Update(GameTime gameTime)
		{
			// With a unit vector established we apply a speed.
			position += velocity * 50f;

			// Code to add - if projectile active for too long set isActive to false so garbage collector can delete it.
			_timer--;
			if (_timer < 0)
				isActive = false;

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
