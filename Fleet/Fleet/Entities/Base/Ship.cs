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

		protected Vector2 playerPosition = Vector2.Zero;

		private BarComponent _barComponent;
		private Vector2 _barOffset;

		public Ship(string spriteName, EntityType type, Vector2? playerPosition = null) : base(spriteName, type)
		{
			this.playerPosition = playerPosition.GetValueOrDefault(Vector2.Zero);

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

			_barComponent = new BarComponent(position, new Vector2(200, 50), 100);
			_barOffset = new Vector2(0, 200);
		}

		public void DrawBoundingBox(SpriteBatch spriteBatch)
		{
			if (drawBoundingBox)
				collisionComponent.DrawBoundingBox(spriteBatch);
		}

		public abstract override void CheckCollision(Entity other);

		public override void Update(GameTime gameTime)
		{
			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			if (currentKeyboardState.IsKeyDown(Keys.F1) && previousKeyboardState.IsKeyUp(Keys.F1))
			{
				drawBoundingBox = !drawBoundingBox;
			}

			_barComponent.Update(100, position + _barOffset);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			_barComponent.Draw(spriteBatch);
			System.Console.Out.WriteLine("meme");
		}
	}
}
