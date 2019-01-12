using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Interfaces;

namespace Fleet.Screen
{
	public class Minimap
	{
		private Vector2 position = new Vector2(1150, 620);
		private Vector2 playerPosition;
		private bool enabled = true;
		private float scale = 0.4f;
		private float sectorWidth = 10000;
		private float sectorHeight = 10000;

		private List<Entity.Entity> entityList;


		public Minimap()
		{

		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Texture2D backgroundImage, GraphicsDevice graphicsDevice)
		{
			if (enabled)
			{
				spriteBatch.Draw(backgroundImage, position, null, Color.White, 0, new Vector2(backgroundImage.Width / 2, backgroundImage.Height / 2), scale, SpriteEffects.None, 0);


				for (int i = 0; i < entityList.Count; i++)
				{
					Color myTransparentColor = new Color(255, 0, 0);

					if (entityList[i] is Entity.Player)
					{
						playerPosition = entityList[i].position;
						myTransparentColor = new Color(255, 255, 0);
					}

					if (entityList[i] is Entity.Enemy)
					{
						myTransparentColor = new Color(255, 0, 0);
					}

					Vector2 temp = entityList[i].position;
					temp.X = temp.X / sectorWidth * backgroundImage.Width / 2 * scale;
					temp.Y = temp.Y / sectorHeight * backgroundImage.Height / 2 * scale;

					//temp = Vector3.Transform(temp, Matrix.CreateRotationY(MathHelper.ToRadians(currentAngle)));

					Rectangle backgroundRectangle = new Rectangle();
					backgroundRectangle.Width = 2;
					backgroundRectangle.Height = 2;
					backgroundRectangle.X = (int)(position.X + temp.X);
					backgroundRectangle.Y = (int)(position.Y + temp.Y);

					Texture2D dummyTexture = new Texture2D(graphicsDevice, 1, 1);
					dummyTexture.SetData(new Color[] { myTransparentColor });

					spriteBatch.Draw(dummyTexture, backgroundRectangle, myTransparentColor);
				}

				playerPosition.X = playerPosition.X / sectorWidth * backgroundImage.Width / 2 * scale;
				playerPosition.Y = playerPosition.Y / sectorHeight * backgroundImage.Height / 2 * scale;

				// playerPosition = Vector3.Transform(playerPosition, Matrix.CreateRotationY(MathHelper.ToRadians(currentAngle)));

				Rectangle backgroundRectangle2 = new Rectangle();
				backgroundRectangle2.Width = 5;
				backgroundRectangle2.Height = 5;
				backgroundRectangle2.X = (int)(position.X + playerPosition.X);
				backgroundRectangle2.Y = (int)(position.Y + playerPosition.Y);

				Texture2D dummyTexture2 = new Texture2D(graphicsDevice, 1, 1);
				dummyTexture2.SetData(new Color[] { Color.Pink });

				spriteBatch.Draw(dummyTexture2, backgroundRectangle2, Color.Pink);
			}
		}

		public void Update(GameTime gameTime, List<Entity.Entity> entityList)
		{
			this.entityList = entityList;
		}
	}
}
