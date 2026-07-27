namespace Chess.Board;

public class Mover(ChessBoard board)
{
    public void MovePiece((int, int) from, (int, int) to)
    {
        if (board.IsOutsideBounds(from))
        {
            throw new InvalidOperationException("Out of bounds coordinate given");
        }
        var sourcePiece = board[from] ?? throw new InvalidOperationException("No piece at the source position.");

        var possibleMoves = sourcePiece.GetPossibleMoves(from, board);

        if (!possibleMoves.Contains(to))
        {
            throw new InvalidOperationException("This piece can't make this move.");
        }

        board[to] = sourcePiece;
        board[from] = null;
        sourcePiece.OnMove(from, to, board);
        Console.WriteLine($"LOG: moving piece ({sourcePiece.Colour} {sourcePiece.GetType().Name}) from ({from.Item1}, {from.Item2}) to ({to.Item1}, {to.Item2})");
    }
}