using System.Collections.Generic;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet.Entities.Base
{
	public class Player
	{
		private List<Ship> _ships = new List<Ship>();

		public void AddShip(Ship ship)
		{
			_ships.Add(ship);
			GameManager.Instance.Entities.Add(ship);
		}

		public Ship GetSelectedShip()
		{
			// TODO: Return selected ship.
			return _ships[0];
		}
	}
}
