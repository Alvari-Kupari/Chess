namespace Chess.CLI;

using Chess.Board;
using Chess.Game;
using Chess.Game.Actions;
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
        TurnResult result = TurnResult.NORMAL;
        while(!game.IsOver)
        {
            if (result.IsError())
            {
                GameInputOutput.PrintMessage("ERROR - invalid move");
            }
            if (result == TurnResult.CHECK)
            {
                GameInputOutput.PrintMessage("Check!");
            }
            Console.WriteLine(game.Board.ToString());
            GameInputOutput.PrintMessage($"{(game.Turn == Colour.WHITE ? "Whites " : "Blacks")} turn:");

            var action = GetMove();
            if (action is null) continue;

            result = game.HandleTurn(action);
        }

        GameInputOutput.PrintMessage("Game over!");

        if (result == TurnResult.STALEMATE)
        {
            GameInputOutput.PrintMessage("Stalemate!");
        } else
        {
            GameInputOutput.PrintMessage($"Checkmate! {game.Turn} wins!");
        }
    }

    private IAction? GetMove()
    {
        string input = GameInputOutput.GetUserInput("Enter move (help for commands): ").Trim().ToLower();

        if (input == "help")
        {
            GameInputOutput.PrintMessage(GetHelpMessage());
            return null;
        }    
        
        string[] parts = input.Split();

        if (parts.Length < 2)
        {
            GameInputOutput.PrintMessage("Not enough arguments given.");
            return null;
        }
        if (parts[0] == "castle" || parts[0] == "c")
        {
            if (parts[1] == "q" || parts[1] == "queenside")
            {
                return new CastlingAction(game.Turn, true);
            } else if (parts[1] == "kingside" || parts[1] == "k")
            {
                return new CastlingAction(game.Turn, false);
            } 
            GameInputOutput.PrintMessage("Bad request: specify either queenside or kingside");
            return null;
        }

        var from = parts[0].Trim();
        var to = parts[1].Trim();
        
        try
        {
            return new MovePieceAction(new Coordinate(from), new Coordinate(to));
        } catch
        {
            GameInputOutput.PrintMessage("Bad coordinates");
            return null;
        }
        throw new NotImplementedException();
    }

    private static string GetHelpMessage()
    {
        return """ 
        Normal move: [from] [to]
        Pawn promotion: [from] [to] {piece}
        Castling: castle (queenside / kingside)

        Where [coord] is either:
            geometric - e.g. 0,1
            algebraic - e.g a3

        And {piece} is either:
            queen
            bishop
            rook
            knight

        queenside can be shortened to q / k. Case doesn't matter for any of the above.
        """;
    }

}