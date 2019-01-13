using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Components.Physics.Interfaces;
using Fleet.Components.Physics;
using Microsoft.Xna.Framework.Input;
using Fleet.Components.Resources;

namespace Fleet.Entities.Base
{
	public abstract class Ship : Entity
	{
		protected IMovementComponent movementComponent;
		protected ICollisionComponent collisionComponent;

		private BarComponent _barComponent;

		public Ship(string spriteName, EntityType type) : base(spriteName, type)
		{
			switch (type)
			{
				case EntityType.PLAYER:
					movementComponent = new PlayerMovementComponent();
					break;

				case EntityType.COMPUTER:
					movementComponent = new AIMovementComponent();
					break;
			}

			collisionComponent = new PerPixelCollisionComponent(this);
			_barComponent = new BarComponent(position, new Vector2(-100, 200), new Vector2(200, 50), 100);
		}

		public abstract override void CheckCollision(Entity other);

		public override void Update(GameTime gameTime)
		{
			_barComponent.Update(100, position);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			_barComponent.Draw(spriteBatch);

			// Debug draws
			if (showBoundingBox)
			{
				collisionComponent.DrawBoundingBox(spriteBatch);
			}
		}
	}
}
