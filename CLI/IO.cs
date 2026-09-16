
using System.Text;
using Chess.Board;

namespace Chess.CLI;

public class GameInputOutput
{
    public static string GetUserInput(string? message = null)
    {
        PrintMessage(message);
        return Console.ReadLine() ?? "";
    }

    public static void PrintMessage(string? message = null)
    {
        Console.WriteLine(message);
    }

    // public static void PrintBoard(ChessBoard board)
    // {
    //     StringBuilder sb = new();

    //     for (int i = 0; i < board.GetBoundary(0); i++)
    //     {
    //         StringBuilder row = new();
    //         for (int j = 0; j < board.GetBoundary(1); j++)
    //         {
    //             var piece = board[new Coordinate(i, j)];
    //             row.Append(piece?.Symbol ?? ' ');
    //         }
    //         sb.Append(row);
    //         sb.Append('\n');
    //     }
    //     PrintMessage(sb.ToString());
    // }

    public static void PrintBoard(ChessBoard board)
    {
        var separator = "  +---+---+---+---+---+---+---+---+";
        StringBuilder sb = new();

        sb.Append(separator);

        for (int i = 0; i < board.GetBoundary(0); i++)
        {
            StringBuilder row = new($"\n{board.GetBoundary(0) - i} |");

            for (int j = 0; j < board.GetBoundary(1); j++)
            {
                char symbol = board[new Coordinate(i, j)]?.Symbol ?? ' ';
                row.Append($" {symbol} |");
            }
            row.Append($"\n{separator}");
            sb.Append(row);
        }
        sb.Append("\n    a   b   c   d   e   f   g   h  ");
        PrintMessage(sb.ToString());
    }


}