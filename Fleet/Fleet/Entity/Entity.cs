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

			TextureData = new Color[Texture.Width * Texture.Height];
			Texture.GetData(TextureData);
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
					&& Intersects(other);
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
					((int)position.X - (int)origin.X),
					((int)position.Y - (int)origin.Y),
					Texture.Width,
					Texture.Height);
			}
		}

		public Matrix Transform
		{
			get
			{
				return Matrix.CreateTranslation(new Vector3(-origin, 0)) *
				  Matrix.CreateRotationZ(rotation) *
				  Matrix.CreateTranslation(new Vector3(position, 0));
			}
		}

		public readonly Color[] TextureData;


		public bool Intersects(Entity entity)
		{
			// Calculate a matrix which transforms from A's local space into
			// world space and then into B's local space
			var transformAToB = this.Transform * Matrix.Invert(entity.Transform);

			// When a point moves in A's local space, it moves in B's local space with a
			// fixed direction and distance proportional to the movement in A.
			// This algorithm steps through A one pixel at a time along A's X and Y axes
			// Calculate the analogous steps in B:
			var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
			var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

			// Calculate the top left corner of A in B's local space
			// This variable will be reused to keep track of the start of each row
			var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

			for (int yA = 0; yA < this.Bounds.Height; yA++)
			{
				// Start at the beginning of the row
				var posInB = yPosInB;

				for (int xA = 0; xA < this.Bounds.Width; xA++)
				{
					// Round to the nearest pixel
					var xB = (int)Math.Round(posInB.X);
					var yB = (int)Math.Round(posInB.Y);

					if (0 <= xB && xB < entity.Bounds.Width &&
						0 <= yB && yB < entity.Bounds.Height)
					{
						// Get the colors of the overlapping pixels
						var colourA = this.TextureData[xA + yA * this.Bounds.Width];
						var colourB = entity.TextureData[xB + yB * entity.Bounds.Width];

						// If both pixel are not completely transparent
						if (colourA.A != 0 && colourB.A != 0)
						{
							return true;
						}
					}

					// Move to the next pixel in the row
					posInB += stepX;
				}

				// Move to the next row
				yPosInB += stepY;
			}

			// No intersection found
			return false;
		}







	}
}
