using System.Linq;
using Codefarts.AutoDownloader.Interfaces;

namespace Codefarts.AutoDownloader
{
    using System;
    using System.Windows.Input;
    using Codefarts.AppCore;
    using Codefarts.AutoDownloader;
    using Codefarts.WPFCommon.Commands;

    public class ApplicationViewModel : PropertyChangedBase
    {
        private Guid mainViewId;

        private ApplicationModel application;
        private int selectedSourcePluginIndex;
        private bool showLogs;

        public bool ShowLogs
        {
            get
            {
                return this.showLogs;
            }

            set
            {
                var currentValue = this.showLogs;
                if (currentValue != value)
                {
                    this.showLogs = value;
                    this.NotifyOfPropertyChange(() => this.ShowLogs);
                }
            }
        }

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
                    while (this.application.ActivePlugins.Count > 0)
                    {
                        var plugin = this.application.ActivePlugins.First();
                        this.application.ActivePlugins.Remove(plugin);
                        plugin.Plugin.Disconnect();
                    }

                    foreach (var plugin in this.application.Plugins.GeneralPlugins)
                    {
                        plugin.Disconnect();
                    }
                });
            }
        }

        public ICommand AddPluginCommand
        {
            get
            {
                return new DelegateCommand(x => this.SelectedSourcePlugin != null, x =>
                 {
                     var info = this.SelectedSourcePlugin;
                     // create source plugin
                     //var info = x;//as PluginInformation;
                     if (info == null)
                     {
                         throw new ArgumentNullException();
                     }

                     if (info.Type == null)
                     {
                         throw new NullReferenceException("Source type not specified!");
                     }

                     // var model = new SearchResultsModel(this.application);
                     var source = info.Type.Assembly.CreateInstance(info.Type.FullName) as ISourcePlugin;
                     var model = new PluginEntryModel()
                     {
                         Plugin = source,
                         Interval = TimeSpan.FromMinutes(1)
                     };
                     this.application.ActivePlugins.Add(model);
                     source.Connect(this.Application);
                 });
            }
        }

        public ICommand ShowLogsCommand
        {
            get
            {
                return new DelegateCommand(x => true, x => { this.ShowLogs = !this.ShowLogs; });
            }
        }
    }
}
