using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace Codefarts.AutoDownloader
{
    /// <summary>
    /// Provides a helper class for MEF composition.
    /// </summary>
    public sealed class MEFHelpers
    {
        /// <summary>
        /// Composes MEF parts.
        /// </summary>
        /// <param name="parts">The composable object parts.</param>
        public static void Compose(params object[] parts)
        {
            Compose(null, parts);
        }

        /// <summary>
        /// Composes MEF parts.
        /// </summary>
        /// <param name="searchFolders">Provides a series of search folders to search for *.dll files.</param>
        /// <param name="parts">The composable object parts.</param>
        public static CompositionContainer Compose(IEnumerable<string> searchFolders, params object[] parts)
        {
            // setup composition container
            var catalog = new AggregateCatalog();

            // check if folders were specified
            if (searchFolders != null)
            {
                // add search folders
                foreach (var folder in searchFolders.Where(System.IO.Directory.Exists))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(folder, "*.dll"));
                }
            }

            catalog.Catalogs.Add(new ApplicationCatalog());

            // compose and create plug ins
            var composer = new CompositionContainer(catalog);
            composer.ComposeParts(parts);
            return composer;
        }
    }
}