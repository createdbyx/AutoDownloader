using System;
using System.Collections;
using Codefarts.AppCore;

namespace AutoDownloader.ViewModels
{
    using System.Windows.Input;
    using Codefarts.AutoDownloader;
    using Codefarts.WPFCommon.Commands;

    public class TimeSpanControlViewModel : PropertyChangedBase
    {
        private PluginEntryModel modelReference;

        public int Seconds
        {
            get
            {
                var model = this.modelReference;
                return model != null ? model.Interval.Seconds : 0;
            }

            set
            {
                var model = this.modelReference;
                if (model == null)
                {
                    return;
                }

                var currentValue = model.Interval.Seconds;
                if (currentValue != value)
                {
                    var interval = this.modelReference.Interval;
                    model.Interval = new TimeSpan(interval.Hours, interval.Minutes, value);
                }
            }
        }

        public int Minutes
        {
            get
            {
                var model = this.modelReference;
                return model != null ? model.Interval.Minutes : 0;
            }

            set
            {
                var model = this.modelReference;
                if (model == null)
                {
                    return;
                }

                var currentValue = model.Interval.Minutes;
                if (currentValue != value)
                {
                    var interval = this.modelReference.Interval;
                    model.Interval = new TimeSpan(interval.Hours, value, interval.Seconds);
                }
            }
        }

        public int Hours
        {
            get
            {
                var model = this.modelReference;
                return model != null ? model.Interval.Hours : 0;
            }

            set
            {
                var model = this.modelReference;
                if (model == null)
                {
                    return;
                }

                var currentValue = model.Interval.Hours;
                if (currentValue != value)
                {
                    var interval = this.modelReference.Interval;
                    model.Interval = new TimeSpan(value, interval.Minutes, interval.Seconds);
                }
            }
        }

        public PluginEntryModel ModelReference
        {
            get
            {
                return this.modelReference;
            }

            set
            {
                var currentValue = this.modelReference;
                if (currentValue != value)
                {
                    if (currentValue != null)
                    {
                        currentValue.PropertyChanged -= this.ModelReference_PropertyChanged;
                    }

                    this.modelReference = value;
                    if (value != null)
                    {
                        value.PropertyChanged += this.ModelReference_PropertyChanged;
                    }

                    this.NotifyOfPropertyChange(() => this.ModelReference);
                    this.NotifyOfPropertyChange(() => this.Hours);
                    this.NotifyOfPropertyChange(() => this.Minutes);
                    this.NotifyOfPropertyChange(() => this.Seconds);
                }
            }
        }

        private void ModelReference_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Interval":
                    this.NotifyOfPropertyChange(() => this.Hours);
                    this.NotifyOfPropertyChange(() => this.Minutes);
                    this.NotifyOfPropertyChange(() => this.Seconds);
                    break;
            }
        }

        public ICommand SetModel
        {
            get
            {
                return new GenericDelegateCommand<PluginEntryModel>(
                    model =>
                    {
                        if (model == null)
                        {
                            return false;
                        }

                        return true;
                    },
                    model => { this.ModelReference = model; });
            }
        }
    }
}
