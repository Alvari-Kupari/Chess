using Chess.Board;

namespace Chess.Pieces;

class Pawn(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♙';
    protected override char BlackSymbol => '♟';
    public bool HasMoved {get; set;} = false;
    public bool EnPassantEatable {get; set;} = false;

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        var moves = new HashSet<Coordinate>();
        Coordinate attackDirection = Colour == Colour.BLACK ? new Coordinate(1, 0) : new Coordinate(-1, 0);
        var forwardSquare = source + attackDirection;

        if (!board.IsOccupied(forwardSquare))
        {
            moves.Add(forwardSquare);
            var forwardTwoSquare = source + attackDirection * 2;

            if (!HasMoved && !board.IsOccupied(forwardTwoSquare))
            {
                moves.Add(forwardTwoSquare);
            }
        }

        var leftDiagonal = new Coordinate(forwardSquare.X, forwardSquare.Y - 1);
        var rightDiagonal = new Coordinate(forwardSquare.X, forwardSquare.Y + 1);

        if (!board.IsOutsideBounds(leftDiagonal) && IsEnemy(leftDiagonal, board)) {
            moves.Add(leftDiagonal);
        }

        if (!board.IsOutsideBounds(rightDiagonal) && IsEnemy(rightDiagonal, board))
        {
            moves.Add(rightDiagonal);
        }
        
        Coordinate leftAdjacent = new(source.X, source.Y - 1);
        Coordinate rightAdjacent = new(source.X, source.Y + 1);

        AddEnpassantMoves(leftAdjacent, attackDirection, moves, board);
        AddEnpassantMoves(rightAdjacent, attackDirection, moves, board);

        return moves;
    }

    private void AddEnpassantMoves(Coordinate enemyPawn, Coordinate attackDirection, ISet<Coordinate> moves, ChessBoard board)
    {
        if (board.IsOutsideBounds(enemyPawn) || board[enemyPawn] is not Pawn pawn) return;
        if (pawn.Colour == Colour || !pawn.EnPassantEatable) return;

        moves.Add(enemyPawn + attackDirection);
    }

}