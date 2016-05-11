using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Model
{
	public class Player
	{
		#region Declaration Section
		private SpriteBatch spriteBatch;
		private int score;
		private bool active;
		private int health;

		// Animation representing the player
		public Texture2D PlayerTexture;

		// Position of the Player relative to the upper left side of the screen
		public Vector2 Position;
		#endregion

		#region Variable Properties (Getters/Setters)
		//Properties for Variables
		public bool Active
		{
			get{ return active; }
			set{ active = value; }
		}

		public int Health 
		{
			get{ return health; }
			set{ health = value; }
		}

		public int Width
		{
			get { return PlayerTexture.Width; }
		}

		public int Height
		{
			get { return PlayerTexture.Height; }
		}

		public int Score
		{
			get{ return score; }
			set{ score = value; }
		}
		#endregion

		#region Initialize
		public void Initialize(Texture2D texture, Vector2 position)
		{
			PlayerTexture = texture; 
			// Set the starting position of the player around the middle of the screen and to the back
			Position = position;

			// Set the player to be active
			active = true;

			// Set the player health
			health = 100;

			//Set the player score
			score = 0;
		}
		#endregion

		public void Draw(SpriteBatch spriteBatch, bool isHoriz, bool isDown, bool isUp)
		{ 
			if(isHoriz == true)
				spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
			if (isDown == true)
				spriteBatch.Draw (PlayerTexture, Position, null, Color.White, 1.6f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			if (isUp == true)
				spriteBatch.Draw (PlayerTexture, Position, null, Color.White, -1.6f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			if(isHoriz == false && isDown == false && isUp == false)
				spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}

