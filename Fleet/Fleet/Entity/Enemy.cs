using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;

namespace Fleet.Entity
{
	public class Enemy : Entity
	{
		Vector2 playerPosition;

    private float _life = 100;

		public Enemy(string filePath) : base(filePath) { }

		public override void Update(GameTime gameTime)
		{
			foreach (Entity player in GameManager.Instance.Entities.OfType<Player>())
			{
				playerPosition = player.position;
				break;
			}

			position = Vector2.Lerp(position, playerPosition, 0.001f);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
      spriteBatch.Draw(this.Texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);
    }
	}
}
