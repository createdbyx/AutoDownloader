using Codefarts.AppCore;
using Codefarts.ViewMessaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using Container = Codefarts.IoC.Container;
using System.Linq;
using Codefarts.AutoDownloader.Interfaces;

namespace Codefarts.AutoDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var viewService = new WpfViewService();
            this.Properties["ViewService"] = viewService;
            PlatformProvider.Current = new WpfPlatformProvider();

            var ioc = Container.Default;
            ioc.Register<IViewService>(() => viewService);
            ioc.Register<IPlatformProvider>(() => PlatformProvider.Current);

            var applicationModel = new ApplicationModel();
            var applicationViewModel = new ApplicationViewModel(applicationModel);
            var parts = this.LoadPlugins(applicationModel);

            this.LoadDictionaries(parts);

            var applicationPlugins = applicationViewModel.Application.Plugins;
            applicationPlugins.GeneralPlugins = new ObservableCollection<IGeneralPlugin>(parts.GeneralPlugins);
            applicationPlugins.SourcePluginInformation = new ObservableCollection<PluginInformation>(parts.SourcePluginInformation);

            // show main window
            var mainView = viewService.CreateView("Application");
            var window = mainView.ViewReference as Window;
            this.MainWindow = window;
            var args = GenericMessageArguments.Build(
                GenericMessageArguments.Show,
                GenericMessageArguments.SetModel(applicationViewModel));
            mainView.SendMessage(args);

            // connect source plugins
            this.ConnectPlugins(applicationViewModel, applicationPlugins.GeneralPlugins);
            applicationModel.Logging.Logs.Add("Status");
        }

        private void ConnectPlugins(ApplicationViewModel applicationViewModel, IEnumerable<IGeneralPlugin> plugins)
        {
            foreach (var item in plugins)
            {
                item.Connect(applicationViewModel.Application);
            }
        }

        private Type ComposablePartExportType<T>(ComposablePartDefinition part)
        {
            if (part.ExportDefinitions.Any(
                def => def.Metadata.ContainsKey("ExportTypeIdentity") &&
                       def.Metadata["ExportTypeIdentity"].Equals(typeof(T).FullName)))
            {
                return ReflectionModelServices.GetPartType(part).Value;
            }

            return null;
        }

        public IEnumerable<Type> GetExportedTypes<T>(ComposablePartCatalog catalog)
        {
            return catalog.Parts.Select(part => this.ComposablePartExportType<T>(part)).Where(t => t != null).ToArray();
        }

        private IEnumerable<PluginInformation> GetSourcePluginInformation(ComposablePartCatalog composerCatalog, ApplicationModel appModel)
        {
            var exportedTypes = this.GetExportedTypes<ISourcePlugin>(composerCatalog);
            var results = exportedTypes.Select(x =>
            {
                var attribute = x.GetCustomAttributes(true).OfType<SourcePluginAttribute>().FirstOrDefault();
                var categoryAttribute = x.GetCustomAttributes(true).OfType<CategoryAttribute>().FirstOrDefault();
                var descAttribute = x.GetCustomAttributes(true).OfType<DescriptionAttribute>().FirstOrDefault();

                if (attribute == null)
                {
                    return null;
                }

                var description = descAttribute == null ? string.Empty : descAttribute.Description;
                var category = categoryAttribute == null ? string.Empty : categoryAttribute.Category;
                appModel.Logging.Logs.Add(string.Format("General plugin found: \"{0}\"", attribute.Title));
                return new PluginInformation(attribute.Title, category, x, description);
            }).Where(x => x != null);

            return results.ToArray();
        }

        private MEFComponents LoadPlugins(ApplicationModel appModel)
        {
            var parts = new MEFComponents();
            var pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            Directory.CreateDirectory(pluginsPath);
            var searchPaths = Directory.GetDirectories(pluginsPath, "*.*", SearchOption.TopDirectoryOnly);
            var composer = MEFHelpers.Compose(searchPaths, parts);

            // parts.FilterPlugins = this.GetPluginInformation<ISearchFilter>(composer.Catalog, appModel);
            // parts.SourcePlugins = this.GetPluginInformation<ISourcePlugin>(composer.Catalog, appModel);
            parts.SourcePluginInformation = this.GetSourcePluginInformation(composer.Catalog, appModel);
            // parts.ResultPlugins = this.GetPluginInformation<IResultsPlugin>(composer.Catalog, appModel);
            return parts;
        }

        private void LoadDictionaries(MEFComponents parts)
        {
            var dictionaries = parts.ResourceDictionaries;
            this.Resources.MergedDictionaries.AddRange(dictionaries);
        }
    }
}
