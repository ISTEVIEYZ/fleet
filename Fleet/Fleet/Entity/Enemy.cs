using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;
using System.Collections.Generic;

namespace Fleet.Entity
{
	public class Enemy : Entity
	{
		Vector2 playerPosition;
		
		public Enemy(string filePath) : base(filePath) { }

		public override void Update(GameTime gameTime)
		{
			foreach (Entity player in GameManager.Instance.Entities.OfType<Player>())
			{
				playerPosition = player.position;
				break;
			}
			TurnToFace(playerPosition);
			position = Vector2.Lerp(position, playerPosition, 0.001f);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			Texture2D box = new Texture2D(GameManager.Instance.graphicsDevice, Bounds.Width, Bounds.Height);
			Color[] data = new Color[Bounds.Width * Bounds.Height];
			for (int i = 0; i < data.Length; ++i)
				data[i] = Color.White;
			box.SetData(data);

			spriteBatch.Draw(box, position, null, Color.Red, rotation, origin, scale, SpriteEffects.None, 1);
			spriteBatch.Draw(this.Texture, position, null, color, (rotation), origin, scale, SpriteEffects.None, 1);
		}

		private void TurnToFace(Vector2 location)
		{
			Vector2 _targetDirection = location - position;
			float _targetRotation = (float)Math.Atan2(_targetDirection.Y, _targetDirection.X);

			if (rotation < _targetRotation) //The scaler here can be replaced by a "turnspeed" in the future
				rotation += 0.1f;
			else if (rotation > _targetRotation)
				rotation -= 0.1f;
		}
	}
}
