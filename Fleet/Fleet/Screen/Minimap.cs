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
	public class Minimap : Showable
	{
		private Vector2 position;
		private Vector2 playerPosition;

		public Minimap()
		{

		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (enabled)
			{
				spriteBatch.Draw(backgroundImage, position, null, Color.White, 0, new Vector2(backgroundImage.Width / 2, backgroundImage.Height / 2), scale, SpriteEffects.None, 0);

				for (int i = 0; i < objectPositions.Length; i++)
				{
					Color myTransparentColor = new Color(255, 0, 0);
					if (highlight == i)
					{
						myTransparentColor = new Color(255, 255, 0);
					}
					else if (highlight > i)
					{
						myTransparentColor = new Color(0, 255, 0);
					}

					Vector3 temp = objectPositions[i];
					temp.X = temp.X / dimension * backgroundImage.Width / 2 * scale;
					temp.Z = temp.Z / dimension * backgroundImage.Height / 2 * scale;

					temp = Vector3.Transform(temp, Matrix.CreateRotationY(MathHelper.ToRadians(currentAngle)));

					Rectangle backgroundRectangle = new Rectangle();
					backgroundRectangle.Width = 2;
					backgroundRectangle.Height = 2;
					backgroundRectangle.X = (int)(position.X + temp.X);
					backgroundRectangle.Y = (int)(position.Y + temp.Z);

					Texture2D dummyTexture = new Texture2D(graphicsDevice, 1, 1);
					dummyTexture.SetData(new Color[] { myTransparentColor });

					spriteBatch.Draw(dummyTexture, backgroundRectangle, myTransparentColor);
				}

				myPosition.X = myPosition.X / dimension * backgroundImage.Width / 2 * scale;
				myPosition.Z = myPosition.Z / dimension * backgroundImage.Height / 2 * scale;

				myPosition = Vector3.Transform(myPosition, Matrix.CreateRotationY(MathHelper.ToRadians(currentAngle)));

				Rectangle backgroundRectangle2 = new Rectangle();
				backgroundRectangle2.Width = 5;
				backgroundRectangle2.Height = 5;
				backgroundRectangle2.X = (int)(position.X + myPosition.X);
				backgroundRectangle2.Y = (int)(position.Y + myPosition.Z);

				Texture2D dummyTexture2 = new Texture2D(graphicsDevice, 1, 1);
				dummyTexture2.SetData(new Color[] { Color.Pink });

				spriteBatch.Draw(dummyTexture2, backgroundRectangle2, Color.Pink);
			}
		}

		public void Update(GameTime gameTime)
		{

		}
	}
}
