using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleet.ShipBuilder.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.ShipBuilder.Scenes
{
	public class Home
	{
		private Button startButton;

		public Home(Texture2D startButtonTexture)
		{
			this.startButton = new Button(startButtonTexture, new Rectangle(0, 0, 146, 35), new Rectangle(150, 150, 146, 35));
		}

		public void Update(GameTime gameTime)
		{
			startButton.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			startButton.Draw(spriteBatch, gameTime);
		}
	}
}
