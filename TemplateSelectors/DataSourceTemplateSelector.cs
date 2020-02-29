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

            var selectedItem = item as ISourcePlugin;
            if (element != null && selectedItem != null)
            {
                try
                {
                    var viewService = IoC.Container.Default.Resolve<IViewService>();
                    var name = selectedItem.GetType().Name;
                    var view = viewService.CreateView(name);


                    var factory = new FrameworkElementFactory(view.ViewReference.GetType());
                    // rectangleFactory.SetValue(Shape.FillProperty, new SolidColorBrush(System.Windows.Media.Colors.LightGreen));

                    return new DataTemplate
                    {
                        VisualTree = factory,
                    };
                   // var resource = view.ViewReference as DataTemplate;
                    //var resource = element.FindResource(name) as DataTemplate;
                    //element.DataContext = selectedItem.Item;
                    //  return resource;
                }
                catch
                {
                }
            }

            return this.DefaultTemplate;
        }
    }
}