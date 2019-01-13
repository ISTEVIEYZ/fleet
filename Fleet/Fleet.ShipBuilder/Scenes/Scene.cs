using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.ShipBuilder.Scenes
{
	public interface Scene
	{
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch, GameTime gameTime);
		Scene Transition();
	}
}
