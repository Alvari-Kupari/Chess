using Chess.Board;
using Chess.Pieces;

namespace Chess.Game.Mechanics;

public class ValidMoves
{
    public static bool HasValidMoves(ChessBoard board, MoveRegistry moves, Coordinate friendlyKingLocation, Colour turn)
    {
        // find out if they have valid moves
        foreach (var (enemyPiece, possibleEnemyMoves) in moves)
        {
            if (enemyPiece.Colour == turn) continue;

            Coordinate pieceLocation = board.FindPiece(enemyPiece) ?? throw new Exception("Piece not found.");
            foreach (var possibleMove in possibleEnemyMoves)
            {
                // try the move
                using PieceMove trialMove = new(pieceLocation, possibleMove, board);
                MoveRegistry trialMoves = new(board);
                var enemyCoveredSquares = trialMoves.GetByColour(turn.Opposite());

                if (!enemyCoveredSquares.Contains(friendlyKingLocation))
                {
                    // valid move
                    return true;
                }

            }
        }

        return false;
    }
}