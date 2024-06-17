using NexusMods.Abstractions.GameLocators;
using NexusMods.Abstractions.GameLocators.Stores.Steam;
using NexusMods.Abstractions.Games.DTO;
using NexusMods.Abstractions.IO;
using NexusMods.Abstractions.IO.StreamFactories;
using NexusMods.Paths;

namespace NexusMods.Games.BethesdaGameStudios.Fallout4;

public class Fallout4(IServiceProvider provider) : ABethesdaGame(provider), ISteamGame
{
    public override string Name => "Fallout 4";

    public static GameDomain StaticDomain => GameDomain.From("fallout4");

    public override GameDomain Domain => StaticDomain;
    public override GamePath GetPrimaryFile(GameStore store) => new(LocationId.Game, "Fallout4.exe");

    protected override IReadOnlyDictionary<LocationId, AbsolutePath> GetLocations(IFileSystem fileSystem,
        GameLocatorResult installation)
    {
        return new Dictionary<LocationId, AbsolutePath>
        {
            { LocationId.Game, installation.Path },
            { LocationId.AppData, fileSystem.GetKnownPath(KnownPath.LocalApplicationDataDirectory).Combine("Fallout4") }
        };
    }

    public IEnumerable<uint> SteamIds => new[] { 377160u };

    public override IStreamFactory Icon =>
        new EmbededResourceStreamFactory<Fallout4>("NexusMods.Games.BethesdaGameStudios.Resources.Fallout4.icon.png");

    public override IStreamFactory GameImage =>
        new EmbededResourceStreamFactory<Fallout4>("NexusMods.Games.BethesdaGameStudios.Resources.Fallout4.game_image.jpg");
}
