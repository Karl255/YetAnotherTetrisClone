using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NBodySim
{
	public class YetAnotherTetrisCloneGame : Game
	{
		private GraphicsDeviceManager Graphics;
		private SpriteBatch SpriteBatch;
		private Vector2 ScreenSize = new(0, 0);

		private SpriteFont GameFont;
		private MouseState PreviousMouseState;
		private KeyboardState PreviousKeyboardState;

		public YetAnotherTetrisCloneGame()
		{
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			ScreenSize.X = Graphics.PreferredBackBufferWidth = 100;
			ScreenSize.Y = Graphics.PreferredBackBufferHeight = 300;
			Graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			GameFont = Content.Load<SpriteFont>("gameFont");
		}

		protected override void Update(GameTime gameTime)
		{
			var mouseState = Mouse.GetState();
			var keyboardState = Keyboard.GetState();

			PreviousMouseState = mouseState;
			PreviousKeyboardState = keyboardState;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			SpriteBatch.Begin();
			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
