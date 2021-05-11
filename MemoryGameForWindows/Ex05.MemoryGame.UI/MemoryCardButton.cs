using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Ex05.MemoryGame.logic;

namespace Ex05.MemoryGame.UI
{
    public class MemoryCardButton : Button
    {
        private readonly Color r_CloseColor=Color.DarkGray;
        private readonly int r_Row;
        private readonly int r_Colomn;
        public MemoryCardButton(int i_Row, int i_Colomn)
        {
            r_Row = i_Row;
            r_Colomn = i_Colomn;
            Size = new Size(80, 80);
            BackColor = r_CloseColor;
        }
        public int Row
        {
            get { return r_Row; }
        }
        public int Colomn
        {
            get { return r_Colomn; }
        }
        public void OpenCardButton(Color i_Color, char i_Letter)
        {
            Text = i_Letter.ToString();
            BackColor = i_Color;
        }
        public void CloseCardButton()
        {
            Text = "";
            BackColor = r_CloseColor;
        }

    }
}
