using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Ex05.MemoryGame.logic;

namespace Ex05.MemoryGame.UI
{
    public class MemoryGameForm : Form
    {
        private Board m_Board;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private bool m_IsFirstSelect = true;
        private MemoryCardButton[,] m_CardsButtons;
        private Label m_CurrentPlayerLabel = new Label();
        private Label m_PlayerOneLabel = new Label();
        private Label m_PlayerTwoLabel = new Label();
        private MemoryCardButton m_ButtonSaver;
        public MemoryGameForm(string i_BoardSize, string i_PlayerOneName, string i_PlayerTwoName )
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Text = "Memory Game";
            StartPosition = FormStartPosition.CenterScreen;
            InitializeMembers(i_BoardSize, i_PlayerOneName, i_PlayerTwoName);
            InitializeControls();
            ClientSize = new Size(m_CardsButtons[0, m_Board.Width-1].Right + 20, m_PlayerTwoLabel.Bottom + 20);
        }

        private void InitializeMembers(string i_BoardSize, string i_PlayerOneName, string i_PlayerTwoName)
        {
            m_Board = new Board(GetHeight(i_BoardSize), GetWidth(i_BoardSize));
            GameManager.FillBoard(m_Board);
            m_PlayerOne = new Player(i_PlayerOneName);
            if ((i_PlayerTwoName == "-computer-")||(i_PlayerTwoName== "Computer"))
            {
                m_PlayerTwo = new Player();
                GameManager.AgainstComputer();
            }
            else
            {
                m_PlayerTwo = new Player(i_PlayerTwoName);
            }
            m_CardsButtons = new MemoryCardButton[GetHeight(i_BoardSize), GetWidth(i_BoardSize)];
        }
        private void InitializeControls()
        {
            m_PlayerOneLabel.Text = m_PlayerOne.Name + ": 0 pairs";
            m_PlayerOneLabel.BackColor = Color.DarkSeaGreen;
            m_PlayerOneLabel.Size = m_PlayerOneLabel.PreferredSize;
            m_PlayerTwoLabel.Text = m_PlayerTwo.Name + ": 0 pairs";
            m_PlayerTwoLabel.BackColor = Color.LightSteelBlue;
            m_PlayerTwoLabel.Size = m_PlayerTwoLabel.PreferredSize;
            m_CurrentPlayerLabel.Text = "Current Player: " + m_PlayerOne.Name;
            m_CurrentPlayerLabel.BackColor = m_PlayerOneLabel.BackColor;
            m_CurrentPlayerLabel.Size = m_CurrentPlayerLabel.PreferredSize;
            SetCardsButtonsPosition();
            m_CurrentPlayerLabel.Left = m_CardsButtons[0, 0].Left;
            m_CurrentPlayerLabel.Top = m_CardsButtons[m_Board.Height-1, 0].Bottom + 15;
            m_PlayerOneLabel.Left = m_CurrentPlayerLabel.Left;
            m_PlayerOneLabel.Top = m_CurrentPlayerLabel.Bottom + 10;
            m_PlayerTwoLabel.Left = m_PlayerOneLabel.Left;
            m_PlayerTwoLabel.Top = m_PlayerOneLabel.Bottom + 10;
            Controls.Add(m_PlayerTwoLabel);
            Controls.Add(m_PlayerOneLabel);
            Controls.Add(m_CurrentPlayerLabel);
        }
        private void SetCardsButtonsPosition()
        {
            for (int i = 0; i < m_Board.Height; i++)
            {
                for (int j = 0; j < m_Board.Width; j++)
                {
                    m_CardsButtons[i, j] = new MemoryCardButton(i, j);
                    if ((i == 0) && (j == 0))
                    {
                        m_CardsButtons[i, j].Top = Top + 20;
                        m_CardsButtons[i, j].Left = Left + 20;
                    }
                    else if (j == 0)
                    {
                        m_CardsButtons[i, j].Left = m_CardsButtons[i - 1, j].Left;
                        m_CardsButtons[i, j].Top = m_CardsButtons[i - 1, j].Bottom + 10;
                    }
                    else
                    {
                        m_CardsButtons[i, j].Top = m_CardsButtons[i, j - 1].Top;
                        m_CardsButtons[i, j].Left = m_CardsButtons[i, j - 1].Right + 10;
                    }
                    Controls.Add(m_CardsButtons[i, j]);
                    m_CardsButtons[i, j].Click += CardButton_Click;
                }
            }
        }
        private void CardButton_Click(object sender, EventArgs e)
        {
            MemoryCardButton buttonSelected = (sender as MemoryCardButton);
            Player currentPlayingPlayer = GetCurrentPlayingPlayer();
            if(!currentPlayingPlayer.IsPerson)
            {
                MessageBox.Show("This is computer's turn, please wait for your turn");
            }
            else if(buttonSelected.Text!="")
            {
                MessageBox.Show("This card is already open, please select another card");
            }
            else if(m_IsFirstSelect)
            {
                GameManager.OpenCard(m_Board, buttonSelected.Row, buttonSelected.Colomn);
                buttonSelected.OpenCardButton(m_CurrentPlayerLabel.BackColor, m_Board.GetLetter(buttonSelected.Row, buttonSelected.Colomn));
                m_ButtonSaver = buttonSelected;
                m_IsFirstSelect = false;
            }
            else
            {
                GameManager.OpenCard(m_Board, buttonSelected.Row, buttonSelected.Colomn);
                buttonSelected.OpenCardButton(m_CurrentPlayerLabel.BackColor, m_Board.GetLetter(buttonSelected.Row, buttonSelected.Colomn));
                if(m_ButtonSaver.Text == buttonSelected.Text)
                {
                    currentPlayingPlayer.Points++;
                    RewritePlayersLabels();
                    if(GameManager.IsFinished(m_Board))
                    {
                        DialogResult = MessageBox.Show(string.Format("{0}\n\n Would you like to play again?", GetResultLine()), "Game Result", MessageBoxButtons.YesNo);
                    }
                }
                else
                {
                    RewriteCurrentPlayerLabel();
                    GameManager.CloseCard(m_Board, buttonSelected.Row, buttonSelected.Colomn);
                    GameManager.CloseCard(m_Board, m_ButtonSaver.Row, m_ButtonSaver.Colomn);
                    this.Refresh();
                    System.Threading.Thread.Sleep(2000);
                    buttonSelected.CloseCardButton();
                    m_ButtonSaver.CloseCardButton();
                    if(!m_PlayerTwo.IsPerson)
                    {
                        PlayComputerTurn();
                    }
                }
                m_IsFirstSelect = true;
            }
        }
        private Player GetCurrentPlayingPlayer()
        {
            Player currentPlayer;
            if(m_CurrentPlayerLabel.BackColor == m_PlayerOneLabel.BackColor)
            {
                currentPlayer = m_PlayerOne;
            }
            else
            {
                currentPlayer = m_PlayerTwo;
            }

            return currentPlayer;
        }
        private void PlayComputerTurn()
        {
            this.Refresh();
            System.Threading.Thread.Sleep(500);
            int firstSelectionRow, firstSelectionColomn;
            int secondSelectionRow, secondSelectionColomn;
            GameManager.PlayAndGetComputerSelection(m_Board, out firstSelectionRow, out firstSelectionColomn, out secondSelectionRow, out secondSelectionColomn);
            m_CardsButtons[firstSelectionRow, firstSelectionColomn].OpenCardButton(m_PlayerTwoLabel.BackColor, m_Board.GetLetter(firstSelectionRow, firstSelectionColomn));
            this.Refresh();
            System.Threading.Thread.Sleep(1500);
            m_CardsButtons[secondSelectionRow, secondSelectionColomn].OpenCardButton(m_PlayerTwoLabel.BackColor, m_Board.GetLetter(secondSelectionRow, secondSelectionColomn));
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            if(m_CardsButtons[firstSelectionRow, firstSelectionColomn].Text == m_CardsButtons[secondSelectionRow, secondSelectionColomn].Text)
            {
                m_PlayerTwo.Points++;
                RewritePlayersLabels();
                if (GameManager.IsFinished(m_Board))
                {
                    DialogResult = MessageBox.Show(string.Format("{0}\n\n Would you like to play again?", GetResultLine()), "Game Result", MessageBoxButtons.YesNo);
                }
                else
                {
                    PlayComputerTurn();
                }
            }
            else
            {
                RewriteCurrentPlayerLabel();
                m_CardsButtons[firstSelectionRow, firstSelectionColomn].CloseCardButton();
                m_CardsButtons[secondSelectionRow, secondSelectionColomn].CloseCardButton();
            }
        }
        public void RewritePlayersLabels()
        {
            if (m_CurrentPlayerLabel.BackColor == m_PlayerOneLabel.BackColor)
            {
                m_PlayerOneLabel.Text = new StringBuilder(m_PlayerOne.Name + ": " + m_PlayerOne.Points + " pairs").ToString();
            }
            else
            {
                m_PlayerTwoLabel.Text = new StringBuilder(m_PlayerTwo.Name + ": " + m_PlayerTwo.Points + " pairs").ToString();
            }
        }
        public void RewriteCurrentPlayerLabel()
        {
            if (m_CurrentPlayerLabel.BackColor == m_PlayerOneLabel.BackColor)
            {
                m_CurrentPlayerLabel.Text ="CurrentPlayer: " + m_PlayerTwo.Name;
                m_CurrentPlayerLabel.BackColor = m_PlayerTwoLabel.BackColor;
            }
            else
            {
                m_CurrentPlayerLabel.Text = "CurrentPlayer: " + m_PlayerOne.Name;
                m_CurrentPlayerLabel.BackColor = m_PlayerOneLabel.BackColor;
            }
            m_CurrentPlayerLabel.Size = m_CurrentPlayerLabel.PreferredSize;
        }
        private int GetHeight(string i_BoardSize)
        {
            int height = int.Parse(i_BoardSize.Substring(0,1));

            return height;
        }
        private int GetWidth(string i_BoardSize)
        {
            int width = int.Parse(i_BoardSize.Substring(2, 1));

            return width;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if(DialogResult==DialogResult.Yes)
            {
                this.Visible = false;
                MemoryGameForm newGame = new MemoryGameForm(string.Format("{0}X{1}", m_Board.Height, m_Board.Width), m_PlayerOne.Name, m_PlayerTwo.Name);
                newGame.ShowDialog();
            }
        }
        public string GetResultLine()
        {
            string result;
            if(m_PlayerOne.Points>m_PlayerTwo.Points)
            {
                result = m_PlayerOne.Name + " is the winner!!!!";
            }
            else if((m_PlayerTwo.Points>m_PlayerOne.Points)&&(m_PlayerTwo.IsPerson))
            {
                result = m_PlayerTwo.Name + " is the winner!!!!";
            }
            else if(m_PlayerTwo.Points > m_PlayerOne.Points)
            {
                result = "The computer won :(";
            }
            else
            {
                result = "Draw, it was a good contest";
            }

            return result;
        }
    }
}
