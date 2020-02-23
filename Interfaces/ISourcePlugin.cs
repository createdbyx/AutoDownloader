namespace Codefarts.AutoDownloader.Interfaces
{
    public interface ISourcePlugin : IGeneralPlugin
    {

        string Title
        {
            get;
        }

        bool IsRunning
        {
            get;
        }

        void Run();

        void Stop();
    }
}