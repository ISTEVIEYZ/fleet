using System;
using Fleet.Components.Physics;
using Fleet.Components.Physics.Interfaces;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entities.Base
{
	public abstract class Projectile : Entity
	{
		protected ICollisionComponent collisionComponent;
		protected float _timer = 100f;
		protected Vector2 _targetLocation;
		protected Vector2 _targetAngle;

		public Projectile(string spriteName, Vector2 playerPosition, Entity projectileParent) : base(spriteName)
		{
			position = playerPosition;
			parent = projectileParent;

			Vector2 mouseState = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
			_targetLocation = GameManager.Instance.camera.ScreenToWorld(mouseState);
			_targetAngle = _targetLocation - position;
			rotation = (float)Math.Atan2(_targetAngle.Y, _targetAngle.X);
			velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

			collisionComponent = new PerPixelCollisionComponent(this);
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
		}

		public abstract override void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
