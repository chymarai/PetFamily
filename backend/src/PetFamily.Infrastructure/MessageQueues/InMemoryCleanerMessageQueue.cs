using PetFamily.Application.FileProvider;
using PetFamily.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.MessageQueues;

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
