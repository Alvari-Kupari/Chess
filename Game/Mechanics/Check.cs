namespace Chess.Game.Mechanics;

using Chess.Board;
using Chess.Move;
using Chess.Pieces;

public class Check(ChessBoard board, Colour turn)
{
    public bool IsCheckMate(bool IsCheck = true)
    {
        if (!IsCheck) return false;
        var (king, kingLocation) = GetCurrentKing();

        return king.GetPossibleMoves(kingLocation, board).Count == 0;
    }

    public bool IsCheck(IDictionary<ChessPiece, ISet<Coordinate>> possibleMoves) {
        var (king, kingLocation) = GetCurrentKing();

        foreach (var (piece, moves) in possibleMoves)
        {
            if (piece.Colour == king.Colour) continue;
            if (moves.Contains(kingLocation)) {
                return true;
            }
        }
        return false;
    }

    private (King, Coordinate) GetCurrentKing() {
        foreach (var (piece, location) in board)
        {
            if (piece is King king && piece.Colour == turn)
            {
                return (king, location);
            }
        }
        throw new InvalidProgramException("There should be a king on the board.");
    }
}