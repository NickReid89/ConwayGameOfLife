using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Nickolas_Reid___Game_of_life
{
    public partial class Form1 : Form
    {
        
        // MAkes random numbers through out the program
        Random rand = new Random();
        // The "years" a cell will go through.
        static int CellGenerations = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Creates the grid for the cells to live on.
            CreateBoard();
            // Populates cells
            MakeCell();
            // gets rid of a cell being auto selected.
            CellReality.ClearSelection();
            // Starts the cells life scale.
            timer1.Start();
            
        }
        private void CreateBoard()
        {
            // creates the columns for the cells.
            for (int i = 0; i < 20; i++)
            {
                CellReality.Columns.Add(new DataGridViewColumn() { CellTemplate = new DataGridViewTextBoxCell() });
                DataGridViewColumn column = CellReality.Columns[i];
                column.Width = 20;
                CellReality.Rows.Add();
            }
            // Creates the black frame or "walls" for the cells to live in.
            for (int i = 0; i < 20; i++)
            {
                CellReality.Rows[0].Cells[i].Style.BackColor = Color.Black;
                CellReality.Rows[19].Cells[i].Style.BackColor = Color.Black;
                CellReality.Rows[i].Cells[0].Style.BackColor = Color.Black;
                CellReality.Rows[i].Cells[19].Style.BackColor = Color.Black;
            }
            
        }
        private void MakeCell()
        {      
            // Using the random generator I create between 1 and 400 cells on the board.
            for (int i = 0; i < rand.Next(1,400); i++)
            {
                CellReality.Rows[rand.Next(1, 19)].Cells[rand.Next(1, 19)].Style.BackColor = Color.PowderBlue;
            }
        }

        private void CheckBoard()
        {
            // Checks to see if there is any life at all on the board, it's senseless to continue
            // generations if there is not.
             bool life = false;
            // Going through the cells to check for life.
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (CellReality.Rows[i].Cells[j].Style.BackColor == Color.PowderBlue)
                    {
                        life = true;
                    }
                        // Calls check for life to do an evaluation to see if a cell lives or dies.
                        CheckForLife(i, j);                 
                }
            }
            // All life has died.
            if(!life){
                // Stop the generations.
                timer1.Stop();
                MessageBox.Show("All life has died");
            }
        }
        private void CheckForLife(int row, int col)
        {
            // Counter based on the rules of living or dying.
            int counter = 0;
            // Checks if the cells can grow within bounds.
            if(row >= 1 && col >= 1 && row <= 18 && col <= 18){
                // Checks the positioning around the cell. Like this:
                // x|x|x
                // x| |x
                // x|x|x
                if (CellReality.Rows[row - 1].Cells[col].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row + 1].Cells[col].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row].Cells[col - 1].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row].Cells[col + 1].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row - 1].Cells[col - 1].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row - 1].Cells[col + 1].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row + 1].Cells[col - 1].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                if (CellReality.Rows[row + 1].Cells[col + 1].Style.BackColor == Color.PowderBlue)
                {
                    counter++;
                }
                // Go to the method to determine life and death of a cell.
                LifeOrDie(counter, row, col);
            }

        }
        private void LifeOrDie(int lives,int row,int col)
        {
            
            if (lives > 3)
            {
                CellReality.Rows[row].Cells[col].Style.BackColor = Color.White;
            }
            else if (lives == 3)
            {
                CellReality.Rows[row].Cells[col].Style.BackColor = Color.PowderBlue;

            }
            else if (lives == 2 || lives == 3) {
              //Experimental
                // CellReality.Rows[row].Cells[col].Style.BackColor = Color.Purple;
            }

            else
            {
                CellReality.Rows[row].Cells[col].Style.BackColor = Color.White;
            } 
        }
        private void StartGame()
        {       
                // Checks the board for life.
                CheckBoard();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Start the game.
            StartGame();
            // Year has passed for cells.
            CellGenerations++;
            // Show generation at bottom of the screen.
            lblGeneration.Text = "Generations: " + CellGenerations.ToString();

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set all the cells back to white.
            foreach (DataGridViewRow row in this.CellReality.Rows)
            {
                foreach (DataGridViewCell Cell in row.Cells)
                {
                    Cell.Style.BackColor = Color.White;
                }
            }
            // Creates the black frame or "walls" for the cells to live in.
            for (int i = 0; i < 20; i++)
            {
                CellReality.Rows[0].Cells[i].Style.BackColor = Color.Black;
                CellReality.Rows[19].Cells[i].Style.BackColor = Color.Black;
                CellReality.Rows[i].Cells[0].Style.BackColor = Color.Black;
                CellReality.Rows[i].Cells[19].Style.BackColor = Color.Black;
            }
            // seed cells
            MakeCell();
            // make sure nothing is selected
            CellReality.ClearSelection();
            //year zero
            CellGenerations = 0;
            // show year zero
            lblGeneration.Text = "Generations: " + CellGenerations.ToString();
            // Bring the cells to life.
            timer1.Start();

            
        }
    }
}
