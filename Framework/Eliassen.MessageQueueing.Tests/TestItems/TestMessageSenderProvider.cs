﻿using Eliassen.MessageQueueing.Services;
using Eliassen.TestUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Eliassen.MessageQueueing.Tests.TestItems;

public class TestMessageSenderProvider : IMessageSenderProvider
{
    public const string ProviderName = "test-provider";

    private readonly TestContext _context;
    private readonly ILogger _logger;

    public TestMessageSenderProvider(
        TestContext context,
        ILogger<TestMessageSenderProvider> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public Task<string?> SendAsync(object message, IMessageContext context)
    {
        _logger.LogInformation($"{nameof(SendAsync)}({{{nameof(message)}}}, {{{nameof(context)}}})", message, context);
        _context.AddResult(message, fileName: nameof(message));
        _context.AddResult(context, fileName: nameof(context)); 
        return Task.FromResult((string?)null);
    }
}
