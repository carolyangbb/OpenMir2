using System;
using System.Windows.Forms;

namespace GameSvr
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            M2Share.FrmMain = new GameHost();
            Application.Run();
        }
    }
}

