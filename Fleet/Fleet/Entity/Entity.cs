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
		public float scale = 1;
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

		public bool CollidesWith(Entity other)
		{
			// Default behavior uses per-pixel collision detection
			return CollidesWith(other, true);
		}

		public bool CollidesWith(Entity other, bool calcPerPixel)
		{
			// Get dimensions of texture
			int widthOther = other.Texture.Width;
			int heightOther = other.Texture.Height;
			int widthMe = Texture.Width;
			int heightMe = Texture.Height;

			if (calcPerPixel &&                                // if we need per pixel
				((Math.Min(widthOther, heightOther) > 100) ||  // at least avoid doing it
				(Math.Min(widthMe, heightMe) > 100)))          // for small sizes (nobody will notice :P)
			{
				return Bounds.Intersects(other.Bounds) // If simple intersection fails, don't even bother with per-pixel
					&& PerPixelCollision(this, other);
			}

			return Bounds.Intersects(other.Bounds);
		}

		static bool PerPixelCollision(Entity a, Entity b)
		{
			// Get Color data of each Texture
			Color[] bitsA = new Color[a.Texture.Width * a.Texture.Height];
			a.Texture.GetData(bitsA);
			Color[] bitsB = new Color[b.Texture.Width * b.Texture.Height];
			b.Texture.GetData(bitsB);

			// Calculate the intersecting rectangle
			int x1 = Math.Max(a.Bounds.X, b.Bounds.X);
			int x2 = Math.Min(a.Bounds.X + a.Bounds.Width, b.Bounds.X + b.Bounds.Width);

			int y1 = Math.Max(a.Bounds.Y, b.Bounds.Y);
			int y2 = Math.Min(a.Bounds.Y + a.Bounds.Height, b.Bounds.Y + b.Bounds.Height);

			// For each single pixel in the intersecting rectangle
			for (int y = y1; y < y2; ++y)
			{
				for (int x = x1; x < x2; ++x)
				{
					// Get the color from each texture
					Color aCheck = bitsA[(x - a.Bounds.X) + (y - a.Bounds.Y) * a.Texture.Width];
					Color bCheck = bitsB[(x - b.Bounds.X) + (y - b.Bounds.Y) * b.Texture.Width];

					if (aCheck.A != 0 && bCheck.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
					{
						return true;
					}
				}
			}
			// If no collision occurred by now, we're clear.
			return false;
		}

		private Rectangle bounds = Rectangle.Empty;
		public virtual Rectangle Bounds
		{
			get
			{
				return new Rectangle(
					((int)position.X + Texture.Width),
					((int)position.Y + Texture.Height),
					Texture.Width,
					Texture.Height);
			}
		}
	}
}
