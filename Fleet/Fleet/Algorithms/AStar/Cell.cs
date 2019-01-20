namespace Fleet.Algorithms.AStar
{
	public class Cell
	{
		public Cell parent;
		public int x, y;
		public float f, g, h, priority;
		public bool isOpen;

		public Cell(int x, int y)
		{
			this.x = x;
			this.y = y;

			f = 0f;
			g = 0f;
			h = 0f;
			priority = 0f;
			isOpen = true;
		}
	}
}
