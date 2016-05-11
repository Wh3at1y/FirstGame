using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using FirstGame.Model;

namespace FirstGame.Controller
{
	/// This is the main type for your game.
	public class FirstGame : Game
	{
		#region Declaration Section
		private bool isHoriz;
		private bool isDown;
		private bool isUp;
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Player player;

		// Keyboard states used to determine key presses
		private KeyboardState currentKeyboardState;
		private KeyboardState previousKeyboardState;

		// Gamepad states used to determine button presses
		private GamePadState currentGamePadState;
		private GamePadState previousGamePadState; 

		// A movement speed for the player
		private float playerMoveSpeed;
		#endregion

		#region Constructor
		public FirstGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		}
		#endregion

		#region Initialize (For Game Engine)
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		protected override void Initialize ()
		{
			player = new Player ();

			// Set a constant player move speed
			playerMoveSpeed = 8.0f;
            
			base.Initialize ();
		}
		#endregion

		#region Load Content (Pictures)
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			// Load the player resources 
			Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X - 15,GraphicsDevice.Viewport.TitleSafeArea.Y +GraphicsDevice.Viewport.TitleSafeArea.Height / 3);

			//Initializes the player's texture
			player.Initialize(Content.Load<Texture2D>("Textures/player"), playerPosition);
		}
		#endregion

		#region Update Player (KeyStrokes)
		private void UpdatePlayer(GameTime gameTime)
		{
			// Get Thumbstick Controls
			player.Position.X += currentGamePadState.ThumbSticks.Left.X *playerMoveSpeed;
			player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y *playerMoveSpeed;
			// Use the Keyboard / Dpad
			if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A) ||
				currentGamePadState.DPad.Left == ButtonState.Pressed)
			{
				player.Position.X -= playerMoveSpeed;
				isHoriz = true;
				isDown = false;
				isUp = false;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D) ||
				currentGamePadState.DPad.Right == ButtonState.Pressed)
			{
				player.Position.X += playerMoveSpeed;
				isHoriz = false;
				isDown = false;
				isUp = false;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W) ||
				currentGamePadState.DPad.Up == ButtonState.Pressed)
			{
				player.Position.Y -= playerMoveSpeed;
				isHoriz = false;
				isDown = false;
				isUp = true;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S) ||
				currentGamePadState.DPad.Down == ButtonState.Pressed)
			{
				player.Position.Y += playerMoveSpeed;
				isHoriz = false;
				isDown = true;
				isUp = false;
			}
			setPlayerBounds ();
		}
		#endregion

		#region Player Region
		// Sets the player bounds position
		private void setPlayerBounds()
		{
			player.Position.X = MathHelper.Clamp(player.Position.X, -15,GraphicsDevice.Viewport.Width - player.Width);
			player.Position.Y = MathHelper.Clamp(player.Position.Y, 0,GraphicsDevice.Viewport.Height - player.Height);
		}
		#endregion

		#region Updates the Game (Every 60 Seconds)
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		protected override void Update (GameTime gameTime)
		{
			//Exits the game if Escape is Pressed
			if (Keyboard.GetState ().IsKeyDown (Keys.Escape)) 
			{	Exit ();	}
            
			// Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
			previousGamePadState = currentGamePadState;
			previousKeyboardState = currentKeyboardState;

			// Read the current state of the keyboard and gamepad and store it
			currentKeyboardState = Keyboard.GetState();
			currentGamePadState = GamePad.GetState(PlayerIndex.One);


			//Update the player
			UpdatePlayer(gameTime);
            
			base.Update (gameTime);
		}
		#endregion
			
		#region Puts everything on display (Last Method Call)
		/// This is called when the game should draw itself.
		protected override void Draw (GameTime gameTime)
		{
			//Sets the background color
			graphics.GraphicsDevice.Clear (Color.AntiqueWhite);

			// Start drawing
			spriteBatch.Begin();

			// Draw the Player
			player.Draw(spriteBatch, isHoriz, isDown, isUp);

			// Stop drawing
			spriteBatch.End();
            
			//Re-draws the screen every time it updates
			base.Draw (gameTime);
		}
		#endregion
	}
}

