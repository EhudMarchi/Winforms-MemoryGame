using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.logic
{
    public struct Card
    {
        private readonly char r_CardLetter;
        private bool m_IsOpen;

        public char CardLetter   // properties
        {
            get { return r_CardLetter; }
        }
        public bool IsOpen   // properties
        {
            get { return m_IsOpen; }
        }
        public Card(char i_CardLetter)// ctor
        {
            r_CardLetter = i_CardLetter;
            m_IsOpen = false;
        }
        internal void Open()  
        {
             m_IsOpen = true;
        }
        internal void Close()   
        {
            m_IsOpen = false;
        }
    }
}
