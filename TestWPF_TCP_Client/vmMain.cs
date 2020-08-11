using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPF_TCP_Client
{
    public class vmMain : INotifyPropertyChanged
    {
        private string _message = "Hello world!";
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private string _displayMessage = "";
        public string DisplayMessage
        {
            get
            {
                return _displayMessage;
            }
            set
            {
                _displayMessage = value;
                OnPropertyChanged("DisplayMessage");
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
      
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
