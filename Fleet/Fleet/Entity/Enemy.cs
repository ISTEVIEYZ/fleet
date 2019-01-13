using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;
using Fleet.Screen;

namespace Fleet.Entity
{
	public class Enemy : Entity
	{
		private Vector2 _playerPosition;
		private Vector2 _targetDirection;
		private float _targetRotation;
		private BarComponent barComponent;
		private Vector2 barOffset;

		public Enemy(string filePath) : base(filePath)
		{
			barComponent = new BarComponent(position, new Vector2(200, 50), 100);
			barOffset = new Vector2(0, 200);
		}

		private void TurnToFace(Vector2 location)
		{
			_targetDirection = location - position;
			_targetRotation = (float)Math.Atan2(_targetDirection.Y, _targetDirection.X);

			if (rotation < _targetRotation) // The scaler here can be replaced by a "turnspeed" in the future
				rotation += 0.1f;
			else if (rotation > _targetRotation)
				rotation -= 0.1f;
		}

		public override void Update(GameTime gameTime)
		{
			_playerPosition = GameManager.Instance.player.position;
			TurnToFace(_playerPosition);
			position = Vector2.Lerp(position, _playerPosition, 0.005f);

			barComponent.Update(100, position + barOffset);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			//Texture2D box = new Texture2D(GameManager.Instance.graphicsDevice, Bounds.Width, Bounds.Height);
			//Color[] data = new Color[Bounds.Width * Bounds.Height];
			//for (int i = 0; i < data.Length; ++i)
			//	data[i] = Color.White;
			//box.SetData(data);
			//spriteBatch.Draw(box, position, null, Color.Red, rotation, origin, scale, SpriteEffects.None, 1);

			spriteBatch.Draw(this.Texture, position, null, color, (rotation), origin, scale, SpriteEffects.None, 1);

			barComponent.Draw(spriteBatch);
		}
	}
}
