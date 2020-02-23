using System;

namespace Codefarts.AutoDownloader
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SourcePluginAttribute : Attribute
    {
        public string Title { get; protected set; }

        public  SourcePluginAttribute(string title)
        {
            this.Title = title;
        }
    }
}