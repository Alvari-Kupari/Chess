
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

    public static void PrintBoard(ChessBoard board)
    {
        var separator = "  +---+---+---+---+---+---+---+---+";
        StringBuilder sb = new();

        sb.Append(separator);

        for (int i = 0; i < board.XLength; i++)
        {
            StringBuilder row = new($"\n{board.XLength - i} |");

            for (int j = 0; j < board.YLength; j++)
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