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
		private KeyboardState PreviousKeyboardState;

		private Random Rng { get; init; } = new();
		private GameState GameState = GameState.StartScreen;
		private (bool occupied, Color color)[,] Playfield;
		private bool[,] FallingPiece = Tetrominos.O;
		private Color FallingPieceColor;
		private (int X, int Y) FallingPiecePosition = new(0, 0);

		private TimeSpan FallStepTime;
		private TimeSpan LeftKeyFired;
		private TimeSpan RightKeyFired;
		private TimeSpan DownKeyFired;

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
			var keyboardState = Keyboard.GetState();
			var totalGameTime = gameTime.TotalGameTime;

			switch (GameState)
			{
				case GameState.StartScreen:
					if (keyboardState.IsKeyDown(Keys.Enter))
					{
						FallStepTime = gameTime.TotalGameTime;
						NewGame();
						GameState = GameState.Playing;
						NextPiece();
					}

					break;

				case GameState.Playing:
					// left
					if (keyboardState.IsKeyDown(Keys.Left) // key
						&& (totalGameTime - LeftKeyFired).TotalMilliseconds >= 100 // delay
						&& !TestMoveCollideFallingPiece(-1, 0)) // colision
					{
						FallingPiecePosition.X--;
						LeftKeyFired = totalGameTime;
					}

					// right
					if (keyboardState.IsKeyDown(Keys.Right) // key
						&& (totalGameTime - RightKeyFired).TotalMilliseconds >= 100 // delay
						&& !TestMoveCollideFallingPiece(1, 0)) // colision
					{
						FallingPiecePosition.X++;
						RightKeyFired = totalGameTime;
					}

					if (keyboardState.IsKeyDown(Keys.Down) // key
						&& (totalGameTime - DownKeyFired).TotalMilliseconds >= 50) // delay
					{
						if (TestMoveCollideFallingPiece(0, -1)) // colision
							PlaceAndNextPiece();
						else
						{
							FallingPiecePosition.Y--;
							DownKeyFired = totalGameTime;
							FallStepTime = gameTime.TotalGameTime; // supress auto-falling
						}
					}

					if ((totalGameTime - FallStepTime).TotalMilliseconds >= 500)
					{
						if (TestMoveCollideFallingPiece(0, -1))
							PlaceAndNextPiece();
						else
							FallingPiecePosition.Y--;

						FallStepTime = gameTime.TotalGameTime;
					}

					break;

				case GameState.Lost:

					break;

				case GameState.Won:

					break;
			}

			PreviousKeyboardState = keyboardState;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// TODO: switch to overlay color blending for block coloring
			SpriteBatch.Begin();

			if (GameState == GameState.StartScreen)
			{
				string text = "Press Enter to start";
				SpriteBatch.DrawString(GameFont, text, new(5 * BlockSize - GameFont.MeasureString(text).X / 2, 90), Color.White);
			}
			else
			{
				for (int y = 0; y < 20; y++)
					for (int x = 0; x < 10; x++)
						if (Playfield[x, y].occupied)
						{
							SpriteBatch.Draw(
								Block,
								new Vector2(
									x * BlockSize,
									(19 - y) * BlockSize),
								Playfield[x, y].color);
						}

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
									(19 - FallingPiecePosition.Y + y) * BlockSize),
								FallingPieceColor);
						}
			}

			//SpriteBatch.DrawString(GameFont, $"({FallingPiecePosition.X:0}, {FallingPiecePosition.Y:0})", new(), Color.White);

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		// utility methods

		private void NewGame()
		{
			Playfield = new (bool, Color)[10, 24]; // visually capped to 20 height
		}

		private void NextPiece()
		{
			(FallingPiece, FallingPieceColor) = Tetrominos.All[Rng.Next(Tetrominos.All.Length)];
			int width = FallingPiece.GetLength(1);

			FallingPiecePosition.X = 10 / 2 - width / 2; // centering
			FallingPiecePosition.Y = width < 4 ? 19 : 20; // for the I piece so it touches the ceiling
		}

		private void PlaceAndNextPiece()
		{
			int fallingWidth = FallingPiece.GetLength(1);
			int fallingHeight = FallingPiece.GetLength(0);

			for (int y = 0; y < fallingHeight; y++)
				for (int x = 0; x < fallingWidth; x++)
					if (FallingPiece[x, y])
					{
						Playfield[FallingPiecePosition.X + x, FallingPiecePosition.Y - y] = (true, FallingPieceColor);
					}


			NextPiece();
		}

		private bool TestMoveCollideFallingPiece(int dx, int dy)
		{
			(int x, int y) position = (
				FallingPiecePosition.X + dx,
				FallingPiecePosition.Y + dy
			);

			int fallingWidth = FallingPiece.GetLength(1);
			int fallingHeight = FallingPiece.GetLength(0);

			// collide with blocks in the playing field
			for (int y = 0; y < fallingHeight; y++)
				for (int x = 0; x < fallingWidth; x++)
					if (FallingPiece[x, y])
					{
						// collide block of piece with boundaries
						if (position.x + x < 0 || position.x + x >= 10 ||
							position.y - y < 0 || position.y - y >= 24)
							return true;

						// collide block of piece with playfield
						if (Playfield[position.x + x, position.y - y].occupied)
							return true;
					}

			return false;
		}
	}
}
