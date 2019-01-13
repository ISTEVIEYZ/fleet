using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fleet.Entity;
using Fleet.Managers;
using Fleet.Screen;
using Fleet.Globals;

namespace Fleet
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;

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

			// Load game assets
			font = ResourceManager.Instance.GetFont(Fonts.CALIBRI);
			Player player = new Player(Sprites.DEFAULT_SHIP);
			Enemy enemy = new Enemy(Sprites.TITAN_SHIP) { position = new Vector2(300, 500), color = Color.RoyalBlue };

			// Setup Game Manager
			GameManager.Instance.player = player;
			GameManager.Instance.Entities.Add(player);
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

			foreach (Entity.Entity entity in GameManager.Instance.Entities.ToArray())
			{
				entity.Update(gameTime);
			}

			// Update world and players
			foreach (Entity.Entity entity in GameManager.Instance.Entities.ToArray())
			{
				foreach (Entity.Entity entity2 in GameManager.Instance.Entities.ToArray())
				{
					if (entity != entity2 && !(entity2 is Player) && entity != entity2.parent)
					{
						if (entity.CollidesWith(entity2))
						{
							if (entity is Projectile)
							{
								entity.isActive = false;
							}
							Console.Out.WriteLine("collision" + entity.position);
						}
					}
				}

				if (!entity.isActive)
					GameManager.Instance.Entities.Remove(entity);
			}

			GameManager.Instance.minimap.Update(gameTime, GameManager.Instance.Entities);

			// Update others
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
			foreach (Entity.Entity entity in GameManager.Instance.Entities)
			{
				entity.Draw(spriteBatch, gameTime);
			}
			spriteBatch.End();

			// Draw text
			spriteBatch.Begin();
			spriteBatch.DrawString(font, "Position: { X: " + GameManager.Instance.player.position.X.ToString("0.##") + ", Y: " + GameManager.Instance.player.position.Y.ToString("0.##") + " }", new Vector2(10, 10), Color.White);
			spriteBatch.DrawString(font, "Rotation: " + GameManager.Instance.player.rotation.ToString("0.##"), new Vector2(10, 30), Color.White);
			spriteBatch.DrawString(font, "Velocity: { X: " + GameManager.Instance.player.velocity.X.ToString("0.##") + ", Y: " + GameManager.Instance.player.velocity.Y.ToString("0.##") + " }", new Vector2(10, 50), Color.White);
			spriteBatch.DrawString(font, "Mouse: { X: " + Mouse.GetState().X + ", Y: " + Mouse.GetState().Y + " }", new Vector2(10, 70), Color.White);

			// Draw other stuff
			GameManager.Instance.minimap.Draw(spriteBatch, gameTime);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
