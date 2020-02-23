using Codefarts.AutoDownloader;
using Codefarts.AutoDownloader.Interfaces;

namespace Codefarts.AutoDownloader.Models
{
    using System.Collections.ObjectModel;
    using Codefarts.AppCore;

    public class PluginsModel : PropertyChangedBase
    {
        // private ObservableCollection<PluginInformation> filterPlugins;
        private ApplicationModel application;
        // private ObservableCollection<PluginInformation> sourcePlugins;
        // private ObservableCollection<PluginInformation> resultPlugins;
        private ObservableCollection<PluginInformation> sourcePluginInformation;
        private ObservableCollection<IGeneralPlugin> generalPlugins;

        public ObservableCollection<IGeneralPlugin> GeneralPlugins
        {
            get
            {
                return this.generalPlugins;
            }

            set
            {
                var pValue = this.generalPlugins;
                if (pValue != value)
                {
                    this.generalPlugins = value;
                    this.NotifyOfPropertyChange(() => this.GeneralPlugins);
                }
            }
        }

        public ObservableCollection<PluginInformation> SourcePluginInformation
        {
            get
            {
                return this.sourcePluginInformation;
            }

            set
            {
                var pValue = this.sourcePluginInformation;
                if (pValue != value)
                {
                    this.sourcePluginInformation = value;
                    this.NotifyOfPropertyChange(() => this.SourcePluginInformation);
                }
            }
        }

        //public ObservableCollection<PluginInformation> ResultPlugins
        //{
        //    get
        //    {
        //        return this.resultPlugins;
        //    }

        //    set
        //    {
        //        var pValue = this.resultPlugins;
        //        if (pValue != value)
        //        {
        //            this.resultPlugins = value;
        //            this.NotifyOfPropertyChange(() => this.ResultPlugins);
        //        }
        //    }
        //}

        //public ObservableCollection<PluginInformation> SourcePlugins
        //{
        //    get
        //    {
        //        return this.sourcePlugins;
        //    }

        //    set
        //    {
        //        var pValue = this.sourcePlugins;
        //        if (pValue != value)
        //        {
        //            this.sourcePlugins = value;
        //            this.NotifyOfPropertyChange(() => this.SourcePlugins);
        //        }
        //    }
        //}

        public PluginsModel(ApplicationModel application)
        {
            this.application = application;
        }

        //public ObservableCollection<PluginInformation> FilterPlugins
        //{
        //    get
        //    {
        //        return this.filterPlugins;
        //    }

        //    set
        //    {
        //        var pValue = this.filterPlugins;
        //        if (pValue != value)
        //        {
        //            this.filterPlugins = value;
        //            this.NotifyOfPropertyChange(() => this.FilterPlugins);
        //        }
        //    }
        //}
    }
}