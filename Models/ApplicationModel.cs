using Codefarts.AutoDownloader.Models;

namespace Codefarts.AutoDownloader
{
    using System.Collections.ObjectModel;
    using Codefarts.AppCore;

    public class ApplicationModel : PropertyChangedBase
    {

        private LoggingDataModel logging;
        private PluginsModel plugins;

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