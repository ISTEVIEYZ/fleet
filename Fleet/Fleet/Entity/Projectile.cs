﻿using System;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Projectile : Entity
	{
		private Vector2 _targetLocation;
    private Vector2 _targetAngle;

    private float timer;

		public Projectile(string filePath) : base(filePath) { }
    
    public Projectile(string filePath, Vector2 playerPosition) : base(filePath)
    {
      var mouseState = Mouse.GetState();

      timer = 100f;
      this.position = playerPosition;
      _targetLocation = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
      _targetAngle = _targetLocation - position;
      rotation = (float)Math.Atan2(_targetAngle.Y, _targetAngle.X);
    }

    public override void Update(GameTime gameTime)
		{
      
      this.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)); // calculates a given unit vector based off of the unit vector between target and start
      this.position += this.velocity * 100f; //with a unit vector established we apply a speed



      // code to add - if projectile active for too long set isActive to false so garbage collector can delete it
      timer--;
      if (timer < 0)
        isActive = false;
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(Texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);
		}
	}
}
