namespace YetAnotherTetrisClone
{
	public enum GameState
	{
		StartScreen, // draws background and title text
		Playing, // draws playfield, falling piece, score...
		Paused, // same as playing, but also draws the paused text
		Won,    // same as playing, but also draws the won text
		Lost    // same as playing, but also draws the lost text
	}
}