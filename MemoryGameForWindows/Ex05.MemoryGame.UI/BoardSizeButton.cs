using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Ex05.MemoryGame.UI
{
    class BoardSizeButton : Button
    {
        private eBoardSize m_BoardSize;
        private enum eBoardSize
        {
            _4X4,
            _4X5,
            _4X6,
            _5X4,
            _5X6,
            _6X4,
            _6X5,
            _6X6
        }
        public void NextSize()
        {
            if(m_BoardSize==eBoardSize._6X6)
            {
                m_BoardSize = eBoardSize._4X4;
            }
            else
            {
                m_BoardSize++;
            }
            Text = (m_BoardSize.ToString()).Substring(1);
        }
        public BoardSizeButton()
        {
            Size = new Size(100, 75);
            BackColor = Color.LightSkyBlue;
            m_BoardSize = eBoardSize._4X4;
            Text = (m_BoardSize.ToString()).Substring(1);
        }
    }
}
