using Microsoft.Xna.Framework;

namespace YetAnotherTetrisClone
{
	public static class Tetrominos
	{
		public static Color IColor = new Color(15, 255, 255);
		public static bool[,] I =
		{
			{ false, true , false, false },
			{ false, true , false, false },
			{ false, true , false, false },
			{ false, true , false, false }
		};

		public static Color OColor = new Color(255, 255, 0);
		public static bool[,] O =
		{
			{ true, true },
			{ true, true }
		};

		public static Color TColor = new Color(229, 0, 229);
		public static bool[,] T =
		{
			{ false, true , false },
			{ true , true , false },
			{ false, true , false }
		};

		public static Color SColor = new Color(0, 255, 0);
		public static bool[,] S =
		{
			{ true , false, false },
			{ true , true , false },
			{ false, true , false }
		};

		public static Color ZColor = new Color(255, 0, 0);
		public static bool[,] Z =
		{
			{ false, true , false },
			{ true , true , false },
			{ true , false, false }
		};

		public static Color JColor = new Color(0, 0, 255);
		public static bool[,] J =
		{
			{ false, true , false },
			{ false, true , false },
			{ true , true , false }
		};

		public static Color LColor = new Color(255, 143, 0);
		public static bool[,] L =
		{
			{ true , true , false },
			{ false, true , false },
			{ false, true , false }
		};

		public static (bool[,], Color)[] All = {
			(I, IColor),
			(O, OColor),
			(T, TColor),
			(S, SColor),
			(Z, ZColor),
			(J, JColor),
			(L, LColor)
		};
	}
}
