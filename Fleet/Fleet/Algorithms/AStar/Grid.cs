using System.Collections.Generic;
using Fleet.Entities.Base;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Algorithms.AStar
{
	public class Grid
	{
		private int _width, _height;

		public Cell[,] Cells { get; private set; }

		public Grid(int width, int height, List<Entity> entities)
		{
			_width = width;
			_height = height;
			Cells = new Cell[width, height];
			CreateGrid(width, height, entities);
		}

		public void CreateGrid(int width, int height, List<Entity> entities)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Cells[i, j] = new Cell(i, j);
				}
			}

			//for (int i = 0; i < entities.Count; i++)
			//{
			//	// TODO: Take width and height into account so they take up more than one cell.
			//	Cells[(int)entities[i].position.X, (int)entities[i].position.Y].isOpen = false;
			//}
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					Cells[i, j].Draw(spriteBatch, gameTime);
				}
			}
		}
	}
}
