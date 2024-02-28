using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using NexusMods.Abstractions.Loadouts;
using NexusMods.Abstractions.Loadouts.Mods;
using NexusMods.Abstractions.Serialization.Attributes;
using NexusMods.App.UI.Pages.ViewModInfo.Types;
using NexusMods.App.UI.WorkspaceSystem;

namespace NexusMods.App.UI.Pages.ViewModInfo;

[JsonName("NexusMods.App.UI.Pages.ViewModInfo.ViewModInfoPageContext")]
public record ViewModInfoPageContext : IPageFactoryContext
{
    public required LoadoutId LoadoutId { get; init; }
    public required ModId ModId { get; init; }
    public required CurrentViewModInfoPage Page { get; init; }
}

[UsedImplicitly]
public class ViewModInfoPageFactory : APageFactory<IViewModInfoViewModel, ViewModInfoPageContext>
{
    public ViewModInfoPageFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public static readonly PageFactoryId StaticId = PageFactoryId.From(Guid.Parse("12ac717d-fa73-4dc0-a23b-17fd5410b065"));
    public override PageFactoryId Id => StaticId;

    public override IViewModInfoViewModel CreateViewModel(ViewModInfoPageContext context)
    {
        var vm = ServiceProvider.GetRequiredService<IViewModInfoViewModel>();
        vm.SetContext(context);
        return vm;
    }
}

