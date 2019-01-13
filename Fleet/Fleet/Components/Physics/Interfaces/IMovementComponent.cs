using Fleet.Entities.Base;
using Microsoft.Xna.Framework;

namespace Fleet.Components.Physics.Interfaces
{
	public interface IMovementComponent
	{
		void Move(GameTime gameTime, Entity entity);
	}
}
