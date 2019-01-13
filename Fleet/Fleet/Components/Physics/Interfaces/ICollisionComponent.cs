using Fleet.Entities.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Components.Physics.Interfaces
{
	public interface ICollisionComponent
	{
		bool CollidesWith(Entity other);

		void DrawBoundingBox(SpriteBatch spriteBatch);
	}
}
