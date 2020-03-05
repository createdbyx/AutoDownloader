namespace Codefarts.AutoDownloader
{
    using System;
    using Codefarts.AppCore;
    using Codefarts.AutoDownloader.Interfaces;

    public class PluginEntryModel : PropertyChangedBase
    {
        private ISourcePlugin plugin;
        private TimeSpan interval = TimeSpan.FromMinutes(1);
        private int executionCount;
        private DateTime lastStarted;

        public TimeSpan RunDuration
        {
            get
            {
                return DateTime.Now - this.LastStarted;
            }
        }

        public DateTime LastStarted
        {
            get
            {
                return this.lastStarted;
            }

            set
            {
                var currentValue = this.lastStarted;
                if (currentValue != value)
                {
                    this.lastStarted = value;
                    this.NotifyOfPropertyChange(() => this.LastStarted);
                }
            }
        }

        public int ExecutionCount
        {
            get
            {
                return this.executionCount;
            }

            set
            {
                var currentValue = this.executionCount;
                if (currentValue != value)
                {
                    this.executionCount = value;
                    this.NotifyOfPropertyChange(() => this.ExecutionCount);
                }
            }
        }

        /// <summary>
        /// Gets or sets the time interval.
        /// </summary>
        /// <remarks>
        /// Min value is restricted to 1 second.
        /// </remarks>
        public TimeSpan Interval
        {
            get
            {
                return this.interval;
            }

            set
            {
                var currentValue = this.interval;
                if (currentValue != value)
                {
                    this.interval = value;
                    this.NotifyOfPropertyChange(() => this.Interval);
                }
            }
        }

        public ISourcePlugin Plugin
        {
            get
            {
                return this.plugin;
            }

            set
            {
                var currentValue = this.plugin;
                if (currentValue != value)
                {
                    this.plugin = value;
                    this.NotifyOfPropertyChange(() => this.Plugin);
                }
            }
        }
    }
}