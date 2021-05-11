using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.logic
{
    internal struct ComputerMemorySlot
    {
        private readonly Position r_Position;
        private readonly char r_Letter;
        private readonly int r_TurnNumber;

        public ComputerMemorySlot(Position i_Position , char i_Letter, int i_TurnNumber)   // ctor
        {
            r_Position = i_Position;
            r_Letter = i_Letter;
            r_TurnNumber = i_TurnNumber;
        }
        public Position Position   // properties
        {
            get { return r_Position; }
        }
        public int Row  
        {
            get { return r_Position.Row; }
        }
        public int Colomn   
        {
            get { return r_Position.Colomn; }
        }
        public int TurnNumber   // properties
        {
            get { return r_TurnNumber; }
        }
        public char Letter  // properties
        {
            get { return r_Letter; }
        }
    }
}
