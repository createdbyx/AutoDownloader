namespace Codefarts.AutoDownloader
{
    using System;
    using Codefarts.AppCore;

    public class PluginInformation : PropertyChangedBase
    {
        private string title;
        private string category;
        private Type type;
        private string description;

        public PluginInformation(string title, string category, Type type)
        {
            this.title = title;
            this.category = category;
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public PluginInformation(string title, string category, Type type, string description)
        {
            this.title = title;
            this.category = category;
            this.type = type ?? throw new ArgumentNullException(nameof(type));
            this.description = description;
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                var pValue = this.description;
                if (pValue != value)
                {
                    this.description = value;
                    this.NotifyOfPropertyChange(() => this.Description);
                }
            }
        }

        public Type Type
        {
            get
            {
                return this.type;
            }

            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    this.NotifyOfPropertyChange(() => this.Type);
                }
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }

            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.NotifyOfPropertyChange(() => this.Category);
                }
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.NotifyOfPropertyChange(() => this.Title);
                }
            }
        }
    }
}