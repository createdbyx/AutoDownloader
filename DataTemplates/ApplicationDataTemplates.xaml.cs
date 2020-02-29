namespace Codefarts.AutoDownloader.DataTemplates
{
    using System.ComponentModel.Composition;
    using System.Windows;
                                              
    /// <summary>
    /// Interaction logic for ApplicationDataTemplates.xaml
    /// </summary>
    [Export(typeof(ResourceDictionary))]
    public partial class ApplicationDataTemplates : ResourceDictionary 
    {
        public ApplicationDataTemplates()
        {
           this.InitializeComponent();
        }
    }
}
