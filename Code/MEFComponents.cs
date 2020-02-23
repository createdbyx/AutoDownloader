using System.Windows;

namespace Codefarts.AutoDownloader
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using AutoDownloader.Interfaces;

    public partial class App
    {
        internal class MEFComponents
        {

            [ImportMany(typeof(ResourceDictionary))]
            public IEnumerable<ResourceDictionary> ResourceDictionaries
            {
                get; set;
            }

            public IEnumerable<PluginInformation> SourcePluginInformation
            {
                get; set;
            }

            [ImportMany(typeof(IGeneralPlugin))]
            public IEnumerable<IGeneralPlugin> GeneralPlugins
            {
                get; set;
            }
        }
    }
}