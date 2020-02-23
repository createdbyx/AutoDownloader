namespace Codefarts.AutoDownloader.Interfaces
{
    public interface IGeneralPlugin
    {

        void Connect(ApplicationModel appModel);

        void Disconnect();
    }
}