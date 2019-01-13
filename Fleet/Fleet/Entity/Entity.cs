using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Managers;

namespace Fleet.Entity
{
	public abstract class Entity : ICloneable
	{
		// Public variables
		public bool isActive = true;
		public float scale = 1;
		public float rotation = 0.0f;
		public Entity parent;
		public Vector2 position = Vector2.Zero;
		public Vector2 velocity = Vector2.Zero;
		public Vector2 acceleration = Vector2.Zero;
		public Vector2 origin = Vector2.Zero;
		public Color color = Color.White;
		public readonly Color[] textureData;

		// Protected variables
		protected KeyboardState currentKeyboardState;
		protected KeyboardState previousKeyboardState;
		protected MouseState currentMouseState;
		protected MouseState previousMouseState;

		// Properties
		public Texture2D Texture { get; private set; }

		private Rectangle _boundingBox = Rectangle.Empty;
		public Rectangle Bounds
		{
			get
			{
				_boundingBox.X = (int)position.X - (int)origin.X;
				_boundingBox.Y = (int)position.Y - (int)origin.Y;
				_boundingBox.Width = Texture.Width;
				_boundingBox.Height = Texture.Height;

				return _boundingBox;
			}
		}

		private Matrix _transform = new Matrix();
		public Matrix Transform
		{
			get
			{
				_transform = Matrix.CreateTranslation(new Vector3(-origin, 0)) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(position, 0));
				return _transform;
			}
		}

		// Implementation
		public Entity(string filePath)
		{
			Texture = ResourceManager.Instance.GetTexture(filePath);
			origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);

			textureData = new Color[Texture.Width * Texture.Height];
			Texture.GetData(textureData);
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

			if (calcPerPixel && (Math.Min(widthOther, heightOther) > 100 || Math.Min(widthMe, heightMe) > 100))
			{
				// If simple intersection fails, don't even bother with per-pixel
				return Bounds.Intersects(other.Bounds) && Intersects(other);
			}

			return Bounds.Intersects(other.Bounds);
		}
		
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

					if (0 <= xB && xB < entity.Bounds.Width && 0 <= yB && yB < entity.Bounds.Height)
					{
						// Get the colors of the overlapping pixels
						var colourA = textureData[xA + yA * Bounds.Width];
						var colourB = entity.textureData[xB + yB * entity.Bounds.Width];

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
