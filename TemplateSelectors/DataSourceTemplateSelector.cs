namespace Codefarts.AutoDownloader.TemplateSelectors
{
    using System.Windows;
    using System.Windows.Controls;
    using Codefarts.AutoDownloader.Interfaces;
    using Codefarts.ViewMessaging;

    public class DataSourceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate
        {
            get; set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element != null && item != null)
            {
                try
                {
                    var model = (PluginEntryModel)item;
                    var viewService = IoC.Container.Default.Resolve<IViewService>();
                    var name = model.Plugin.GetType().Name;
                    var view = viewService.CreateView(name);

                    var factory = new FrameworkElementFactory(view.ViewReference.GetType());

                    return new DataTemplate
                    {
                        VisualTree = factory,
                    };
                }
                catch
                {
                }
            }

            return this.DefaultTemplate;
        }
    }
}