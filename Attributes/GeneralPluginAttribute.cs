namespace Codefarts.AutoDownloader
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class GeneralPluginAttribute : Attribute
    {
        public string Title { get; protected set; }

        public GeneralPluginAttribute(string title)
        {
            this.Title = title;
        }
    }
}