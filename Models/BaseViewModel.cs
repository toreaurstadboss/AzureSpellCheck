using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace AzureSpellCheckDemo.Models
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T, object>> expression)
        {
           if (expression == null)
            {
                return;
            }
            string propertyName = PropertyName.For(expression); 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName)){
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
