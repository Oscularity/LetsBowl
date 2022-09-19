using System;
using System.Collections.Generic;
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

namespace LetsBowl.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var bowler = new Bowler();
            DataContext = bowler;
            bowler.CreateScoreCard();

            //AddData2(bowler);
            //AddData(bowler);
            AddSampleData(bowler);

            dg.ItemsSource = bowler.Frames;

        }

        private void AddSampleData(Bowler bowler)
        {
            bowler.AddScore(1, 1, 4);
            bowler.AddScore(1, 2, 3);
            bowler.AddScore(2, 1, 7);
            bowler.AddScore(2, 2, 3);
            bowler.AddScore(3, 1, 5);
            bowler.AddScore(3, 2, 2);
            bowler.AddScore(4, 1, 8);
            bowler.AddScore(4, 2, 1);
            bowler.AddScore(5, 1, 4);
            bowler.AddScore(5, 2, 6);
            bowler.AddScore(6, 1, 2);
            bowler.AddScore(6, 2, 4);
            bowler.AddScore(7, 1, 8);
            bowler.AddScore(7, 2, 0);
            bowler.AddScore(8, 1, 8);
            bowler.AddScore(8, 2, 0);
            bowler.AddScore(9, 1, 8);
            bowler.AddScore(9, 2, 2);
            bowler.AddScore(10, 1, 10);
            bowler.AddScore(10, 2, 1);
            bowler.AddScore(10, 3, 7);
            UpdateModel();
        }

        private void AddData(Bowler bowler)
        {
            bowler.AddScore(1, 1, 4);
            bowler.AddScore(1, 2, 3);
            bowler.AddScore(2, 1, 7);
            bowler.AddScore(2, 2, 3);
            bowler.AddScore(3, 1, 5);
            bowler.AddScore(3, 2, 2);
            bowler.AddScore(4, 1, 8);
            bowler.AddScore(4, 2, 1);
            bowler.AddScore(5, 1, 4);
            bowler.AddScore(5, 2, 6);
            bowler.AddScore(6, 1, 2);
            bowler.AddScore(6, 2, 4);
            bowler.AddScore(7, 1, 8);
            bowler.AddScore(7, 2, 0);
            bowler.AddScore(8, 1, 8);
            bowler.AddScore(8, 2, 0);
            bowler.AddScore(9, 1, 8);
            bowler.AddScore(9, 2, 2);
            bowler.AddScore(10, 1, 10);
            bowler.AddScore(10, 2, 1);
            bowler.AddScore(10, 3, 7);
        }

        private void AddData2(Bowler bowler)
        {
            bowler.AddScore(1, 1, 10);
            bowler.AddScore(2, 1, 10);
            bowler.AddScore(3, 1, 10);
            bowler.AddScore(4, 1, 10);
            bowler.AddScore(5, 1, 10);
            bowler.AddScore(6, 1, 10);
            bowler.AddScore(7, 1, 10);
            bowler.AddScore(8, 1, 10);
            bowler.AddScore(9, 1, 10);
            bowler.AddScore(10, 1, 10);
            bowler.AddScore(10, 2, 10);
            bowler.AddScore(10, 3, 10);
        }

        private void UpdateModel()
        {
            List<int?> scoresTest = new List<int?>();
            List<int?> scores1Test = new List<int?>();
            var dc = DataContext;
            if (dc != null)
            {
                var frames = ((Bowler)dc).Frames;
                for (int i = 0; i < 10; i++)
                {
                    var working = frames[i];
                    var score = working.Score;
                    scoresTest.Add(score);
                    var rscore = working.RunningScore;
                    scores1Test.Add(rscore);
                }
                var a = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bowler = (Bowler)DataContext;
            if (bowler != null)
            {
                bowler.ReScore();
            }
        }

        private void dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "LastFrame" || e.PropertyName == "NextFrame" || e.PropertyName == "Parent" || e.PropertyName == "" || e.PropertyName == "" || e.PropertyName == "")
            {
                e.Column = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string message;
            var bowler = (Bowler)DataContext;
            if (bowler != null)
            {
                bowler.ReScore();
                int rscore = 0;
                var frames = bowler.Frames;
                if (frames[9].IsFrameComplete)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var working = frames[i];
                        var score = working.Score;
                        rscore = working.RunningScore;
                    }
                }

                message = $"Final Score: {rscore}";
            }
            else
            {
                message = $"Complete all frames to get a final score";
            }

            MessageBox.Show(message);
        }
    }
}
