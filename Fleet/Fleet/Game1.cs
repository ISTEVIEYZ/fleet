using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Fleet.Entity;
using Fleet.Managers;

namespace Fleet
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		SpriteFont font;
		Camera camera;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.IsMouseVisible = true;
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			camera = new Camera(graphics.GraphicsDevice.Viewport, new Vector2(0, 0), 0.2f, 0);
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			font = Content.Load<SpriteFont>("font");
			Enemy enemy = new Enemy(Content.Load<Texture2D>("ship"));
			Player player = new Player(Content.Load<Texture2D>("ship"));

			GameManager.Instance.activePlayer = player;
			GameManager.Instance.entityList.Add(player);
			GameManager.Instance.entityList.Add(enemy);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Update world and players
			foreach (Entity.Entity entity in GameManager.Instance.entityList)
			{
				entity.Update(gameTime);
			}

			// Update others
			camera.Update();

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// Draw world and players
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.GetTransformation(GraphicsDevice));
			foreach (Entity.Entity entity in GameManager.Instance.entityList)
			{
				entity.Draw(spriteBatch, gameTime);
			}
			spriteBatch.End();

			// Draw text
			spriteBatch.Begin();
			spriteBatch.DrawString(font, "X: " + GameManager.Instance.activePlayer.Position.X + ", Y: " + GameManager.Instance.activePlayer.Position.Y, new Vector2(5, 0), Color.White);
			spriteBatch.DrawString(font, "Rotation: " + GameManager.Instance.activePlayer.Rotation, new Vector2(5, 20), Color.White);
			spriteBatch.DrawString(font, "Velocity: " + GameManager.Instance.activePlayer.Velocity, new Vector2(5, 40), Color.White);
			spriteBatch.DrawString(font, "Mouse: {X: " + Mouse.GetState().X + ", Y: " + Mouse.GetState().Y + "}", new Vector2(5, 60), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
