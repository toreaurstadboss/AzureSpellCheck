using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureSpellCheckDemo.Models
{
    public class SpellCheckViewModel : BaseViewModel
    {

        private string? _inputText;
        public string? InputText
        {
            get { return _inputText; }
            set {
                _inputText = value;
                OnPropertyChanged();
                //OnPropertyChanged<SpellCheckViewModel>(m => m.InputText!);
            }
        }


        public string OutputText { get; set; } = string.Empty;

        
    }
}
