using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet
{
    public class Camera
    {
		public Viewport Viewport;
		public Matrix transform; // Matrix Transform
        
        protected float rotation; // Camera Rotation

		private Vector2 boundsOffset;
		private float zoom; // Camera Zoom
		private Vector2 position; // Camera Position

        public Vector2 Position
        {
            get { return this.position; }
            set { position = value; }
        }

        public Camera(Viewport viewport, Vector2 startPosition, float startZoom, float startRotation)
        {
            this.position = startPosition;
            this.zoom = startZoom;
            this.rotation = startRotation;
			this.Viewport = viewport;

			this.boundsOffset.X = Viewport.Width * 0.05f;
			this.boundsOffset.Y = Viewport.Height * 0.05f;
			
		}

        // Sets and gets zoom
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Update()
        {
            var currentKBState = Keyboard.GetState();
			var currentMouseState = Mouse.GetState();

			// Move camera with mouse
			if (currentMouseState.X <= boundsOffset.X)
			{
				this.position.X -= 50;
			}
			if (currentMouseState.X >= Viewport.Width - boundsOffset.X)
			{
				this.position.X += 50;
			}
			if (currentMouseState.Y <= boundsOffset.Y)
			{
				this.position.Y -= 50;
			}
			if (currentMouseState.Y >= Viewport.Height - boundsOffset.Y)
			{
				this.position.Y += 50;
			}

			// Move camera with keyboard
			if (currentKBState.IsKeyDown(Keys.Up))
            {
                this.position.Y -= 50;
            }
            if (currentKBState.IsKeyDown(Keys.Down))
            {
                this.position.Y += 50;
            }
            if (currentKBState.IsKeyDown(Keys.Left))
            {
                this.position.X -= 50;
            }
            if (currentKBState.IsKeyDown(Keys.Right))
            {
                this.position.X += 50;
            }

			// Camera Zoom
            if (currentKBState.IsKeyDown(Keys.X))
            {
                this.zoom -= 0.01f;
            }
            if (currentKBState.IsKeyDown(Keys.Z))
            {
                this.zoom += 0.01f;
            }

			// Camera default position
			if (currentKBState.IsKeyDown(Keys.Space))
			{
				this.position = GameManager.Instance.activePlayer.Position;
			}
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public void LookAt(Vector2 position)
        {
            position = position - new Vector2(Viewport.Width / 2f,
                Viewport.Height / 2f);
        }

        public Vector2 WorldToScreen(Vector2 worldPosition, GraphicsDevice graphicsDevice)
        {
            return Vector2.Transform(worldPosition, GetTransformation(graphicsDevice));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition, GraphicsDevice graphicsDevice)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(GetTransformation(graphicsDevice)));
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            transform =
              Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(Viewport.Width * 0.5f, Viewport.Height * 0.5f, 0));
            return transform;
        }
    }
}
