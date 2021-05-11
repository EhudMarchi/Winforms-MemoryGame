using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.logic
{
    public class Player
    {
        private string m_Name;
        private int m_Points = 0;
        private readonly bool r_IsPerson;
        public int Points   // properties
        {
            get { return m_Points; }
            set { m_Points = value; }
        }
        public string Name   // properties
        {
            get { return m_Name; }
        }
        public bool IsPerson   // properties
        {
            get { return r_IsPerson; }
        }
        public Player(string i_Name)// ctor
        {
            if (IsValidName(i_Name))
            {
                m_Name = i_Name;
                r_IsPerson = true;
            }
            else
            {
                throw new FormatException("the name is not valid because there are digits that arn't letters");
            }
        }
        public Player()// ctor
        {
            m_Name = "Computer";
            r_IsPerson = false;
        }
        public static bool IsValidName(string i_Name)
        {
            bool isValidName = true;
            if (i_Name.Length == 0)
            {
                isValidName = false;
            }
            else
            {
                foreach (char letter in i_Name)
                {
                    if (!char.IsLetter(letter))
                    {
                        isValidName = false;
                    }
                }
            }

            return isValidName;
        }
    }
}       