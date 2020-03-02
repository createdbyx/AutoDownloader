namespace Codefarts.AutoDownloader
{
    using System;
    using System.Collections.Generic;
    using Codefarts.AppCore;
    using Codefarts.Logging;

    /// <summary>
    /// Provides a collection of <see cref="LogModel"/> types.
    /// </summary>
    /// <seealso cref="Codefarts.AppCore.BindableCollection{LogModel}" />
    public class LogsCollection : BindableCollection<LogModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogsCollection"/> class.
        /// </summary>
        public LogsCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogsCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        public LogsCollection(IEnumerable<LogModel> collection)
            : base(collection)
        {
        }

        public void Add(string message)
        {
            this.Add(LogEntryType.Generic, message, null);
        }

        public void Add(LogEntryType type, string message)
        {
            this.Add(type, message, null);
        }

        public void Add(string message, string category)
        {
            this.Add(LogEntryType.Generic, message, category);
        }

        public void Add(LogEntryType type, string message, string category)
        {
            var item = new LogModel();
            item.Message = message;
            item.Type = type;
            item.Category = category;
            item.TimeStamp = DateTime.Now;
            base.Add(item);
        }
    }
}