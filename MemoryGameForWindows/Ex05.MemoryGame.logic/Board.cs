using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.logic
{
    public class Board
    {
        private readonly int r_height;
        private readonly int r_width;
        private Card[,] m_BoardMatrix;
        private int m_openCardsAmount = 0;
        public int OpenCardsAmount   // properties
        {
            get { return m_openCardsAmount; }
        }
        public int Width   // properties
        {
            get { return r_width; }
        }
        public int Height   // properties
        {
            get { return r_height; }
        }
        internal Card[,] BoardMatrix   // properties
        {
            get { return m_BoardMatrix; }
        }
        public int Size   
        {
            get { return r_width*r_height; }
        }
        internal void OpenCard(int i_Row, int i_Colomn)
        {
            m_BoardMatrix[i_Row, i_Colomn].Open();
            m_openCardsAmount++;
        }
        internal void CloseCard(int i_Row, int i_Colomn)
        {
            m_BoardMatrix[i_Row, i_Colomn].Close();
            m_openCardsAmount--;
        }
        public char GetLetter(int i_Row, int i_Colomn)
        {
            return m_BoardMatrix[i_Row, i_Colomn].CardLetter;
        }
        public Board(int i_Height, int i_Width)//ctor
        {
            m_BoardMatrix = new Card[i_Height, i_Width];
            r_width = i_Width;
            r_height = i_Height;
        }
    }
}
