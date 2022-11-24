using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Player
    {
        public Physics physics;

        public Player(PointF position, Size size)
        {
            physics = new Physics(position, size);
        }

        public void DrawSprite(Graphics g)
        {
            DrawNeededSprite(g, 1525, 0, 100, 91, 1, 1.35f);
        }

        public void DrawNeededSprite(Graphics g, int srcX, int srcY, int width, int height, int delta, float multiplier)
        {
            g.DrawImage(GameController.spritesheet, new Rectangle(new Point((int)physics.transform.position.X, 
                (int)physics.transform.position.Y), new Size((int)(physics.transform.size.Width * multiplier), 
                physics.transform.size.Height)), srcX + delta, srcY, width, height, GraphicsUnit.Pixel);
        }
    }
}
