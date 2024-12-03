using PetFamily.Core.Messaging;
using System.Threading.Channels;

namespace PetFamily.Core.MessageQueues;

public class InMemoryCleanerMessageQueue<TMessage> : IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();

    public async Task WriteAsync(TMessage message, CancellationToken token = default)
    {
        await _channel.Writer.WriteAsync(message, token);
    }

    public async Task<TMessage> ReadAsync(CancellationToken token = default)
    {
        return await _channel.Reader.ReadAsync(token);
    }
}
