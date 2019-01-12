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

		public Button(Texture2D texture, Rectangle src, Rectangle dst)
		{
			this.texture = texture;
			this.sourceRect = src;
			this.destRect = dst;
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
		}
	}
}
