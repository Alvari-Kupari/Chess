namespace Chess.Game;

using Chess.Pieces;

class GameController
{
    private readonly ChessGame game;
    public GameController()
    {
        game = new();
    }

    public void Play()
    {
        ChessMessages messages = new();

        while(!messages.IsCheckMate())
        {
            if (messages.IsCheck())
            {
                GameInputOutput.PrintMessage("Check!\n");
            }
            Console.WriteLine(game.Board.ToString());
            GameInputOutput.PrintMessage($"{(game.Turn == Colour.WHITE ? "Whites " : "Blacks")} turn:\n");
            var from = GameInputOutput.GetUserInput("Enter starting square: \n");
            if (from is null)
            {
                GameInputOutput.PrintMessage("Invalid starting square. Try again.\n");
                continue;
            }


            var to = GameInputOutput.GetUserInput("Enter destination square: \n");
            if (to is null)
            {
                GameInputOutput.PrintMessage("Invalid destination square. Try again.\n");
                continue;
            }
            messages = game.HandleTurn(from, to);
        }

        GameInputOutput.PrintMessage("Game over!");
    }

}