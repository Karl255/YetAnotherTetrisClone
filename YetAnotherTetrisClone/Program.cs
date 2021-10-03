using System;

namespace YetAnotherTetrisClone
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using var game = new YetAnotherTetrisCloneGame();
			game.Run();
		}
	}
}
