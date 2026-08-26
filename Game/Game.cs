namespace Chess.Game;

using Chess.Board;
using Chess.Move;
using Chess.Pieces;
using Chess.Game.Mechanics.Castling;

class ChessGame
{
    private Turn _turn;
    public ChessBoard Board {get;}
    private readonly ChessBoardInitializer initializer = new();
    private readonly Castling whiteCastling;
    private readonly Castling blackCastling;

    public ChessGame()
    {
        Board = new(initializer);
        whiteCastling = new(Board, Colour.WHITE);
        blackCastling = new(Board, Colour.BLACK);
        _turn = new(Board, Colour.WHITE);
    }

    public ChessMessages HandleTurn(Coordinate from, Coordinate to)
    {
        var response = _turn.Execute(from, to);
        if (!response.HasErrors())
        {
            _turn = new(Board, _turn.Colour.Opposite());
        }
        return response;
    }

}