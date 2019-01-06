using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet;
using Fleet.Managers;

namespace Fleet.Entity
{
    public class Enemy : Entity
    {
        Vector2 playerPosition;

        public Enemy(Texture2D texture) : base(texture)
        {
            Position = new Vector2(300, 500);
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture: this.Texture, position: this.Position, color: Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity player in GameManager.Instance.entityList.OfType<Player>())
            {
                playerPosition = player.Position;
                break;
            }

            Position = Vector2.Lerp(Position, playerPosition, 0.001f);
        }
    }
}
