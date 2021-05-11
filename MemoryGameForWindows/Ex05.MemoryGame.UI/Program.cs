using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.UI
{
    public class Program
    {
        public static void Main()
        {
            MenuForm settingsForm = new MenuForm();
            settingsForm.ShowDialog();
        }
    }
}
