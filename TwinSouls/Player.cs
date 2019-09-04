using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TwinSouls
{
    

    public class Player : GameCharacter, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string soul;

        

        public bool goleft = false;
        public bool goright = false;
        public bool jumping = false;
        public bool hasKey = false;

        public int jumpSpeed = 15;
        public int force = 14; // force of the jump
        public int score = 0;

        public int playSpeed = 18; // player speed

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
