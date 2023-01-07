using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;

namespace myform
{
    class Program
    {
        static bool autoClickerOn = false;
        static int delay = 1000;
        //static string toggleText = "Turn On";
        static TextBox textBox = new TextBox() 
        {
            Width = 100,
            Height = 30,
            Text = delay.ToString(),
        };

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwdata, int dwextrainfo);
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState( System.Windows.Forms.Keys vKey); 

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; // left btton up

        static public void leftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }


        public static void autoClick() 
        {
            while(true) 
            {
                if (GetAsyncKeyState(Keys.F8) < 0) {
                    autoClickerOn = false;
                }
                if (GetAsyncKeyState(Keys.F9) < 0) {
                    autoClickerOn = true;
                }
                if (autoClickerOn) 
                {
                    Int32.TryParse(textBox.Text, out delay);
                    System.Threading.Thread.Sleep(delay);
                    leftClick();
                }
                //toggleText = autoClickerOn ? "Turn Off" : "Turn On";
            }
        }

        static void Main(string[] args)
        {
            
           ShowWindow(GetConsoleWindow(), 0);
            
            Thread autoClickThread = new Thread(autoClick);
            autoClickThread.Start();

            Form myform = new Form();
            myform.Text = "Auto Clicker";
            myform.Width = 100;
            myform.Height = 100;
            myform.Icon = new System.Drawing.Icon("./cursor.ico");
            myform.FormBorderStyle = FormBorderStyle.FixedSingle;
            myform.MaximizeBox = false;
            myform.MinimizeBox = false;

            textBox.SetBounds(myform.Width/2 - (textBox.Width/2), 10, textBox.Width, textBox.Height);

            Button toggle = new Button()
            {
                Width = 100,
                Height = 30,
                Text = "Help",
                Location = new System.Drawing.Point(myform.Width/2 - (100/2), myform.Height/2 - (30/2)),
            };
            toggle.Click += (o, s) =>
            {
               //autoClickerOn = !autoClickerOn;
               MessageBox.Show("F8 to stop the auto clicker, and F9 to start");
            };

            myform.Controls.Add(toggle);
            myform.Controls.Add(textBox);
            myform.ShowDialog();


           while (myform.Created)
            {
            }
            autoClickThread.Abort();
        }


    }
}