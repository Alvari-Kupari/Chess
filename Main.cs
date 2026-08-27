using Chess.Game;
using Chess.Move;
using Chess.Pieces;

var failures = 0;

failures += RunTest(
    "empty source square",
    game => game.HandleTurn(new Coordinate(5, 0), new Coordinate(4, 0)),
    (game, messages) => messages.Contains(TurnResult.ERROR_SOURCE_IS_EMPTY),
    expectedMessages: [TurnResult.ERROR_SOURCE_IS_EMPTY]);

failures += RunTest(
    "white pawn advance",
    game => game.HandleTurn(new Coordinate(6, 0), new Coordinate(5, 0)),
    (game, messages) => messages.GetSize() == 0 && game.Turn == Colour.BLACK,
    expectedMessages: []);

failures += RunTest(
    "queen gives check",
    game =>
    {
        game.HandleTurn(new Coordinate(6, 3), new Coordinate(5, 3));
        game.HandleTurn(new Coordinate(1, 3), new Coordinate(2, 3));
        Console.WriteLine(game.Board.ToString());
        return game.HandleTurn(new Coordinate(7, 3), new Coordinate(1, 3));
    },
    (game, messages) => messages.Contains(TurnResult.CHECK_BLACK),
    expectedMessages: [TurnResult.CHECK_BLACK]);

Console.WriteLine($"{(failures == 0 ? "All tests passed." : $"{failures} test(s) failed.")}");

static int RunTest(
    string name,
    Func<ChessGame, ChessMessages> execute,
    Func<ChessGame, ChessMessages, bool> assert,
    IReadOnlyList<TurnResult> expectedMessages)
{
    var game = new ChessGame();
    var messages = execute(game);
    var passed = assert(game, messages);

    Console.WriteLine($"{(passed ? "PASS" : "FAIL")} {name}");
    Console.WriteLine($"  expected: {FormatMessages(expectedMessages)}");
    Console.WriteLine($"  actual: {messages}");

    return passed ? 0 : 1;
}

static string FormatMessages(IEnumerable<TurnResult> messages)
{
    var list = messages.ToList();
    return list.Count == 0 ? "(no messages)" : string.Join(", ", list.Select(message => message.ToString()));
}

