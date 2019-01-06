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
	public abstract class Entity
	{
		protected Texture2D Texture { get; private set; }

        public Vector2 Position;

        public float Rotation;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public float TurnAcceleration;


		public Entity(Texture2D texture)
		{
			this.Texture = texture;
		}

		public abstract void Update(GameTime gameTime);

		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
