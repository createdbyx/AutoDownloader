namespace Codefarts.AutoDownloader
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using Codefarts.AppCore;

    public class LoggingDataModel : PropertyChangedBase
    {
        private LogsCollection logs;

        private ApplicationModel appModel;

        public LoggingDataModel(ApplicationModel applicationModel)
        {
            this.appModel = applicationModel ?? throw new ArgumentNullException(nameof(applicationModel));
            //this.appModel.Settings.OnChanged(
            //    () => this.appModel.Settings.MaximumLogEntryCount,
            //    (s, e) => this.TrimLogEntries());

            this.logs = new LogsCollection();
            this.logs.CollectionChanged += this.Logs_CollectionChanged;
        }

        //private void TrimLogEntries()
        //{
        //    // don't auto trim if value is less then 100
        //    var entryCount = this.appModel.Settings.MaximumLogEntryCount;
        //    var entryCountExceeded = this.logs.Count > entryCount;
        //    var changed = false;
        //    while (entryCountExceeded && entryCount > 100)
        //    {
        //        this.logs.RemoveAt(0);
        //        changed = true;
        //    }

        //    if (changed)
        //    {
        //        this.NotifyOfPropertyChange(() => this.Logs);
        //    }
        //}

        private void Logs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                //   this.TrimLogEntries();
            }

            this.NotifyOfPropertyChange(() => this.LastLogEntry);
        }

        public LogsCollection Logs
        {
            get
            {
                return this.logs;
            }

            internal set
            {
                var currentValue = this.logs;
                if (currentValue != value)
                {
                    currentValue.CollectionChanged -= this.Logs_CollectionChanged;
                    this.logs = value;

                    if (value != null)
                    {
                        value.CollectionChanged += this.Logs_CollectionChanged;
                    }

                    this.NotifyOfPropertyChange(() => this.Logs);
                    this.NotifyOfPropertyChange(() => this.LastLogEntry);
                }
            }
        }

        public string LastLogEntry
        {
            get
            {
                return this.Logs.LastOrDefault();
            }
        }
    }
}