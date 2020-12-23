using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPasswdBinding.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _login;
        private string _passwd;

        public string Passwd
        {
            get
            {
                return _passwd;
            }

            set
            {
                if (_passwd != value)
                {
                    _passwd = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Passwd)));
                }
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }

            set
            {
                if (_login != value)
                {
                    _login = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
                }
            }
        }

        public MainViewModel()
        {
            Passwd = "Test1234=";
        }
    }
}
