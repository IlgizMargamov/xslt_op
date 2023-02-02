using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XSLT.Viewer
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> values = new Dictionary<string, object>();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T Get<T>(T defaultValue, [CallerMemberName] string propertyName="")
        {
            if (values.TryGetValue(propertyName, out var value))
            {
                return (T)value;
            }
            return defaultValue;    
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = "")
        {
            values[propertyName] = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetColl<T>(ObservableCollection<T> val, [CallerMemberName] string propertyName = "")
        {
            Set(val, propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ObservableCollection<T> GetColl<T>([CallerMemberName] string propertyName = "")
        {
            if (values.ContainsKey(propertyName))
                return (ObservableCollection<T>)values[propertyName];
            return (ObservableCollection<T>)(values[propertyName] = new ObservableCollection<T>());
        }
    }
}
