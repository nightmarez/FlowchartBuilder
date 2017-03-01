// <copyright file="GameWindow.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-21</date>
// <summary>Игровое окно.</summary>

using System.Windows.Forms;

namespace Makarov.Framework.Components
{
    /// <summary>
    /// Игровое окно.
    /// </summary>
    public class GameWindow : Form
    {
        public delegate void DrawSceneDelegate();

        public event DrawSceneDelegate DrawScene;

        public GameWindow()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x000F)
            {
                if (DrawScene != null)
                    DrawScene();

                Invalidate();
            }
            else
                base.WndProc(ref m);
        }
    }
}