using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Managers;

namespace Fleet.Entity
{
	public abstract class Entity : ICloneable
	{
    public bool isActive = true;

		public float rotation = 0.0f;
		public Vector2 position = Vector2.Zero;
		public Vector2 velocity = Vector2.Zero;
		public Vector2 acceleration = Vector2.Zero;
		public Vector2 origin = Vector2.Zero;
		public Color color = Color.White;

		protected KeyboardState currentKeyboardState;
		protected KeyboardState previousKeyboardState;
		protected MouseState currentMouseState;
		protected MouseState previousMouseState;

		public Texture2D Texture { get; private set; }

		public Entity(string filePath)
		{
			Texture = ResourceManager.Instance.GetTexture(filePath);
			origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public abstract void Update(GameTime gameTime);

		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
