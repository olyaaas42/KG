using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Physics
    {
        public Transform transform;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
        }
    }
}
