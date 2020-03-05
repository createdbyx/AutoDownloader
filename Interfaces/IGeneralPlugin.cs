namespace Codefarts.AutoDownloader.Interfaces
{
    public interface IGeneralPlugin
    {
        string Title
        {
            get;
        }

        ApplicationModel Application
        {
            get;
        }

        void Connect(ApplicationModel appModel);

        void Disconnect();
    }
}