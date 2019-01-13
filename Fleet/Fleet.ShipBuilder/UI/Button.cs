using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.ShipBuilder.UI
{
	public class Button
	{
		private Texture2D texture;
		private Rectangle sourceRect;
		private Rectangle destRect;
		private Color tint;
		private MouseState lastState;

		public event EventHandler OnClicked;

		public Button(Texture2D texture, Rectangle src, Rectangle dst)
		{
			this.texture = texture;
			this.sourceRect = src;
			this.destRect = dst;
			this.tint = Color.White;
		}

		public void Update(GameTime gameTime)
		{
			if (lastState == null)
			{
				lastState = Mouse.GetState();
			}

			MouseState ms = Mouse.GetState();
			if (destRect.Contains(ms.Position))
			{
				tint = Color.Cyan;
				if (ms.LeftButton == ButtonState.Pressed)
				{
					tint = Color.DarkCyan;
				}
				else if (ms.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed)
				{
					this.OnClicked?.Invoke(this, null);
				}
			}
			else
			{
				tint = Color.White;
			}

			lastState = Mouse.GetState();
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(texture, destRect, sourceRect, tint);
		}
	}
}
