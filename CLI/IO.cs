using Chess.Move;

namespace Chess.Game;

public class GameInputOutput
{
    public static Coordinate? GetUserInput(string? message = null)
    {
        PrintMessage(message);
        var input = Console.ReadLine();

        if (input is null) return null;

        return ParseCoordinate(input);
    }

    public static void PrintMessage(string? message = null)
    {
        Console.WriteLine(message);
    }

    private static Coordinate? ParseCoordinate(string input)
    {
        if (input.Trim().IsWhiteSpace())
        {
            return null;
        }

        var coords = input.Replace(" ", null).Split(",");
        if (coords.Length != 2) return null;

        if (!int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y))
        {
            return null;
        }

        return new Coordinate(x, y);
    }


}