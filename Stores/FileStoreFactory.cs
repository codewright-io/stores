using System;
using System.Collections.Concurrent;

namespace DevKnack.Stores
{
    public class FileStoreFactory : IFileStoreFactory
    {
        private static readonly ConcurrentDictionary<string, Type> _lookup = new ConcurrentDictionary<string, Type>();
        private static readonly ConcurrentDictionary<string, Type> _internalLookup = new ConcurrentDictionary<string, Type>();

        private readonly IServiceProvider _provider;

        public FileStoreFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IFileStore? CreateInternal(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            var splits = url.Split(':');

            _internalLookup.TryGetValue(splits[0], out Type fileStoreType);

            return _provider.GetService(fileStoreType) as IFileStore;
        }

        public IFileStore? Create(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            var splits = url.Split(':');

            _lookup.TryGetValue(splits[0], out Type fileStoreType);

            return _provider.GetService(fileStoreType) as IFileStore;
        }

        /// <summary>
        /// Register a file store for retreival by the filestore factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="protocol"></param>
        /// <param name="internalStore"></param>
        public static void RegisterFileStore<T>(string protocol, bool internalStore) where T : IFileStore
        {
            if (internalStore)
            {
                _internalLookup.TryAdd(protocol, typeof(T));
            }
            else
            {
                _lookup.TryAdd(protocol, typeof(T));
            }
        }
    }
}
