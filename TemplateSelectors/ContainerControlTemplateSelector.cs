namespace Codefarts.AutoDownloader.TemplateSelectors
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using Codefarts.AutoDownloader.Interfaces;
    using Codefarts.ViewMessaging;

    public class ContainerControlTemplateSelector : DataTemplateSelector
    {
        public string ContainerControlName
        {
            get; set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            var selectedItem = item as ISourcePlugin;
            if (element != null && selectedItem != null)
            {
                try
                {

                    var viewService = IoC.Container.Default.Resolve<IViewService>();
                    //var name = selectedItem.GetType().Name;
                    var args = new Dictionary<string, object>();
                    args["IsDataTemplate"] = true;
                    var view = viewService.CreateView(this.ContainerControlName, args);

                    var resource = view.ViewReference as DataTemplate;
                    // var resource = element.FindResource(this.ContainerControlName) as DataTemplate;
                    //element.DataContext = selectedItem.Item;
                    return resource;
                }
                catch
                {
                }
            }

            return null;
        }
    }
}