using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet
{
    public class Camera
    {
        protected float zoom; // Camera Zoom
        public Matrix transform; // Matrix Transform
        private Vector2 position; // Camera Position
        protected float rotation; // Camera Rotation
        Viewport Viewport;

        private Vector2 positionGoto, positionFrom;
        private int currentStep, tweenSteps;

        public Vector2 Position
        {
            get { return this.position; }
            set { position = value; }
        }

        public Camera(Viewport viewport)
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
            Viewport = viewport;
        }
        public Camera(Vector2 startPosition, float startZoom, float startRotation)
        {
            this.position = startPosition;
            this.zoom = startZoom;
            this.rotation = startRotation;
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

            if (currentKBState.IsKeyDown(Keys.X))
            {
                this.zoom -= 0.01f;
            }


            if (currentKBState.IsKeyDown(Keys.Z))
            {
                this.zoom += 0.01f;
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
