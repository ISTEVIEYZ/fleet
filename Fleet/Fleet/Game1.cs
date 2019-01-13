using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Entities.Base;
using Fleet.Managers;
using Fleet.Screen;
using Fleet.Globals;
using static Fleet.Entities.Base.Entity;
using Fleet.Entities.Ships;

namespace Fleet
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;

		Player player;
		Titan enemy;
		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			IsMouseVisible = true;
			IsFixedTimeStep = true;
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
			ResourceManager.Instance.SetContentManager(Content);
			GameManager.Instance.graphicsDevice = graphics.GraphicsDevice;
			GameManager.Instance.camera = new Camera(graphics.GraphicsDevice.Viewport, new Vector2(0, 0), 0.2f, 0);
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

			// Load fonts
			font = ResourceManager.Instance.GetFont(Fonts.CALIBRI);

			// Load player
			player = new Player();
			player.AddShip(new Crusader(EntityType.PLAYER));

			// Load enemies
			enemy = new Titan(EntityType.COMPUTER, player.GetSelectedShip().position) { position = new Vector2(300, 500), color = Color.RoyalBlue };

			// Setup Game Manager
			GameManager.Instance.player = player.GetSelectedShip();
			GameManager.Instance.Entities.Add(enemy);
			GameManager.Instance.minimap = new Minimap(Sprites.MINIMAP);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			Content.Unload();
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

			// Update all the entities
			for (int i = 0; i < GameManager.Instance.Entities.ToArray().Length; i++)
			{
				GameManager.Instance.Entities[i].Update(gameTime);
			}

			// Check for collisions
			foreach (Entity entity in GameManager.Instance.Entities.ToArray())
			{
				foreach (Entity other in GameManager.Instance.Entities.ToArray())
				{
					entity.CheckCollision(other);
				}
			}

			// Update others
			GameManager.Instance.minimap.Update(gameTime, GameManager.Instance.Entities);
			GameManager.Instance.camera.Update();
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
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, GameManager.Instance.camera.GetTransformation());
			foreach (Entity entity in GameManager.Instance.Entities)
			{
				entity.Draw(spriteBatch, gameTime);
			}
			spriteBatch.End();

			// Draw text
			spriteBatch.Begin();
			spriteBatch.DrawString(font, "Position: { X: " + player.GetSelectedShip().position.X.ToString("0.##") + ", Y: " + player.GetSelectedShip().position.Y.ToString("0.##") + " }", new Vector2(10, 10), Color.White);
			spriteBatch.DrawString(font, "Rotation: " + player.GetSelectedShip().rotation.ToString("0.##"), new Vector2(10, 30), Color.White);
			spriteBatch.DrawString(font, "Velocity: { X: " + player.GetSelectedShip().velocity.X.ToString("0.##") + ", Y: " + player.GetSelectedShip().velocity.Y.ToString("0.##") + " }", new Vector2(10, 50), Color.White);
			spriteBatch.DrawString(font, "Mouse: { X: " + Mouse.GetState().X + ", Y: " + Mouse.GetState().Y + " }", new Vector2(10, 70), Color.White);

			// Draw other stuff
			GameManager.Instance.minimap.Draw(spriteBatch, gameTime);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
