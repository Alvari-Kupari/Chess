using Chess.Board;
using Chess.Move;
using Chess.Pieces;

namespace Chess.Game.Mechanics.Castling;

public class Castling
{
    private readonly King king;
    private readonly Rook rookLeft;
    private readonly Rook rookRight;
    private readonly CastlingPath pathLeft;
    private readonly CastlingPath pathRight;

    public Castling(ChessBoard board, Colour colour)
    {
        Coordinate? kingLocation = null;
        Coordinate? rookLeftLocation = null;
        Coordinate? rookRightLocation = null;

        foreach (var (piece, location) in board)
        {
            if (piece.Colour != colour) continue;

            switch (piece)
            {
                case King k:
                    king = k;
                    kingLocation = location;
                    break;
    
                case Rook r when rookLeft is null:
                    rookLeft = r;
                    rookLeftLocation = location;
                    break;

                case Rook r:
                    rookRight = r;
                    rookRightLocation = location;
                    break;
            }
        }
        if (
            king is null || 
            kingLocation is null || 
            rookLeft is null || 
            rookLeftLocation is null || 
            rookRight is null || 
            rookRightLocation is null)
        {
            throw new Exception("Piece not found");
        }

        pathLeft = new(kingLocation, rookLeftLocation);
        pathRight = new(kingLocation, rookRightLocation);
    }

    public void AddMoves(ChessBoard board, IDictionary<ChessPiece, ISet<Coordinate>> existingMoves)
    {
        if (king.HasMoved || (rookLeft.HasMoved && rookRight.HasMoved)) return;

        var enemyTargetedSquares = existingMoves
            .Where(pieceMove => pieceMove.Key.Colour != king.Colour)
            .SelectMany(moves => moves.Value)
            .ToHashSet();

        if (!rookLeft.HasMoved && CanCastle(board, enemyTargetedSquares, pathLeft))
        {
            // add action
        }

        if (!rookRight.HasMoved && CanCastle(board, enemyTargetedSquares, pathRight))
        {
            // add action
        } 
    }

    private static bool CanCastle(ChessBoard board, ISet<Coordinate> enemyCoveredSquares, CastlingPath path)
    {
        // check if path is obstructed
        if (path.GetRequiredEmptySquares().Any(board.IsOccupied)) return false;

        // check if king's path is / goes through check
        if (path.GetKingsPath().Any(enemyCoveredSquares.Contains)) return false;

        return true;
    }
}