using System.Collections.ObjectModel;
using DynamicData;
using NexusMods.Abstractions.GameLocators;
using NexusMods.Abstractions.Games.DTO;
using NexusMods.Abstractions.IO;
using NexusMods.Abstractions.Loadouts;
using NexusMods.Abstractions.Loadouts.Files;
using NexusMods.Abstractions.Loadouts.Mods;
using NexusMods.App.UI.Controls.Trees.Files;
using NexusMods.Paths.Trees.Traits;
using ModFileNode = NexusMods.App.UI.TreeNodeVM<NexusMods.App.UI.Controls.Trees.Files.IFileTreeNodeViewModel, NexusMods.Abstractions.GameLocators.GamePath>;
namespace NexusMods.App.UI.Controls.ModInfo.ViewModFiles;

public class ViewModFilesViewModel : AViewModel<IViewModFilesViewModel>, IViewModFilesViewModel
{
    private readonly ILoadoutRegistry _registry;
    private readonly IFileStore _fileStore;
    private readonly SourceCache<IFileTreeNodeViewModel, GamePath> _sourceCache;
    private ReadOnlyObservableCollection<ModFileNode> _items;
    private int _rootCount;
    private string? _primaryRootLocation;

    public ReadOnlyObservableCollection<ModFileNode> Items => _items;
    public int RootCount => _rootCount;
    public string? PrimaryRootLocation => _primaryRootLocation;

    public ViewModFilesViewModel(ILoadoutRegistry registry, IFileStore fileStore)
    {
        _registry = registry;
        _fileStore = fileStore;
        _items = new ReadOnlyObservableCollection<ModFileNode>([]);
        _sourceCache = new SourceCache<IFileTreeNodeViewModel, GamePath>(x => x.FullPath);
    }

    public void Initialize(LoadoutId loadoutId, List<ModId> contextModIds)
    {
        // Note: The code below only shows the 'raw' files, not as deployed in the case of multi-select.
        //       This is because games can use custom synchronizers, which means custom sort rules,
        //       so functionality such as 'get me sorted mods' can vary with game and need to instead
        //       be implemented in a loadout synchronizer.
        //
        //       In the UI, we will need some sort of warning that this does not represent the 'final' state.
        
        // Fetch all the files.
        var dict = new Dictionary<GamePath, ModFilePair>();
        var availableLocations = new HashSet<LocationId>();
        foreach (var modId in contextModIds)
        {
            var mod = _registry.Get(loadoutId, modId);
            if (mod == null)
                continue;

            foreach (var (_, file) in mod.Files)
            {
                // TODO: Check for IStoredFile, IToFile interfaces if we ever have more types of files that get put to disk.
                if (file is not StoredFile storedFile)
                    continue;
                
                dict[storedFile.To] = new ModFilePair { Mod = mod, File = file };
                availableLocations.Add(storedFile.To.LocationId);
            }
        }

        // Add them to the cache.
        var allItems = FlattenedLoadout.Create(dict).GetAllDescendents().ToArray();
        
        var displayedItems = new List<IFileTreeNodeViewModel>();
        var hashToFileSize = new Dictionary<ulong, long>();
        foreach (var x in allItems)
        {
            if (!x.Item.IsFile)
                continue;
            
            var storedFile = (StoredFile)x.Item.Value.File;
            
            // TODO: Optimize fetching file sizes, this is pretty inefficient for very large mods.
            var fileSize = _fileStore.GetFileStream(storedFile.Hash).Result.Length;
            hashToFileSize[storedFile.Hash.Value] = fileSize;
            displayedItems.Add(new FileTreeNodeViewModel<ModFilePair>(x, fileSize));
        }
        
        // Now calculate folder sizes.
        foreach (var x in allItems)
        {
            if (x.Item.IsFile)
                continue;

            var fileSize = (long)0;
            foreach (var child in x.EnumerateChildrenBfs())
            {
                var storedFile = (StoredFile)child.Value.Item.Value.File;
                fileSize += hashToFileSize[storedFile.Hash.Value];
            }
            displayedItems.Add(new FileTreeNodeViewModel<ModFilePair>(x, fileSize));
        }
 
        _sourceCache.Clear();
        _sourceCache.AddOrUpdate(displayedItems);
        
        // Resolve folder locations.
        var namedLocations = new Dictionary<LocationId, string>();
        var loadout = _registry.Get(loadoutId);
        var register = loadout!.Installation.LocationsRegister;
        foreach (var location in availableLocations)
            namedLocations.Add(location, register[location].ToString());
        
        // Flatten them with DynamicData
        BindItems(_sourceCache, namedLocations, false, out _items, out _rootCount, out _primaryRootLocation);
    }
    
    /// <summary>
    ///     Binds all items in the given cache.
    ///     If the items have multiple roots (LocationIds), separate nodes are made for them.
    /// </summary>
    internal static void BindItems(
        SourceCache<IFileTreeNodeViewModel, GamePath> cache, 
        Dictionary<LocationId, string> locations, 
        bool alwaysRoot, 
        out ReadOnlyObservableCollection<ModFileNode> result,
        out int rootCount,
        out string? primaryRootLocation)
    {
        // AlwaysRoot is left as a parameter because it may be a user preference in settings in the future.
        // If there's more than 1 location, create dummy nodes.
        rootCount = locations.Count;
        var hasMultipleRoots = (alwaysRoot || locations.Count > 1); 
        if (hasMultipleRoots)
        {
            foreach (var location in locations)
                cache.AddOrUpdate(new FileTreeNodeDesignViewModel(false, new GamePath(location.Key, ""), location.Value));

            primaryRootLocation = null;
        }
        else
        {
            primaryRootLocation = locations.First().Value;
        }
        
        cache.Connect()
            .TransformToTree(model => model.ParentPath)
            .Transform(node => new ModFileNode(node))
            .Bind(out result)
            .Subscribe(); // force evaluation
    }
}
