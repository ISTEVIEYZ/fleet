using System.Collections.Generic;
using Fleet.Entities.Base;

namespace Fleet.Algorithms.AStar
{
	public class Grid
	{
		public Cell[,] Cells { get; private set; }

		public Grid(int width, int height, List<Entity> entities)
		{
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

			for (int i = 0; i < entities.Count; i++)
			{
				// TODO: Take width and height into account so they take up more than one cell.
				int x = (int)entities[i].position.X;
				int y = (int)entities[i].position.Y;

				Cells[x, y].isOpen = false;
			}
		}
	}
}
