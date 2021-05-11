using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Ex05.MemoryGame.logic;

namespace Ex05.MemoryGame.UI
{
    public class MenuForm : Form
    {
        private Label m_FirstPlayerNameLabel = new Label();
        private Label m_SecondPlayerNameLabel = new Label();
        private Label m_BoardSizeLable = new Label();
        private Button m_StartButton = new Button();
        private Button m_ComputerOrFriendButton = new Button();
        private BoardSizeButton m_BoardSizeButton= new BoardSizeButton();
        private TextBox m_FirstPlayerNameTextBox = new TextBox();
        private TextBox m_SecondPlayerNameTextBox = new TextBox();
        public MenuForm()
        {
            this.
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Text = "Memory Game - Settings";
            StartPosition = FormStartPosition.CenterScreen;
            initializeControls();
            ClientSize = new Size(m_StartButton.Right + 20, m_StartButton.Bottom + 20);
        }
        private void initializeControls()
        {
            StartPosition = FormStartPosition.CenterScreen;
            
            // =================== text initialize ============

            m_FirstPlayerNameLabel.Text = "First Player Name:";
            m_SecondPlayerNameLabel.Text = "Second Player Name:";
            m_BoardSizeLable.Text = "Board Size";
            m_StartButton.Text = "Start!";
            m_ComputerOrFriendButton.Text = "Against a Friend";
            m_SecondPlayerNameTextBox.Text = "-computer-";

            //==================== size initialize ===============

            m_FirstPlayerNameLabel.Size = m_FirstPlayerNameLabel.PreferredSize;
            m_SecondPlayerNameLabel.Size = m_SecondPlayerNameLabel.PreferredSize;
            m_BoardSizeLable.Size = m_BoardSizeLable.PreferredSize;
            m_StartButton.Size = new Size(75, 20);
            m_ComputerOrFriendButton.Size = new Size(103, 20);
            m_FirstPlayerNameTextBox.Size = new Size(103, 20);
            m_SecondPlayerNameTextBox.Size = new Size(103, 20);

            //=================== contros adding ===================

            Controls.Add(m_FirstPlayerNameLabel);
            Controls.Add(m_SecondPlayerNameLabel);
            Controls.Add(m_BoardSizeLable);
            Controls.Add(m_StartButton);
            Controls.Add(m_ComputerOrFriendButton);
            Controls.Add(m_BoardSizeButton);
            Controls.Add(m_FirstPlayerNameTextBox);
            Controls.Add(m_SecondPlayerNameTextBox);

            //================== Position Initialize ===================

            m_FirstPlayerNameLabel.Top = Top+20;
            m_FirstPlayerNameLabel.Left = Left+20;
            m_SecondPlayerNameLabel.Top = m_FirstPlayerNameLabel.Bottom + 12;
            m_SecondPlayerNameLabel.Left = m_FirstPlayerNameLabel.Left;
            m_BoardSizeLable.Top = m_SecondPlayerNameLabel.Bottom + 17;
            m_BoardSizeLable.Left = m_SecondPlayerNameLabel.Left;
            m_BoardSizeButton.Top = m_BoardSizeLable.Bottom + 5;
            m_BoardSizeButton.Left = m_BoardSizeLable.Left;
            m_SecondPlayerNameTextBox.Top = m_SecondPlayerNameLabel.Top + m_SecondPlayerNameLabel.Height / 2 - m_SecondPlayerNameTextBox.Height / 2;
            m_SecondPlayerNameTextBox.Left = m_SecondPlayerNameLabel.Right + 12;
            m_FirstPlayerNameTextBox.Top = m_FirstPlayerNameLabel.Top + m_FirstPlayerNameLabel.Height / 2 - m_FirstPlayerNameTextBox.Height / 2;
            m_FirstPlayerNameTextBox.Left = m_SecondPlayerNameTextBox.Left;
            m_ComputerOrFriendButton.Top = m_SecondPlayerNameTextBox.Top;
            m_ComputerOrFriendButton.Left = m_SecondPlayerNameTextBox.Right + 12;
            m_StartButton.Top = m_BoardSizeButton.Bottom - m_StartButton.Height;
            m_StartButton.Left = m_ComputerOrFriendButton.Right - m_StartButton.Width;

            //====================== events initialize =============

            m_BoardSizeButton.Click += m_BoardSizeButton_Click;
            m_ComputerOrFriendButton.Click += m_ComputerOrFriendButton_Click;
            m_StartButton.Click += m_StartButton_Click;


            //====================== else ==========================

            m_StartButton.BackColor = Color.LawnGreen;
            m_SecondPlayerNameTextBox.Enabled = false;
        }
        private void m_StartButton_Click(object sender, EventArgs e)
        {
            if (Player.IsValidName(m_FirstPlayerNameTextBox.Text) && (Player.IsValidName(m_SecondPlayerNameTextBox.Text) || (!m_SecondPlayerNameTextBox.Enabled)))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Players name is not valid, please enter only letters");
            }
        }
        private void m_ComputerOrFriendButton_Click(object sender, EventArgs e)
        {
            if (m_SecondPlayerNameTextBox.Enabled)
            {
                m_SecondPlayerNameTextBox.Enabled = false;
                m_SecondPlayerNameTextBox.Text = "-computer-";
                m_ComputerOrFriendButton.Text = "Against a Friend";
            }
            else
            {
                m_SecondPlayerNameTextBox.Enabled = true;
                m_SecondPlayerNameTextBox.Text = "";
                m_ComputerOrFriendButton.Text = "Against Computer";
            }
        }
        private void m_BoardSizeButton_Click(object sender, EventArgs e)
        {
            m_BoardSizeButton.NextSize();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (Player.IsValidName(m_FirstPlayerNameTextBox.Text) && (Player.IsValidName(m_SecondPlayerNameTextBox.Text) || (!m_SecondPlayerNameTextBox.Enabled)))
            {
                MemoryGameForm game = new MemoryGameForm(m_BoardSizeButton.Text, m_FirstPlayerNameTextBox.Text, m_SecondPlayerNameTextBox.Text);
                this.Visible = false;
                game.ShowDialog();
            }
            else
            {
                MessageBox.Show("Players name is not valid, please enter only letters");
            }
        }
    }
}
