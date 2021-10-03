namespace YetAnotherTetrisClone
{
	public static class Tetrominos
	{
		public static bool[,] I =
		{
			{ false, true , false, false },
			{ false, true , false, false },
			{ false, true , false, false },
			{ false, true , false, false }
		};

		public static bool[,] O =
		{
			{ true, true },
			{ true, true }
		};

		public static bool[,] T =
		{
			{ false, true , false },
			{ true , true , false },
			{ false, true , false }
		};

		public static bool[,] S =
		{
			{ true , false, false },
			{ true , true , false },
			{ false, true , false }
		};

		public static bool[,] Z =
		{
			{ false, true , false },
			{ true , true , false },
			{ true , false, false }
		};

		public static bool[,] J =
		{
			{ false, true , false },
			{ false, true , false },
			{ true , true , false }
		};

		public static bool[,] L =
		{
			{ true , true , false },
			{ false, true , false },
			{ false, true , false }
		};

		public static bool[][,] All = { I, O, T, S, Z, J, L };
	}
}
