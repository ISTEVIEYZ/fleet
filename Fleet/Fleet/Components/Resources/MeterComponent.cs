using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;

namespace Fleet.Components.Resources
{
	public class MeterComponent
	{
		private const float MAX_METER_ANGLE = 230;
		private bool _enabled = false;
		private float _scale;
		private float _lastAngle;
		private Vector2 _meterPosition;
		private Vector2 _meterOrigin;
		private Texture2D _backgroundImage;
		private Texture2D _needleImage;
		public float currentAngle = 0;

		/// <summary>
		/// Creates a new TextComponent for the HUD.
		/// </summary>
		/// <param name="position">Component position on the screen.</param>
		/// <param name="backgroundImage">Image for the background of the meter.</param>
		/// <param name="needleImage">Image for the neede of the meter.</param>
		/// <param name="spriteBatch">SpriteBatch that is required to draw the sprite.</param>
		/// <param name="scale">Factor to scale the graphics.</param>
		public MeterComponent(Vector2 position, Texture2D backgroundImage, Texture2D needleImage,float scale)
		{
			_backgroundImage = backgroundImage;
			_needleImage = needleImage;
			_scale = scale;

			_lastAngle = 0;

			_meterPosition = new Vector2(position.X + backgroundImage.Width / 2, position.Y + backgroundImage.Height / 2);
			_meterOrigin = new Vector2(52, 18);
		}

		/// <summary>
		/// Updates the current value of that should be displayed.
		/// </summary>
		/// <param name="currentValue">Value that to be displayed.</param>
		/// <param name="maximumValue">Maximum value that can be displayed by the meter.</param>
		public void Update(float currentValue, float maximumValue)
		{
			currentAngle = MathHelper.SmoothStep(_lastAngle, (currentValue / maximumValue) * MAX_METER_ANGLE, 0.2f);
			_lastAngle = currentAngle;
		}

		/// <summary>
		/// Draws the MeterComponent with the values set before.
		/// <param name="spriteBatch">SpriteBatch that is required to draw the sprite.</param>
		/// </summary>
		public void Draw(SpriteBatch spriteBatch)
		{
			if (_enabled)
			{
				//blendstate.alphablend???
				//spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);
				spriteBatch.Begin();
				spriteBatch.Draw(_backgroundImage, _meterPosition, null, Color.White, 0, new Vector2(_backgroundImage.Width / 2, _backgroundImage.Height / 2), _scale, SpriteEffects.None, 0); //Draw(backgroundImage, position, Color.White);
				spriteBatch.Draw(_needleImage, _meterPosition, null, Color.White, MathHelper.ToRadians(currentAngle), _meterOrigin, _scale, SpriteEffects.None, 0);
				spriteBatch.End();
			}
		}
	}
}
