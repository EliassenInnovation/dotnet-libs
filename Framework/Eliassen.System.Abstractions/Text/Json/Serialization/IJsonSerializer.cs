using Nucleus.Dataloader.Cli;

namespace Eliassen.System.Text.Json.Serialization;

/// <summary>
/// interface to identify shared JSON serialization process.
/// </summary>
public interface IJsonSerializer : ISerializer
{
        string AsPropertyName(string propertyName);
}
