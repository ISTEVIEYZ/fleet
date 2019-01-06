using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Screen
{
	public class Camera
	{
		public Viewport Viewport;
		public Matrix transform;

		private float _zoom;
		private float _rotation;
		private Vector2 _position;
		private Vector2 _boundsOffset;

		public Vector2 Position { get { return _position; } set { _position = value; } }

		public float Rotation { get { return _rotation; } set { _rotation = value; } }

		public float Zoom
		{
			get { return _zoom; }
			set
			{
				_zoom = value;
				_zoom = _zoom < 0.1f ? 0.1f : _zoom; // Negative zoom will flip image
			}
		}

		public Camera(Viewport viewport, Vector2 startPosition, float startZoom, float startRotation)
		{
			Position = startPosition;
			Zoom = startZoom;
			Rotation = startRotation;
			Viewport = viewport;

			_boundsOffset.X = Viewport.Width * 0.05f;
			_boundsOffset.Y = Viewport.Height * 0.05f;
		}

		public void Update()
		{
			var currentKBState = Keyboard.GetState();
			var currentMouseState = Mouse.GetState();

			// Move camera with mouse
			if (currentMouseState.X <= _boundsOffset.X && currentMouseState.X > 0)
			{
				_position.X -= 50;
			}
			if (currentMouseState.X >= Viewport.Width - _boundsOffset.X && currentMouseState.X < Viewport.Width)
			{
				_position.X += 50;
			}
			if (currentMouseState.Y <= _boundsOffset.Y && currentMouseState.Y > 0)
			{
				_position.Y -= 50;
			}
			if (currentMouseState.Y >= Viewport.Height - _boundsOffset.Y && currentMouseState.Y < Viewport.Height)
			{
				_position.Y += 50;
			}

			// Move camera with keyboard
			if (currentKBState.IsKeyDown(Keys.Up))
			{
				_position.Y -= 50;
			}
			if (currentKBState.IsKeyDown(Keys.Down))
			{
				_position.Y += 50;
			}
			if (currentKBState.IsKeyDown(Keys.Left))
			{
				_position.X -= 50;
			}
			if (currentKBState.IsKeyDown(Keys.Right))
			{
				_position.X += 50;
			}

			// Camera Zoom
			if (currentKBState.IsKeyDown(Keys.X))
			{
				Zoom -= 0.01f;
			}
			if (currentKBState.IsKeyDown(Keys.Z))
			{
				Zoom += 0.01f;
			}

			// Camera default position
			if (currentKBState.IsKeyDown(Keys.Space))
			{
				_position = GameManager.Instance.player.position;
			}
		}

		// Auxiliary function to move the camera
		public void Move(Vector2 amount)
		{
			_position += amount;
		}

		public void LookAt(Vector2 position)
		{
			position = position - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
		}

		public Vector2 WorldToScreen(Vector2 worldPosition)
		{
			return Vector2.Transform(worldPosition, GetTransformation());
		}

		public Vector2 ScreenToWorld(Vector2 screenPosition)
		{
			return Vector2.Transform(screenPosition, Matrix.Invert(GetTransformation()));
		}

		public Matrix GetTransformation()
		{
			transform = Matrix.CreateTranslation(
				new Vector3(-_position.X, -_position.Y, 0)) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
				Matrix.CreateTranslation(new Vector3(Viewport.Width * 0.5f, Viewport.Height * 0.5f, 0));

			return transform;
		}
	}
}
