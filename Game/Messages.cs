namespace Chess.Game;

public class ChessMessages
{
    private readonly HashSet<ChessMessage> messages;
    public ChessMessages()
    {
        messages = [];
    }

    public ChessMessages Append(ChessMessage message)
    {
        messages.Add(message);
        return this; 
    }

    public bool HasErrors()
    {
        return messages.Any((msg) => msg.IsError());
    }

    public int GetSize()
    {
        return messages.Count;
    }

    public bool Contains(ChessMessage message)
    {
        return messages.Contains(message);
    }

    public IEnumerable<ChessMessage> GetMessages()
    {
        return messages;
    }

    public bool IsCheckMate()
    {
        return messages.Contains(ChessMessage.CHECKMATE);
    }

    public bool IsCheck()
    {
        return messages.Contains(ChessMessage.CHECK);
    }

    public override string ToString()
    {
        return messages.Count > 0 ? string.Join(", ", messages.Select(message => message.ToString())) : "(no messages)";
    }
}