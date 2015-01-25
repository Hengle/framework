﻿
using System;
using System.Collections.Generic;
using ActionStreetMap.Infrastructure.Dependencies;
using ActionStreetMap.Infrastructure.Reactive;
using ActionStreetMap.Maps.Entities;
using ActionStreetMap.Maps.Sources;

namespace ActionStreetMap.Maps.Index.Search
{

    /// <summary>
    ///     Provides the way to find elements by given text parameters.
    /// </summary>
    public interface ISearchEngine
    {
        /// <summary>
        ///     Searches all elements with given key and similiar value in current active element source.
        /// </summary>
        /// <param name="key">Tag key.</param>
        /// <param name="value">Tag value.</param>
        /// <returns>Element collection.</returns>
        IEnumerable<Element> SearchByTag(string key, string value);
    }

    /// <summary>
    ///     Implementation of <see cref="ISearchEngine"/> which depends on default implementation of <see cref="IElementSource"/>.
    /// </summary>
    public class SearchEngine: ISearchEngine
    {
        private readonly IElementSourceProvider _elementSourceProvider;

        /// <summary>
        ///     Creates instance of <see cref="SearchEngine"/>
        /// </summary>
        /// <param name="elementSourceProvider">Element source provider.</param>
        [Dependency]
        public SearchEngine(IElementSourceProvider elementSourceProvider)
        {
            _elementSourceProvider = elementSourceProvider;
        }

        /// <inheritdoc />
        public IEnumerable<Element> SearchByTag(string key, string value)
        {
            var elementSource = GetElementSource();
            foreach (var pair in elementSource.KvStore.Search(new KeyValuePair<string, string>(key, value)))
            {
                var kvOffset = elementSource.KvIndex.GetOffset(pair);
                var usageOffset = elementSource.KvStore.GetUsage(kvOffset);
                var offsets = elementSource.KvUsage.Get(usageOffset);
                foreach (var offset in offsets)
                    yield return elementSource.ElementStore.Get(offset);
            }
        }

        private LocalElementSource GetElementSource()
        {
            var elementSource = _elementSourceProvider.Get().Wait() as LocalElementSource;
            if (elementSource == null)
                throw new NotSupportedException(Strings.SearchNotSupported);
            return elementSource;
        }
    }
}
