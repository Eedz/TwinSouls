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
        public Weapon EquippedWeapon { get; set; }

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

        public Player() : base()
        {
            playSpeed = 18;
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
