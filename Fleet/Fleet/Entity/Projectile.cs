using System;
using Fleet.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fleet.Entity
{
	public class Projectile : Entity
	{
		private Vector2 _targetLocation;
		private Vector2 _startPosition;

    private float timer;

		public Projectile(string filePath) : base(filePath) { }

		public Projectile(string filePath, Vector2 playerPosition) : base(filePath)
		{
			var mouseState = Mouse.GetState();

      _startPosition = playerPosition;
      _targetLocation = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
		}

    public Projectile(string filePath, Vector2 playerPosition, float playerRotation) : base(filePath)
    {
      var mouseState = Mouse.GetState();

      timer = 20f;

      this.position = playerPosition;
      this.rotation = playerRotation;
      _targetLocation = GameManager.Instance.camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
    }

    public override void Update(GameTime gameTime)
		{
      //i hate my life
      //it took me an hour to see an easy solution to this
      //didnt use a video tho so im proud
      this.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)); // calculates a given unit vector based off of the player rotation we already have
      this.position += this.velocity * 100f; //with a unit vector established we just apply a speed and hurray it works. M A G I C


      // code to add - if projectile active for too long set isActive to false so garbage collector can delete it
      timer--;
      if (timer < 0)
        isActive = false;
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Draw(Texture, position, null, color, rotation, origin, 2, SpriteEffects.None, 1);
		}
	}
}
