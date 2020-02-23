namespace Codefarts.AutoDownloader
{
    using System;
    using System.Windows.Input;
    using AutoDownloader;
    using Codefarts.AppCore;
    using Codefarts.WPFCommon.Commands;


    public class ApplicationViewModel : PropertyChangedBase
    {
        private Guid mainViewId;

        private ApplicationModel application;
        private int selectedSourcePluginIndex;

        /// <summary>
        /// Gets or sets the index of the selected results plugin.
        /// </summary>
        /// <value>
        /// The index of the selected results plugin.
        /// </value>
        public int SelectedSourcePluginIndex
        {
            get
            {
                return this.selectedSourcePluginIndex;
            }

            set
            {
                var currentValue = this.selectedSourcePluginIndex;
                if (value < -1)
                {
                    value = -1;
                }

                var plugins = this.Application.Plugins.SourcePluginInformation;
                if (plugins != null && value > plugins.Count - 1)
                {
                    value = plugins.Count - 1;
                }

                if (currentValue != value)
                {
                    this.selectedSourcePluginIndex = value;
                    this.NotifyOfPropertyChange(() => this.SelectedSourcePluginIndex);
                    this.NotifyOfPropertyChange(() => this.SelectedSourcePlugin);
                }
            }
        }

        /// <summary>
        /// Gets the selected results plugin.
        /// </summary>
        /// <value>
        /// The selected results plugin.
        /// </value>
        public PluginInformation SelectedSourcePlugin
        {
            get
            {
                var index = this.selectedSourcePluginIndex;
                var plugins = this.application.Plugins.SourcePluginInformation;
                if (plugins != null && index >= 0 && index <= plugins.Count - 1)
                {
                    return plugins[index];
                }

                return null;
            }
        }

        public ApplicationViewModel(ApplicationModel application)
        {
            this.Application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public ApplicationModel Application
        {
            get
            {
                return this.application;
            }

            set
            {
                var currentValue = this.application;
                if (currentValue != value)
                {
                    this.application = value;
                    this.NotifyOfPropertyChange(() => this.Application);
                }
            }
        }

        public ICommand QuitApplication
        {
            get
            {
                return new AppShutdownCommand();
            }
        }

        public ICommand ClosingMainWindow
        {
            get
            {
                return new DelegateCommand(x => true, x =>
                {
                    //while (this.application.Plugins.Count > 0)
                    //{
                    //    var search = this.application.Searches.First();
                    //    this.application.Searches.Remove(search);
                    //    search.SourcePlugin.Disconnect();
                    //}

                    //foreach (var plugin in this.application.Plugins.GeneralPlugins)
                    //{
                    //    plugin.Disconnect();
                    //}
                });
            }
        }
    }
}
