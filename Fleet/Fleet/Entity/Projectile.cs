using System;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Projectile : Entity
	{
		private float _timer = 100f;
		private Vector2 _targetLocation;
		private Vector2 _targetAngle;

		public Projectile(string filePath) : base(filePath) { }

		public Projectile(string filePath, Vector2 playerPosition, Entity projectileParent) : base(filePath)
		{
			position = playerPosition;
			parent = projectileParent;

			Vector2 mouseState = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
			_targetLocation = GameManager.Instance.camera.ScreenToWorld(mouseState);
			_targetAngle = _targetLocation - position;
			rotation = (float)Math.Atan2(_targetAngle.Y, _targetAngle.X);
			velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
		}

		public override void Update(GameTime gameTime)
		{
			// With a unit vector established we apply a speed.
			position += velocity * 50f; 

			// Code to add - if projectile active for too long set isActive to false so garbage collector can delete it.
			_timer--;
			if (_timer < 0)
				isActive = false;
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			//Texture2D box = new Texture2D(GameManager.Instance.graphicsDevice, Bounds.Width, Bounds.Height);
			//Color[] data = new Color[Bounds.Width * Bounds.Height];
			//for (int i = 0; i < data.Length; ++i)
			//	data[i] = Color.White;
			//box.SetData(data);
			//spriteBatch.Draw(box, position, null, Color.Gainsboro, rotation, origin, scale, SpriteEffects.None, 1);

			spriteBatch.Draw(Texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);
		}
	}
}
