namespace Codefarts.AutoDownloader.Interfaces
{
    public interface ISourcePlugin : IGeneralPlugin
    {
        bool IsRunning
        {
            get;
        }

        void Run();

        void Stop();
    }
}