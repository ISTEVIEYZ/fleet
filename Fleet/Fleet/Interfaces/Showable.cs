using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Interfaces
{
	interface Showable
	{
		void Update(GameTime gameTime);

		void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
