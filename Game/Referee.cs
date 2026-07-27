namespace Chess.Game;

using Chess.Board;
using Chess.Pieces;

public class Referee(ChessBoard board, King WhiteKing, King BlackKing)
{
    public bool IsCheckMate(Colour turn)
    {
        if (!IsCheck(turn)) return false;
        var (king, kingLocation) = GetCurrentKing(turn);

        return king.GetPossibleMoves(kingLocation, board).Count == 0;
    }

    public bool IsCheck(Colour turn) {
        var (_, kingLocation) = GetCurrentKing(turn);

        foreach (var piece in board)
        {
            if (piece is King) continue;
            var location = board.FindPiece(piece) ?? throw new InvalidProgramException("Piece should exist.");
            if (piece.GetPossibleMoves(location, board).Contains(kingLocation)) {
                return true;
            }
        }
        return false;
    }

    private (King, (int, int)) GetCurrentKing(Colour turn) {
        var king = turn == Colour.WHITE ? WhiteKing : BlackKing;
        return (king, board.FindPiece(king) ?? throw new InvalidProgramException("There should be a king on the board."));
    }
}