namespace AutoDownloader.ValueConverters
{
    using System;

    public enum TimeSpanValue
    {
        Hours,
        Minutes,
        Seconds
    }



    /// <summary>
    /// ValueConvertor class that converts a nullable integer to a string and back.
    /// </summary>
    /// <remarks>
    /// Useful for databinding string value properties to nullable integer properties.
    /// </remarks>
    [System.Windows.Data.ValueConversion(typeof(TimeSpan), typeof(int?))]
    public class NullableIntegerTimeSpanConverter : System.Windows.Data.IValueConverter
    {
        public TimeSpanValue Part
        {
            get; set;
        }

        /// <summary>
        /// Converts a nullable integer to a string.
        /// </summary>
        /// <param name="value">The value produced by the binding source</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>Converted value</returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && !(value is int?))
            {
                throw new System.InvalidOperationException("The source must be a nullable integer");
            }

            if (targetType != typeof(TimeSpan))
            {
                throw new System.InvalidOperationException("The target must be a TimeSpan");
            }

            var intValue = (int?)value;
            if (intValue.HasValue)
            {
                switch (this.Part)
                {
                    case TimeSpanValue.Hours:
                        return TimeSpan.FromHours(intValue.Value);

                    case TimeSpanValue.Minutes:
                        return TimeSpan.FromMinutes(intValue.Value);

                    case TimeSpanValue.Seconds:
                        return TimeSpan.FromSeconds(intValue.Value);
                }
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Converts a string to a nullable integer.
        /// </summary>
        /// <param name="value">The value produced by the binding source</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>Converted value</returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is TimeSpan))
            {
                throw new System.InvalidOperationException("The source must be a TimeSpan");
            }

            if (targetType != typeof(int?))
            {
                throw new System.InvalidOperationException("The target must be a nullable integer");
            }

            var timeValue = (TimeSpan) value;
            switch (this.Part)
            {
                case TimeSpanValue.Hours:
                    return timeValue.Hours;

                case TimeSpanValue.Minutes:
                    return timeValue.Minutes;

                case TimeSpanValue.Seconds:
                    return timeValue.Seconds;
            }

            return default(int?);
        }
    }
}
