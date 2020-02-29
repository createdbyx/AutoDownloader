using System;

namespace AutoDownloader.ViewModels
{
    using System.Windows.Input;
    using Codefarts.AppCore;
    using Codefarts.AutoDownloader.Interfaces;
    using Codefarts.WPFCommon.Commands;

    public class SourceContainerViewModel : PropertyChangedBase
    {
        private ISourcePlugin pluginReference;

        public ISourcePlugin PluginReference
        {
            get
            {
                return this.pluginReference;
            }

            set
            {
                var currentValue = this.pluginReference;
                if (currentValue != value)
                {
                    this.pluginReference = value;
                    this.NotifyOfPropertyChange(() => this.PluginReference);
                }
            }
        }


        public ICommand SetModel
        {
            get
            {
                return new GenericDelegateCommand<ISourcePlugin>(
                    model =>
                    {
                        if (model == null)
                        {
                            return false;
                        }

                        return true;
                    },
                    model => { this.PluginReference = model; });
            }
        }

        public ICommand StartPlugin
        {
            get
            {
                return new GenericDelegateCommand<ISourcePlugin>(
                    model =>
                    {
                        var plugin = this.PluginReference;
                        return plugin != null && !plugin.IsRunning;
                    },
                    model =>
                    {
                        var plugin = this.PluginReference;
                        if (plugin != null)
                        {
                            plugin.Run();
                        }
                    });
            }
        }

        public ICommand StopPlugin
        {
            get
            {
                return new GenericDelegateCommand<ISourcePlugin>(
                    model =>
                    {
                        var plugin = this.PluginReference;
                        return plugin != null && plugin.IsRunning;
                    },
                    model =>
                    {
                        var plugin = this.PluginReference;
                        if (plugin != null)
                        {
                            plugin.Stop();
                        }
                    });
            }
        }

        public ICommand RemovePlugin
        {
            get
            {
                return new GenericDelegateCommand<ISourcePlugin>(
                    model =>
                    {
                        var plugin = this.PluginReference;
                        return plugin != null && plugin.Application != null;
                    },
                    model =>
                    {
                        var plugin = this.PluginReference;
                        if (plugin != null)
                        {
                            var app = this.pluginReference.Application;
                            if (app == null)
                            {
                                throw new ArgumentNullException("this.pluginReference.Application");
                            }

                            app.ActivePlugins.Remove(plugin);
                        }
                    });
            }
        }
    }
}