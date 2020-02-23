namespace Codefarts.AutoDownloader
{
    using System.Collections.Generic;
    using Codefarts.AppCore;

    /// <summary>
    /// Provides a collection of <see cref="IResultsPlugin"/> types.
    /// </summary>
    /// <seealso cref="Codefarts.AppCore.BindableCollection{string}" />
    public class LogsCollection : BindableCollection<string>
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
        public LogsCollection(IEnumerable<string> collection)
            : base(collection)
        {
        }
    }
}