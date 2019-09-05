using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TwinSouls
{
    public class GameCharacter
    {
        public int _left;
        public int _top;
        public int Width { get; set; }
        public int Height { get; set; }

        public bool jumping = false;
        public bool hasKey = false;

        public int jumpSpeed = 15;
        public int force = 14; // force of the jump
        public int score = 0;

        public int playSpeed = 18; // player speed

        public bool InstersectsWith(GameCharacter gc)
        {
            Rectangle rec = new Rectangle(_left, _top, Width, Height);
            Rectangle otherRec = new Rectangle(gc._left, gc._top, gc.Width, gc.Height);

            if (rec.IntersectsWith(otherRec))
                return true;
            else
                return false;

            
        }

        public Rectangle GetRectangle()
        {
            return new  Rectangle(_left, _top, Width, Height);
        }
    }

   
}
