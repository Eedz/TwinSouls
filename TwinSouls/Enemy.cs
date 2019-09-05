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

        public int Speed { get { return playSpeed; } set { playSpeed = value; } }

        public Enemy()
        {
            playSpeed = 3;
        }

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
