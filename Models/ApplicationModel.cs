namespace Codefarts.AutoDownloader
{
    using System.Collections.ObjectModel;
    using Codefarts.AppCore;
    using Codefarts.AutoDownloader.Interfaces;
    using Codefarts.AutoDownloader.Models;

    public class ApplicationModel : PropertyChangedBase
    {
        private LoggingDataModel logging;
        private PluginsModel plugins;
        private IObservableCollection<PluginEntryModel> activePlugins;

        public IObservableCollection<PluginEntryModel> ActivePlugins
        {
            get
            {
                return this.activePlugins;
            }

            set
            {
                var currentValue = this.activePlugins;
                if (currentValue != value)
                {
                    this.activePlugins = value;
                    this.NotifyOfPropertyChange(() => this.ActivePlugins);
                }
            }
        }

        public PluginsModel Plugins
        {
            get
            {
                return this.plugins;
            }

            internal set
            {
                var currentValue = this.plugins;
                if (currentValue != value)
                {
                    this.plugins = value;
                    this.NotifyOfPropertyChange(() => this.Plugins);
                }
            }
        }

        public LoggingDataModel Logging
        {
            get
            {
                return this.logging;
            }

            set
            {
                var pValue = this.logging;
                if (pValue != value)
                {
                    this.logging = value;
                    this.NotifyOfPropertyChange(() => this.Logging);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ApplicationModel()
        {
            this.Plugins = new PluginsModel(this);
            this.logging = new LoggingDataModel(this);
            this.activePlugins = new BindableCollection<PluginEntryModel>();
        }

        public string Title
        {
            get
            {
                return "Auto Downloader";
            }
        }
    }
}