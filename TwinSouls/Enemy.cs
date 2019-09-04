using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace TwinSouls
{

    

    public class Enemy : GameCharacter, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }
        private int _left;
        private int _top;
        private int _speed = 3;

        public int Left
        {
            get { return _left; }
            set
            {
                _left = value;
                OnPropertyChanged("Left");
            }
        }

        public int Top
        {
            get { return _top; }
            set
            {
                _top = value;
                OnPropertyChanged("Top");
            }
        }

        public int Speed { get { return _speed; } set { _speed = value; } }

        // Create the OnPropertyChanged method to raise the event
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
