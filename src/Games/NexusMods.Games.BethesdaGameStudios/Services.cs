using Microsoft.Extensions.DependencyInjection;
using NexusMods.Abstractions.Games;
using NexusMods.Abstractions.Loadouts;
using NexusMods.Abstractions.Loadouts.Synchronizers;
using NexusMods.Abstractions.Serialization.ExpressionGenerator;
using NexusMods.Extensions.DependencyInjection;
using NexusMods.Games.BethesdaGameStudios.SkyrimLegendaryEdition;
using NexusMods.Games.BethesdaGameStudios.SkyrimSpecialEdition;
using NexusMods.Games.BethesdaGameStudios.Fallout4;

namespace NexusMods.Games.BethesdaGameStudios;

public static class Services
{
    public static IServiceCollection AddBethesdaGameStudios(this IServiceCollection services) =>
        services.AddGame<SkyrimSpecialEdition.SkyrimSpecialEdition>()
            .AddGame<SkyrimLegendaryEdition.SkyrimLegendaryEdition>()
            .AddGame<Fallout4.Fallout4>()
            .AddSingleton<ITool, SkyrimLegendaryEditionGameTool>()
            .AddSingleton<ITool, SkyrimSpecialEditionGameTool>()
            .AddSingleton<ITool, Fallout4GameTool>()
            .AddGeneratedFile<PluginOrderFile>()
            .AddSingleton<PluginAnalyzer>()
            .AddSingleton<PluginSorter>();
}
