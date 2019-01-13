using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Managers;

namespace Fleet.Entities.Base
{
	public abstract class Entity
	{
		public enum EntityType { NONE, PLAYER, COMPUTER };
		public readonly EntityType entityType;

		// Public variables
		public bool isActive = true;
		public bool isSelected = false;
		public float scale = 1;
		public float rotation = 0.0f;
		public Entity parent;
		public Vector2 position = Vector2.Zero;
		public Vector2 velocity = Vector2.Zero;
		public Vector2 acceleration = Vector2.Zero;
		public Vector2 origin = Vector2.Zero;
		public Color color = Color.White;
		public Color[] textureData;

		// Protected variables
		protected KeyboardState currentKeyboardState;
		protected KeyboardState previousKeyboardState;
		protected MouseState currentMouseState;
		protected MouseState previousMouseState;
		protected bool drawBoundingBox = false;

		// Private variables
		private Rectangle _boundingBox = Rectangle.Empty;

		// Properties
		public Texture2D Texture { get; private set; }

		public Rectangle BoundingBox
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

		// Implementation
		public Entity(string spriteName, EntityType type = EntityType.NONE)
		{
			Texture = ResourceManager.Instance.GetTexture(spriteName);
			origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
			entityType = type;

			textureData = new Color[Texture.Width * Texture.Height];
			Texture.GetData(textureData);
		}

		public abstract void CheckCollision(Entity other);

		public abstract void Update(GameTime gameTime);

		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
