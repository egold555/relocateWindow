using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace relocateWindow
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        Random random = new Random();
        Boolean randomXY = true; //if true make window run away from mouse
        public Form1()
        {
            InitializeComponent();
        }

        private void relocateWindow()
        {
            //debug msg
           // Console.WriteLine("Window Moved!");

            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;
            int x = random.Next(width / 2);
            int y = random.Next(height / 2);


            this.Left = x;
            this.Top = y;

            Console.WriteLine("Location: X:" + Location.X + " Y:" + Location.Y);
        }

        private bool CollisionCheck()
        {
            Point win32Mouse = MouseHelper.GetPosition();

            if (win32Mouse.X <= Location.X || win32Mouse.X >= (Location.X + Width))
                return false;

            if (win32Mouse.Y <= Location.Y || win32Mouse.Y >= (Location.Y + Height))
                return false;

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CollisionCheck())
            {
                //change the location hahaha
               

                //Console.WriteLine("Mouse Touched Window!");
                if (randomXY)
                {
                    relocateWindow();
                }
                else
                {
                    Location = new Point(Location.X + 1, Location.Y + 1);
                    Console.WriteLine("Location: X:" + Location.X + " Y:" + Location.Y);
                }

            }
        }
    }

    static class MouseHelper
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Point pt);

        public static Point GetPosition()
        {
            Point w32Mouse = new Point();
            GetCursorPos(ref w32Mouse);
            return w32Mouse;
        }
    }
}
