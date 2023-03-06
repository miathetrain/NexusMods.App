﻿using System.Resources;
using NexusMods.Common;
using NexusMods.Paths;
using NexusMods.Paths.Extensions;

namespace NexusMods.FileExtractor.StreamFactories;

/// <summary>
/// A Stream Factory that's backed by an embeded resource.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EmbededResourceStreamFactory<T> : IStreamFactory
{
    private readonly string _path;

    /// <summary>
    /// Factory for a stream backed by an embeded resource.
    /// </summary>
    /// <param name="path">Full path to the embedded resource</param>
    public EmbededResourceStreamFactory(string path)
    {
        _path = path;
    }

    /// <summary>
    /// Always returns DateTime.MinValue
    /// </summary>
    public DateTime LastModifiedUtc => DateTime.MinValue;

    /// <summary>
    /// Returns the name of the resource as a relative path.
    /// </summary>
    public IPath Name => _path.ToRelativePath();

    /// <summary>
    /// Returns the size of the resource.
    /// </summary>
    /// <exception cref="MissingManifestResourceException"></exception>
    public Size Size

    {
        get
        {
            using var info = typeof(T).Assembly.GetManifestResourceStream(_path);
            if (info == null)
                throw new MissingManifestResourceException($"Could not find {_path}");
            return Size.From(info.Length);
        }
    }

    /// <summary>
    /// Returns a stream to the resource.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="MissingManifestResourceException"></exception>
    public ValueTask<Stream> GetStreamAsync()
    {
        var result = typeof(T).Assembly.GetManifestResourceStream(_path);
        if (result == null)
            throw new MissingManifestResourceException($"Could not find {_path}");

        return ValueTask.FromResult(result);
    }
}