using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
namespace TwinSouls
{
    public class Projectile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _speed;
        private Rectangle _rect;

        public Rectangle Rect { get { return _rect; } set { _rect = value; OnPropertyChanged("Rect"); } }

        public int Speed { get { return _speed; } set { _speed = value; OnPropertyChanged("Speed"); } }

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
