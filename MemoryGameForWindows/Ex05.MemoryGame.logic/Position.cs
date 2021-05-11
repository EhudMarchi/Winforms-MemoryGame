using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.logic
{
    internal struct Position
    {
        private int m_Row;
        private int m_Colomn;
        public int Row   // properties
        {
            get { return m_Row; }
            set { m_Row = value; }
        }
        public int Colomn   // properties
        {
            get { return m_Colomn; }
            set { m_Colomn = value; }
        }
        public Position(int i_Row ,int i_Colomn)
        {
            m_Row = i_Row;
            m_Colomn = i_Colomn;
        }
    }
}
