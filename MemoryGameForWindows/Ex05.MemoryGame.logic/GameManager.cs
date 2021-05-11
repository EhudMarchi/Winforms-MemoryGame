using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGame.logic
{
    public abstract class GameManager
    {
        private static List<ComputerMemorySlot> s_ComputerMemory;
        private static int s_TurnsCount;
        private static Random s_Generator = new Random();

        public static void FillBoard(Board i_GameBoard)
        {
            s_TurnsCount = 1;
            s_ComputerMemory = null;
            List<char> letters = new List<char>();
            for (int i = 0; i < i_GameBoard.Size / 2; i++)
            {
                AddNewRandomLetter(letters);//Generates random letter
                PlacePair(letters[i], i_GameBoard);//Puts 2 cards with the same letter in random positions
            }
        }
        private static void AddNewRandomLetter(List<char> i_Letters)
        {
            char letter = (char)('A' + s_Generator.Next(0, 26));
            while (i_Letters.Contains(letter))
            {
                letter = (char)('A' + s_Generator.Next(0, 26));
            }
                i_Letters.Add(letter);
        }
        private static void PlacePair(char i_Letter, Board i_GameBoard)
        {
            PlaceLetter(i_Letter, i_GameBoard);
            PlaceLetter(i_Letter, i_GameBoard);
        }
        private static void PlaceLetter(char i_Letter, Board i_GameBoard)
        {
            int colomn = s_Generator.Next(i_GameBoard.Width);
            int row = s_Generator.Next(i_GameBoard.Height);
            while (i_GameBoard.GetLetter(row,colomn) != 0)
            {
                colomn = s_Generator.Next(i_GameBoard.Width);
                row = s_Generator.Next(i_GameBoard.Height);
            }
            i_GameBoard.BoardMatrix[row, colomn] = new Card(i_Letter);
        }
        public static bool IsFinished(Board i_GameBoard)
        {
            bool isFinished = false;
            if (i_GameBoard.OpenCardsAmount == i_GameBoard.Size)
            {
                isFinished = true;
            }

            return isFinished;
        }
        public static void PlayAndGetComputerSelection(Board i_GameBoard, out int o_FirstRow, out int o_FirstColomn, out int o_SecondtRow, out int o_SecondColomn)
        {
            Position firstPosition, secondPosition;
            ManageComputerMemory(i_GameBoard);
            if(!HavePairInMemory(out firstPosition,out secondPosition))
            {
                firstPosition = GeneratePosition(i_GameBoard);
                OpenCard(i_GameBoard, firstPosition.Row, firstPosition.Colomn);
                if (!HaveMatchInMemory(i_GameBoard.GetLetter(firstPosition.Row, firstPosition.Colomn), firstPosition, out secondPosition))
                {
                    secondPosition = GeneratePosition(i_GameBoard);
                }
                OpenCard(i_GameBoard, secondPosition.Row, secondPosition.Colomn);
            }
            else
            {
                OpenCard(i_GameBoard, firstPosition.Row, firstPosition.Colomn);
                OpenCard(i_GameBoard, secondPosition.Row, secondPosition.Colomn);
            }
            o_FirstRow = firstPosition.Row;
            o_FirstColomn = firstPosition.Colomn;
            o_SecondtRow = secondPosition.Row;
            o_SecondColomn = secondPosition.Colomn;
            if(i_GameBoard.GetLetter(firstPosition.Row,firstPosition.Colomn) != i_GameBoard.GetLetter(secondPosition.Row,secondPosition.Colomn))
            {
                CloseCard(i_GameBoard, firstPosition.Row, firstPosition.Colomn);
                CloseCard(i_GameBoard, secondPosition.Row, secondPosition.Colomn);
            }
        }
        private static bool HavePairInMemory(out Position o_FirstPositionInMemory , out Position o_SecondPositionInMemory)
        {
            o_FirstPositionInMemory = new Position(-1,-1);
            o_SecondPositionInMemory = new Position(-1, -1);
            bool haveMatch = false;
            for (int i = 0; i < s_ComputerMemory.Count; i++)
            {
                for (int j = s_ComputerMemory.Count - 1; i < j; j--)
                {
                    if (s_ComputerMemory[i].Letter == s_ComputerMemory[j].Letter)
                    {
                        o_FirstPositionInMemory = s_ComputerMemory[i].Position;
                        o_SecondPositionInMemory = s_ComputerMemory[j].Position;
                        haveMatch = true;
                        break;
                    }
                }
                if (haveMatch)
                {
                    break;
                }
            }

            return haveMatch;
        }
        private static bool HaveMatchInMemory(char i_Letter, Position i_FirstPosition, out Position i_SecondPosition)
        {
            bool isInside = false;
            i_SecondPosition = new Position(-1, -1);
            for (int i = 0; i < s_ComputerMemory.Count; i++)
            {
                if ((s_ComputerMemory[i].Letter == i_Letter) && (s_ComputerMemory[i].Row != i_FirstPosition.Row) && (s_ComputerMemory[i].Colomn != i_FirstPosition.Colomn))
                {
                    isInside = true;
                    i_SecondPosition = s_ComputerMemory[i].Position;
                    break;
                }
            }

            return isInside;
        }
        private static void ManageComputerMemory(Board i_GameBoard)
        {
            for (int i = 0; i < s_ComputerMemory.Count; i++)
            {
                int row = s_ComputerMemory[i].Row;
                int col = s_ComputerMemory[i].Colomn;
                if (i_GameBoard.BoardMatrix[row, col].IsOpen)
                {
                    s_ComputerMemory.RemoveAt(i);
                }
                else
                {
                    int turnsAlive = s_TurnsCount - s_ComputerMemory[i].TurnNumber;
                    int chanceToRemember = GetChanceToRemember(turnsAlive);
                    if (s_Generator.Next(100) >= chanceToRemember)
                    {
                        s_ComputerMemory.RemoveAt(i);
                    }
                }
            }
        }
        private static int GetChanceToRemember(int i_TurnsAlive)
        {
            int chanceToRemember;
            if (i_TurnsAlive == 1)
            {
                chanceToRemember = 90;
            }
            else if (i_TurnsAlive == 2)
            {
                chanceToRemember = 80;
            }
            else if (i_TurnsAlive <= 4)
            {
                chanceToRemember = 60;
            }
            else if (i_TurnsAlive <= 6)
            {
                chanceToRemember = 30;
            }
            else
            {
                chanceToRemember = 15;
            }

            return chanceToRemember;
        }
        private static void AddToMemory(Position i_Position, char i_Letter)
        {
            for (int i = 0; i < s_ComputerMemory.Count; i++)
            {
                if ((s_ComputerMemory[i].Row == i_Position.Row)&& (s_ComputerMemory[i].Colomn == i_Position.Colomn))
                {
                    s_ComputerMemory.RemoveAt(i);
                }
            }
            s_ComputerMemory.Add(new ComputerMemorySlot(i_Position, i_Letter, s_TurnsCount));
        }
        private static Position GeneratePosition(Board i_GameBoard)
        {
            Position position = new Position();
            position.Row = s_Generator.Next(0, i_GameBoard.Height);
            position.Colomn =s_Generator.Next(0, i_GameBoard.Width);
            while (i_GameBoard.BoardMatrix[position.Row,position.Colomn].IsOpen)
            {
                position.Row = s_Generator.Next(0, i_GameBoard.Height);
                position.Colomn = s_Generator.Next(0, i_GameBoard.Width);
            }

            return position;
        }
        public static void OpenCard(Board i_Board, int i_Row, int i_Colomn)
        {
            i_Board.OpenCard(i_Row, i_Colomn);
            if (s_ComputerMemory != null)
            {
                AddToMemory(new Position(i_Row, i_Colomn), i_Board.GetLetter(i_Row, i_Colomn));
            }
            if (i_Board.OpenCardsAmount%2 == 0)
            {
                s_TurnsCount++;
            }
        }
        public static void AgainstComputer()
        {
            s_ComputerMemory = new List<ComputerMemorySlot>();
        }
        public static void CloseCard(Board i_Board, int i_Row, int i_Colomn)
        {
            i_Board.CloseCard(i_Row, i_Colomn);
        }


    }
}
