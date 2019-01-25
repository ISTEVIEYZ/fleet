using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Fleet.Managers;
using Fleet.Globals;

namespace Fleet.Algorithms.AStar
{
	public class Cell
	{
		public Cell parent;
		public bool isOpen;
		public float f, g, h, priority;

		private int _width = 100;
		private int _height = 100;
		private Vector2 _position;
		private Texture2D _rectangle;

		public Cell(float x, float y)
		{
			isOpen = true;
			f = g = h = priority = 0f;
			_position = new Vector2(x * _width, y * _height);

			SetRectangleTexture();
		}

		private void SetRectangleTexture()
		{
			_rectangle = ResourceManager.Instance.GetTexture(TextureNames.CELL_RECTANGLE);

			if (_rectangle == null)
			{
				List<Color> colors = new List<Color>();

				for (int y = 0; y < _height; y++)
				{
					for (int x = 0; x < _width; x++)
					{
						// Top, Left, Bottom, Right
						if (y == 0 || x == 0 || y == _height - 1 || x == _width - 1)
						{
							colors.Add(new Color(255, 255, 255, 255)); // white
						}
						else
						{
							colors.Add(new Color(0, 0, 0, 0)); // transparent 
						}
					}
				}

				_rectangle = new Texture2D(GameManager.Instance.graphicsDevice, _width, _height);
				_rectangle.SetData(colors.ToArray());
				ResourceManager.Instance.AddTexture(TextureNames.CELL_RECTANGLE, _rectangle);
			}
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(_rectangle, _position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
		}
	}
}
