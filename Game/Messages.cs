using System.Collections.Generic;
using System.Linq;

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
        return messages.Any(msg => msg.IsCheckMate());
    }

    public bool IsCheck()
    {
        return messages.Any(msg => msg.IsCheck());
    }

    public override string ToString()
    {
        return messages.Any() ? string.Join(", ", messages.Select(message => message.ToString())) : "(no messages)";
    }
}