﻿using System;
using System.Collections.Generic;
using Fleet.Components.Physics.Interfaces;
using Fleet.Entities.Base;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Fleet.Entities.Base.Entity;

namespace Fleet.Components.Physics
{
	public class PerPixelCollisionComponent : ICollisionComponent
	{
		private readonly Entity _parent;
		private Texture2D _boundingBoxTexture;

		public PerPixelCollisionComponent(Entity parent)
		{
			_parent = parent;
			SetRectangleTexture();
		}

		private Matrix GetTransform(Entity entity)
		{
			return Matrix.CreateTranslation(new Vector3(-entity.origin, 0)) *
					Matrix.CreateRotationZ(entity.rotation) *
					Matrix.CreateTranslation(new Vector3(entity.position, 0));
		}

		public bool CollidesWith(Entity other)
		{
			if (_parent != other && _parent != other.parent && other.entityType != EntityType.PLAYER && other.entityType != _parent.entityType)
			{
				// Get dimensions of texture
				int widthOther = other.Texture.Width;
				int heightOther = other.Texture.Height;
				int widthMe = _parent.Texture.Width;
				int heightMe = _parent.Texture.Height;

				if (Math.Min(widthOther, heightOther) > 100 || Math.Min(widthMe, heightMe) > 100)
				{
					// If simple intersection fails, don't even bother with per-pixel
					return _parent.BoundingBox.Intersects(other.BoundingBox) && Intersects(other);
				}

				return _parent.BoundingBox.Intersects(other.BoundingBox);
			}

			return false;
		}

		private bool Intersects(Entity other)
		{
			// Calculate a matrix which transforms from A's local space into
			// world space and then into B's local space
			var transformAToB = GetTransform(_parent) * Matrix.Invert(GetTransform(other));

			// When a point moves in A's local space, it moves in B's local space with a
			// fixed direction and distance proportional to the movement in A.
			// This algorithm steps through A one pixel at a time along A's X and Y axes
			// Calculate the analogous steps in B:
			var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
			var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

			// Calculate the top left corner of A in B's local space
			// This variable will be reused to keep track of the start of each row
			var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

			for (int yA = 0; yA < _parent.BoundingBox.Height; yA++)
			{
				// Start at the beginning of the row
				var posInB = yPosInB;

				for (int xA = 0; xA < _parent.BoundingBox.Width; xA++)
				{
					// Round to the nearest pixel
					var xB = (int)Math.Round(posInB.X);
					var yB = (int)Math.Round(posInB.Y);

					if (0 <= xB && xB < other.BoundingBox.Width && 0 <= yB && yB < other.BoundingBox.Height)
					{
						// Get the colors of the overlapping pixels
						var colourA = _parent.textureData[xA + yA * _parent.BoundingBox.Width];
						var colourB = other.textureData[xB + yB * other.BoundingBox.Width];

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

		private void SetRectangleTexture()
		{
			List<Color> colors = new List<Color>();

			for (int y = 0; y < _parent.Texture.Height; y++)
			{
				for (int x = 0; x < _parent.Texture.Width; x++)
				{
					// Top, Left, Bottom, Right
					if (y == 0 || x == 0 || y == _parent.Texture.Height - 1 || x == _parent.Texture.Width - 1)
					{
						colors.Add(new Color(255, 255, 255, 255)); // white
					}
					else
					{
						colors.Add(new Color(0, 0, 0, 0)); // transparent 
					}
				}
			}

			_boundingBoxTexture = new Texture2D(GameManager.Instance.graphicsDevice, _parent.Texture.Width, _parent.Texture.Height);
			_boundingBoxTexture.SetData(colors.ToArray());
		}

		public void DrawBoundingBox(SpriteBatch spriteBatch)
		{
			// Draw rectangle
			spriteBatch.Draw(_boundingBoxTexture, _parent.position, null, Color.Red, _parent.rotation, _parent.origin, _parent.scale, SpriteEffects.None, 1);
		}
	}
}
