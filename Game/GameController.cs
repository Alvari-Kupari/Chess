namespace Chess.Game;

using Chess.Board;
using Chess.Pieces;

class GameController
{
    private Colour turn;
    private readonly ChessBoard board;
    private readonly ChessBoardInitializer initializer = new();
    private readonly Referee checkReferee;
    private readonly Mover pieceMover;

    public GameController()
    {
        turn = Colour.WHITE;
        board = new(initializer);
        checkReferee = new(board, initializer.WhiteKing, initializer.BlackKing);
        pieceMover = new(board);
    }

    public void Play()
    {
        while(!checkReferee.IsCheckMate(turn))
        {
            if (checkReferee.IsCheck(turn))
            {
                GameInputOutput.PrintMessage("Check!\n");
            }
            Console.WriteLine(board.ToString());
            GameInputOutput.PrintMessage($"{(turn == Colour.WHITE ? "Whites " : "Blacks")} turn:\n");
            var from = GameInputOutput.GetUserInput("Enter starting square: \n");
            if (from is null)
            {
                GameInputOutput.PrintMessage("Invalid starting square. Try again.\n");
                continue;
            }

            var source = board[from.Value];
            if (source is not null && source.Colour != turn)
            {
                GameInputOutput.PrintMessage("Piece is the wrong colour for the turn.");
                continue;
            }
            var to = GameInputOutput.GetUserInput("Enter destination square: \n");
            if (to is null)
            {
                GameInputOutput.PrintMessage("Invalid destination square. Try again.\n");
                continue;
            }
            try
            {
                pieceMover.MovePiece(from.Value, to.Value);
                turn = turn == Colour.BLACK ? Colour.WHITE : Colour.BLACK;
            } catch (Exception e)
            {
                GameInputOutput.PrintMessage(e.Message);
            }
        }

        GameInputOutput.PrintMessage("Game over!");
    }

}