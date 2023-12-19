﻿using Eliassen.MessageQueueing;
using Eliassen.MessageQueueing.Services;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace Eliassen.WebApi.Provider
{
    public interface IExampleMessageProvider
    {
        Task<string> PostAsync(object message, string? correlationId);
    }
    public class ExampleMessageProvider : IExampleMessageProvider, IMessageHandler<ExampleMessageProvider>
    {
        private readonly IMessageSender _sender;
        private readonly ILogger _logger;

        public ExampleMessageProvider(
            IMessageSender<ExampleMessageProvider> sender,
            ILogger<ExampleMessageProvider> logger
            )
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task<string> PostAsync(object message, string? correlationId)
        {
            _logger.LogInformation("Send: {message} [{correlationId}]", message, correlationId);
            var messageId = await _sender.SendAsync(message, correlationId);
            _logger.LogInformation("Sent: {message} [{correlationId}]-[{messageId}]", message, correlationId, messageId);
            return messageId;
        }

        public Task HandleAsync(object message, IMessageContext context)
        {
            throw new NotImplementedException();
        }
    }
}
