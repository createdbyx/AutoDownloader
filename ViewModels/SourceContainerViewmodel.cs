namespace AutoDownloader.ViewModels
{
    using System;
    using System.Windows.Input;
    using Codefarts.AppCore;
    using Codefarts.AutoDownloader;
    using Codefarts.AutoDownloader.Interfaces;
    using Codefarts.WPFCommon.Commands;

    public class SourceContainerViewModel : PropertyChangedBase
    {
        private PluginEntryModel modelReference;

        public PluginEntryModel ModelReference
        {
            get
            {
                return this.modelReference;
            }

            set
            {
                var currentValue = this.modelReference;
                if (currentValue != value)
                {
                    this.modelReference = value;
                    this.NotifyOfPropertyChange(() => this.ModelReference);
                }
            }
        }


        public ICommand SetModel
        {
            get
            {
                return new GenericDelegateCommand<PluginEntryModel>(
                    model =>
                    {
                        if (model == null)
                        {
                            return false;
                        }

                        return true;
                    },
                    model => { this.ModelReference = model; });
            }
        }

        public ICommand StartPlugin
        {
            get
            {
                return new GenericDelegateCommand<PluginEntryModel>(
                    model =>
                    {
                        var entryModel = this.ModelReference;
                        return entryModel != null && !entryModel.Plugin.IsRunning;
                    },
                    model =>
                    {
                        var entryModel = this.ModelReference;
                        if (entryModel != null)
                        {
                            entryModel.Plugin.Run();
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
                        var entryModel = this.ModelReference;
                        return entryModel != null && entryModel.Plugin.IsRunning;
                    },
                    model =>
                    {
                        var entryModel = this.ModelReference;
                        if (entryModel != null)
                        {
                            entryModel.Plugin.Stop();
                        }
                    });
            }
        }

        public ICommand RemovePlugin
        {
            get
            {
                return new GenericDelegateCommand<PluginEntryModel>(
                    model =>
                    {
                        var entryModel = this.ModelReference;
                        return entryModel != null && entryModel.Plugin.Application != null;
                    },
                    model =>
                    {
                        var entryModel = this.ModelReference;
                        if (entryModel != null)
                        {
                            var app = entryModel.Plugin.Application;
                            if (app == null)
                            {
                                throw new ArgumentNullException("this.modelReference.Plugin.Application");
                            }

                            app.ActivePlugins.Remove(entryModel);
                        }
                    });
            }
        }
    }
}