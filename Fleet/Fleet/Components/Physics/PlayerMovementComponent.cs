using System;
using Fleet.Components.Physics.Interfaces;
using Fleet.Entities.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Components.Physics
{
	public class PlayerMovementComponent : IMovementComponent
	{
		private KeyboardState _currentKeyboardState;

		public PlayerMovementComponent() { }

		public void Move(GameTime gameTime, Entity entity)
		{
			_currentKeyboardState = Keyboard.GetState();

			var deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

			// Rotate
			if (_currentKeyboardState.IsKeyDown(Keys.A))
			{
				entity.rotation -= MathHelper.ToRadians(3f);
			}
			if (_currentKeyboardState.IsKeyDown(Keys.D))
			{
				entity.rotation += MathHelper.ToRadians(3f);
			}

			// Accelerate
			if (_currentKeyboardState.IsKeyDown(Keys.W))
			{
				entity.acceleration.X = (float)Math.Cos(entity.rotation);
				entity.acceleration.Y = (float)Math.Sin(entity.rotation);
			}
			else if (gameTime.ElapsedGameTime.Milliseconds > 0)
			{
				entity.acceleration.X = -1f * (entity.velocity.X / deltaTime);
				entity.acceleration.Y = -1f * (entity.velocity.Y / deltaTime);
			}

			if (_currentKeyboardState.IsKeyDown(Keys.S))
			{
				entity.acceleration.X = (float)Math.Cos(entity.rotation) * -1f;
				entity.acceleration.Y = (float)Math.Sin(entity.rotation) * -1f;
			}

			// Update values
			entity.acceleration.X = MathHelper.Clamp(entity.acceleration.X, -0.5f, 0.5f);
			entity.acceleration.Y = MathHelper.Clamp(entity.acceleration.Y, -0.5f, 0.5f);

			entity.velocity.X = entity.velocity.X + entity.acceleration.X * gameTime.ElapsedGameTime.Milliseconds;
			entity.velocity.Y = entity.velocity.Y + entity.acceleration.Y * gameTime.ElapsedGameTime.Milliseconds;

			entity.position.X = entity.position.X + entity.velocity.X * deltaTime;
			entity.position.Y = entity.position.Y + entity.velocity.Y * deltaTime;
		}
	}
}
