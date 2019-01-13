using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fleet.Managers;

namespace Fleet.Components.Resources
{
	public class BarComponent
	{
		private Texture2D _dummyTexture;
		private GraphicsDevice _graphicsDevice;
		private Color _backgroundColor;
		private Color _barColor;
		private Rectangle _backgroundRectangle;
		private Vector2 _position;
		private Vector2 _offset;
		private Vector2 _dimension;
		private float _valueMax;
		private float _valueCurrent;
		private float _percent;
		private bool _enabled;

		/// <summary>
		/// Creates a new Bar Component for the HUD.
		/// </summary>
		/// <param name="position">Component position on the screen.</param>
		/// <param name="dimension">Component dimensions.</param>
		/// <param name="valueMax">Maximum value to be displayed.</param>
		public BarComponent(Vector2 position, Vector2 offset, Vector2 dimension, float valueMax)
		{
			_position = position;
			_offset = offset;
			_dimension = dimension;
			_valueMax = valueMax;
			_enabled = true;
			_backgroundColor = new Color(0, 0, 0, 128);
			_backgroundRectangle = new Rectangle();
			_graphicsDevice = GameManager.Instance.graphicsDevice;
			_dummyTexture = new Texture2D(_graphicsDevice, 1, 1);
		}

		/// <summary>
		/// Sets whether the component should be drawn.
		/// </summary>
		/// <param name="enabled">enable the component</param>
		public void Enable(bool enabled)
		{
			_enabled = enabled;
		}

		/// <summary>
		/// Updates the text that is displayed after ":".
		/// </summary>
		/// <param name="valueCurrent">Text to be displayed.</param>
		public void Update(float valueCurrent, Vector2 position)
		{
			_valueCurrent = valueCurrent;
			_position = position + _offset;
		}

		/// <summary>
		/// Draws the BarComponent with the values set before.
		/// </summary>
		public void Draw(SpriteBatch spriteBatch)
		{
			if (_enabled)
			{
				_percent = _valueCurrent / _valueMax;

				_barColor = new Color(0, 255, 0, 200);
				if (_percent < 0.50)
					_barColor = new Color(255, 255, 0, 200);
				if (_percent < 0.20)
					_barColor = new Color(255, 0, 0, 200);

				_backgroundRectangle.Width = (int)_dimension.X;
				_backgroundRectangle.Height = (int)_dimension.Y;
				_backgroundRectangle.X = (int)_position.X;
				_backgroundRectangle.Y = (int)_position.Y;

				_dummyTexture.SetData(new Color[] { _backgroundColor });

				spriteBatch.Draw(_dummyTexture, _backgroundRectangle, _backgroundColor);

				_backgroundRectangle.Width = (int)(_dimension.X * 0.9);
				_backgroundRectangle.Height = (int)(_dimension.Y * 0.5);
				_backgroundRectangle.X = (int)_position.X + (int)(_dimension.X * 0.05);
				_backgroundRectangle.Y = (int)_position.Y + (int)(_dimension.Y * 0.25);

				spriteBatch.Draw(_dummyTexture, _backgroundRectangle, _backgroundColor);

				_backgroundRectangle.Width = (int)(_dimension.X * 0.9 * _percent);
				_backgroundRectangle.Height = (int)(_dimension.Y * 0.5);
				_backgroundRectangle.X = (int)_position.X + (int)(_dimension.X * 0.05);
				_backgroundRectangle.Y = (int)_position.Y + (int)(_dimension.Y * 0.25);

				//_dummyTexture = new Texture2D(_graphicsDevice, 1, 1);
				_dummyTexture.SetData(new Color[] { _barColor });

				spriteBatch.Draw(_dummyTexture, _backgroundRectangle, _barColor);
			}
		}
	}
}
