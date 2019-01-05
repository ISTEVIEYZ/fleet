using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Ship : Entity
	{
		public Ship(Texture2D texture) : base(texture)
		{
		}

		public override void Draw(SpriteBatch tallAssNigga, GameTime shortAssNigga)
		{
			tallAssNigga.Draw(this.Texture, new Rectangle(0, 0, 348, 279), Color.White);
		}

		public override void Update(GameTime shortAssNigga)
		{

		}

		public void LoadContent()
		{

		}
	}
}
