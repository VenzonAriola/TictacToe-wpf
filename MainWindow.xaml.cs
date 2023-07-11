using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        bool stroke = true;
        int move = 0;

        public MainWindow()
        {
            InitializeComponent();
            x_win.IsEnabled = false;
            draw.IsEnabled = false;
            o_win.IsEnabled = false;
            newGame(this, null);

        }

        public void Button_click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (stroke)
                b.Content = "X";
            else
                b.Content = "O";

            stroke = !stroke;
            b.IsEnabled = false;
            move++;
            winnerCheck();

            if ((!stroke) && (t2.Text.ToUpper() == "EASY"))
            {
                pcMoveEasy();
            }

            if ((!stroke) && (t2.Text.ToUpper() == "MID"))
            {
                pcMoveMid();
            }

            if ((!stroke) && (t2.Text.ToUpper() == "HARD"))
            {
                pcMoveHard();
            }

        }

        private void winnerCheck()
        {
            bool winner = false;

            //Horizontal
            if ((A1.Content == A2.Content) && (A2.Content == A3.Content) && (!A1.IsEnabled))
                winner = true;
            else if ((B1.Content == B2.Content) && (B2.Content == B3.Content) && (!B1.IsEnabled))
                winner = true;
            else if ((C1.Content == C2.Content) && (C2.Content == C3.Content) && (!C1.IsEnabled))
                winner = true;

            // Vertical
            if ((A1.Content == B1.Content) && (B1.Content == C1.Content) && (!A1.IsEnabled))
                winner = true;
            else if ((A2.Content == B2.Content) && (B2.Content == C2.Content) && (!A2.IsEnabled))
                winner = true;
            else if ((A3.Content == B3.Content) && (B3.Content == C3.Content) && (!A3.IsEnabled))
                winner = true;

            // Diagonal
            if ((A1.Content == B2.Content) && (B2.Content == C3.Content) && (!A1.IsEnabled))
                winner = true;
            else if ((A3.Content == B2.Content) && (B2.Content == C1.Content) && (!C1.IsEnabled))
                winner = true;


            if (winner)
            {

                //Button_disable();

                A1.IsEnabled = false;
                A2.IsEnabled = false;
                A3.IsEnabled = false;
                B1.IsEnabled = false;
                B2.IsEnabled = false;
                B3.IsEnabled = false;
                C1.IsEnabled = false;
                C2.IsEnabled = false;
                C3.IsEnabled = false;
                string win = "";
                if (stroke)
                {

                    win = t2.Text;
                    o_win.Text = (Int32.Parse(o_win.Text) + 1).ToString();
                }
                else
                {


                    win = t1.Text;
                    x_win.Text = (Int32.Parse(x_win.Text) + 1).ToString();
                }
                MessageBox.Show(win + " Won", "Winner");
                newGame(this, null);
            }
            else
            {

                if (move == 9)
                {
                    draw.Text = (Int32.Parse(draw.Text) + 1).ToString();
                    MessageBox.Show("Draw!!");
                    newGame(this, null);
                }
            }
        }

        private void pcMoveEasy()
        {
            Button move = null;
            move = searchCorner();
            if (move == null)
            {
                move = Vacant();
            }
            try
            {
                move.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            catch { }

        }

        private void pcMoveMid()
        {
            Button move = null;

            move = winBlockCheck("O");
            if (move == null)
            {
                move = winBlockCheck("X");
                if (move == null)
                {
                    move = searchCorner();
                }
            }

            try
            {
                move.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            catch { }
        }
        private void pcMoveHard()
        {
            Button move = null;

            move = firstMoveO();
            if (move == null)
            {
                move = winBlockCheck("O");
                if (move == null)
                {
                    move = winBlockCheck("X");
                    if (move == null)
                    {
                        move = secondMoveTactics("X");
                        if (move == null)
                        {

                            move = searchCorner();


                        }
                    }
                }
            }

            try
            {
                move.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            catch { }

        }


        private Button winBlockCheck(string mark)
        {
            //horizontal
            if ((A1.Content == mark) && (A2.Content == mark) && (A3.Content == ""))
                return A3;
            else if ((A2.Content == mark) && (A3.Content == mark) && (A1.Content == ""))
                return A1;
            else if ((A1.Content == mark) && (A3.Content == mark) && (A2.Content == ""))
                return A2;

            else if ((B1.Content == mark) && (B2.Content == mark) && (B3.Content == ""))
                return B3;
            else if ((B2.Content == mark) && (B3.Content == mark) && (B1.Content == ""))
                return B1;
            else if ((B1.Content == mark) && (B3.Content == mark) && (B2.Content == ""))
                return B2;

            else if ((C1.Content == mark) && (C2.Content == mark) && (C3.Content == ""))
                return C3;
            else if ((C2.Content == mark) && (C3.Content == mark) && (C1.Content == ""))
                return C1;
            else if ((C1.Content == mark) && (C3.Content == mark) && (C2.Content == ""))
                return C2;

            //Vertical
            else if ((A1.Content == mark) && (B1.Content == mark) && (C1.Content == ""))
                return C1;
            else if ((B1.Content == mark) && (C1.Content == mark) && (A1.Content == ""))
                return A1;
            else if ((A1.Content == mark) && (C1.Content == mark) && (B1.Content == ""))
                return B1;

            else if ((A2.Content == mark) && (B2.Content == mark) && (C2.Content == ""))
                return C2;
            else if ((B2.Content == mark) && (C2.Content == mark) && (A2.Content == ""))
                return A2;
            if ((A2.Content == mark) && (C2.Content == mark) && (B2.Content == ""))
                return B2;

            else if ((A3.Content == mark) && (B3.Content == mark) && (C3.Content == ""))
                return C3;
            else if ((B3.Content == mark) && (C3.Content == mark) && (A3.Content == ""))
                return A3;
            else if ((A3.Content == mark) && (C3.Content == mark) && (B3.Content == ""))
                return B3;

            //Diagonal
            else if ((A1.Content == mark) && (B2.Content == mark) && (C3.Content == ""))
                return C3;
            else if ((B2.Content == mark) && (C3.Content == mark) && (A1.Content == ""))
                return A1;
            else if ((A1.Content == mark) && (C3.Content == mark) && (B2.Content == ""))
                return B2;

            else if ((A3.Content == mark) && (B2.Content == mark) && (C1.Content == ""))
                return C1;
            else if ((B2.Content == mark) && (C1.Content == mark) && (A3.Content == ""))
                return A3;
            else if ((A3.Content == mark) && (C1.Content == mark) && (B2.Content == ""))
                return B2;

            return null;
        }
        private Button firstMoveO()
        {
            if (B2.Content == "")
                return B2;
            else
                return null;
        }
        private Button secondMoveTactics(string mark)
        {
            if ((A2.Content == mark) && (B1.Content == mark) && (A1.Content == ""))
                return A1;
            else if ((A2.Content == mark) && (B3.Content == mark) && (A3.Content == ""))
                return A3;
            else if ((B1.Content == mark) && (C2.Content == mark) && (C1.Content == ""))
                return C1;
            else if ((B3.Content == mark) && (C2.Content == mark) && (C3.Content == ""))
                return C3;

            else if ((A1.Content == mark) && (C3.Content == mark) && (A2.Content == ""))
                return A2;
            else if ((A3.Content == mark) && (C1.Content == mark) && (B1.Content == ""))
                return B1;

            else if ((A1.Content == mark) && (C2.Content == mark) && (C1.Content == ""))
                return C1;
            else if ((A3.Content == mark) && (C2.Content == mark) && (C3.Content == ""))
                return C3;
            else if ((C3.Content == mark) && (B1.Content == mark) && (C1.Content == ""))
                return C1;
            else if ((C3.Content == mark) && (A2.Content == mark) && (A3.Content == ""))
                return A3;
            else if ((C1.Content == mark) && (A2.Content == mark) && (A1.Content == ""))
                return A1;
            else if ((C1.Content == mark) && (B3.Content == mark) && (C3.Content == ""))
                return C3;
            else
                return null;
        }

        private Button searchCorner()
        {



            if (A1.Content == "O")
            {
                if (A3.Content == "")
                    return A3;
                else if (C3.Content == "")
                    return C3;
                else if (C1.Content == "")
                    return C1;
            }

            else if (A3.Content == "O")
            {
                if (A1.Content == "")
                    return A1;
                else if (C3.Content == "")
                    return C3;
                else if (C1.Content == "")
                    return C1;
            }

            else if (C3.Content == "O")
            {
                if (A1.Content == "")
                    return A3;
                else if (A3.Content == "")
                    return A3;
                else if (C1.Content == "")
                    return C1;
            }

            else if (C1.Content == "O")
            {
                if (A1.Content == "")
                    return A3;
                else if (A3.Content == "")
                    return A3;
                else if (C3.Content == "")
                    return C3;
            }

            else if (A1.Content == "")
                return A1;
            else if (A3.Content == "")
                return A3;
            else if (C1.Content == "")
                return C1;
            else if (C3.Content == "")
                return C3;

            return null;
        }

        private Button Vacant()
        {

            if (A2.Content == "")
                return A2;
            else if (B1.Content == "")
                return B1;
            else if (B2.Content == "")
                return B2;
            else if (B3.Content == "")
                return B3;
            else if (C2.Content == "")
                return C2;
            else
                return null;
        }

        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (b.IsEnabled)
            {
                if (stroke)
                    b.Content = "X";
                else
                    b.Content = "O";
            }
        }
        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.IsEnabled == true)
                b.Content = "";
        }

        private void newGame(object sender, RoutedEventArgs e)
        {
            repeatGame();
        }
        private void repeatGame()
        {
            stroke = true;
            move = 0;

            try
            {
                exception();
            }
            catch { }

        }

        private void exception()
        {
            A1.IsEnabled = true;
            A1.Content = "";
            A2.IsEnabled = true;
            A2.Content = "";
            A3.IsEnabled = true;
            A3.Content = "";
            B1.IsEnabled = true;
            B1.Content = "";
            B2.IsEnabled = true;
            B2.Content = "";
            B3.IsEnabled = true;
            B3.Content = "";
            C1.IsEnabled = true;
            C1.Content = "";
            C2.IsEnabled = true;
            C2.Content = "";
            C3.IsEnabled = true;
            C3.Content = "";

        }

        private void reset(object sender, RoutedEventArgs e)
        {
            x_win.Text = "0";
            draw.Text = "0";
            o_win.Text = "0";

            stroke = true;
            move = 0;

            try
            {
                exception();
            }
            catch { }
        }

        private void save(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("Result.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine();
            sw.Write(t1.Text);
            sw.Write(" ");
            sw.Write(label.Content);
            sw.Write(" ");
            sw.Write(t2.Text);
            sw.WriteLine();
            sw.Write(x_win.Text);
            sw.Write(" ");
            sw.Write(draw.Text);
            sw.Write(" ");
            sw.Write(o_win.Text);
            sw.Close();
            fs.Close();
            MessageBox.Show("Results saved successfully!!");
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "https://www.centennialcollege.ca/", UseShellExecute = true });
        }
    }

}

