namespace Codefarts.AutoDownloader.Interfaces
{
    public interface IGeneralPlugin
    {
        ApplicationModel Application
        {
            get;
        }

        void Connect(ApplicationModel appModel);

        void Disconnect();
    }
}