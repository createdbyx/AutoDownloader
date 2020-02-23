namespace Codefarts.AutoDownloader
{
    using System.ComponentModel;
    using System.Windows;

    public class ViewModelLocator
    {
        private DependencyObject dummy = new DependencyObject();

        //public SearchResultsViewModel ViewModel
        //{
        //    get
        //    {
        //        if (this.IsInDesignMode())
        //        {
        //            return new SearchResultsViewModel();
        //        }

        //        return IoC.Container.Default.Resolve<SearchResultsViewModel>();
        //    }
        //}

        // returns true if editing .xaml file in VS for example
        private bool IsInDesignMode()
        {
            return DesignerProperties.GetIsInDesignMode(this.dummy);
        }
    }
}