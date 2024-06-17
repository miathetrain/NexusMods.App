using Microsoft.Extensions.Logging;
using NexusMods.Abstractions.GameLocators;
using NexusMods.Abstractions.Games;
using NexusMods.CrossPlatform.Process;

namespace NexusMods.Games.BethesdaGameStudios.Fallout4;

public class Fallout4GameTool : RunGameWithScriptExtender<Fallout4>
{
    public Fallout4GameTool(IServiceProvider serviceProvider, Fallout4 game)
        : base(serviceProvider, game) { }

    protected override GamePath ScriptLoaderPath => new(LocationId.Game, "f4se_loader.exe");
}
