using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LetsBowl.WPF
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {

        }

        protected void RunOnDispatcher(Action WhatToRun)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                WhatToRun();
            });

        }

        #region Eventing
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        protected void SetProperty<T>(ref T backingStore, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(backingStore, newValue))
            {
                backingStore = newValue;
                OnPropertyChanged(propertyName);
            }
        }

    }
}
