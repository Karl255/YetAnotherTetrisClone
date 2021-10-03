using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace YetAnotherTetrisClone
{
	public class YetAnotherTetrisCloneGame : Game
	{
		private GraphicsDeviceManager Graphics;
		private SpriteBatch SpriteBatch;
		private Vector2 ScreenSize = new(0, 0);

		private Texture2D Block;
		private int BlockSize;

		private SpriteFont GameFont;
		private MouseState PreviousMouseState;
		private KeyboardState PreviousKeyboardState;

		private Random Rng = new();
		private GameState GameState = GameState.StartScreen;
		private bool[,] Playfield = new bool[10, 24]; // visually capped to 20 height
		private bool[,] FallingPiece = Tetrominos.O;
		private Vector2 FallingPiecePosition = new(0, 0);

		public YetAnotherTetrisCloneGame()
		{
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			Block = Content.Load<Texture2D>("block");
			BlockSize = Block.Width;
			GameFont = Content.Load<SpriteFont>("gameFont");

			ScreenSize.X = Graphics.PreferredBackBufferWidth = 10 * BlockSize;
			ScreenSize.Y = Graphics.PreferredBackBufferHeight = 20 * BlockSize;
			Graphics.ApplyChanges();
		}

		protected override void Update(GameTime gameTime)
		{
			var mouseState = Mouse.GetState();
			var keyboardState = Keyboard.GetState();

			switch (GameState)
			{
				case GameState.StartScreen:
					if (keyboardState.IsKeyDown(Keys.Enter))
					{
						GameState = GameState.Playing;
						NextPiece();
					}

					break;

				case GameState.Playing:
					if (PreviousKeyboardState.IsKeyUp(Keys.Enter) && keyboardState.IsKeyDown(Keys.Enter))
						NextPiece();

					break;

				case GameState.Lost:

					break;

				case GameState.Won:

					break;
			}

			PreviousMouseState = mouseState;
			PreviousKeyboardState = keyboardState;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			SpriteBatch.Begin();

			if (GameState == GameState.StartScreen)
			{
				string text = "Press Enter to start";
				SpriteBatch.DrawString(GameFont, text, new(5 * BlockSize - GameFont.MeasureString(text).X / 2, 90), Color.White);
			}
			else
			{
				int fallingWidth = FallingPiece.GetLength(1);
				int fallingHeight = FallingPiece.GetLength(0);

				for (int y = 0; y < fallingHeight; y++)
					for (int x = 0; x < fallingWidth; x++)
						if (FallingPiece[x, y])
						{
							SpriteBatch.Draw(
								Block,
								new Vector2(
									(FallingPiecePosition.X + x) * BlockSize,
									(20 - FallingPiecePosition.Y + y) * BlockSize),
								Color.Blue);
						}
			}

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		// utility methods

		private void NextPiece()
		{
			FallingPiece = Tetrominos.All[Rng.Next(Tetrominos.All.Length)];
			int width = FallingPiece.GetLength(1);

			FallingPiecePosition.X = 10 / 2 - width / 2; // centering
			FallingPiecePosition.Y = width < 4 ? 20 : 21; // for the I piece so it touches the ceiling
		}
	}
}
