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

namespace AutoClicker
{
    class Window
    {
        static bool autoClickerOn = false;
        static int delay = 1000;
        static bool ClickLeft = true; //tell the program that we are left clicking
        //static string toggleText = "Turn On";
        static TextBox textBox = new TextBox() 
        {
            Width = 100,
            Height = 30,
            Text = delay.ToString(),
        };
        static Button clickButton = new Button() 
        {
            Width = 100,
            Height = 30,
            Text = "Left Click",
        };

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwdata, int dwextrainfo);
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState( System.Windows.Forms.Keys vKey); 

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; // left btton up
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; // right mouse button down
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; // right mouse button up

        static public void leftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        static public void rightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
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
                    if (ClickLeft)
                    {
                        leftClick();
                    } else 
                    {
                        rightClick();
                    }

                }
                
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
            myform.Height = 150;
            myform.Icon = new System.Drawing.Icon("./cursor.ico");
            myform.FormBorderStyle = FormBorderStyle.FixedSingle;
            myform.MaximizeBox = false;
            myform.MinimizeBox = false;

            textBox.SetBounds(myform.Width/2 - (textBox.Width/2), 10, textBox.Width, textBox.Height);
            clickButton.SetBounds(myform.Width/2 - (clickButton.Width/2), 80, 100, 30);

            Button toggle = new Button()
            {
                Width = 100,
                Height = 30,
                Text = "Help",
                Location = new System.Drawing.Point(myform.Width/2 - (100/2), 40),
            };
            toggle.Click += (o, s) =>
            {
               MessageBox.Show("F8 start clicking, F9 stop clicking. Press the button below to toggle between Right clicking and Left clicking. \n\n flebedev99 studios©");
            };
            clickButton.Click += (o, s) => 
            {
                ClickLeft = !ClickLeft; //toggle clicking left
                clickButton.Text = ClickLeft ? "Left Click" : "Right Click";
            };

            myform.Controls.Add(toggle);
            myform.Controls.Add(textBox);
            myform.Controls.Add(clickButton);
            myform.ShowDialog();


           while (myform.Created)
            {
            }
            autoClickThread.Abort();
        }


    }
}