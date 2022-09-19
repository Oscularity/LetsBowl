using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsBowl.WPF
{
    public class Bowler
    {
        public Frame[] Frames { get; set; }
        public const int MAXFRAMES = 10;

        public Bowler()
        {
        }

        private int GetScore()
        {
            int response = 0;
            for (int i = 0; i < MAXFRAMES; i++)
            {
                var working = Frames[i];
                if (working.Score != null)
                {
                    response += (int)working.Score;
                    Frames[i].RunningScore = response;
                }
                else
                {
                    Frames[i].RunningScore = 0;
                }
            }
            return response;

        }

        public void ReScore()
        {
            for (int i = 0; i < MAXFRAMES; i++)
            {
                var working = Frames[i];
                var score = working.Score;
                var rscore = working.RunningScore;
            }
            GetScore();
        }

        public void CreateScoreCard()
        {
            //there is code in here to allow for changing the number of frames you want to bowl
            //but there are places more code is needed to make that work - and bowlings been around
            //for at least 40 years, I doubt it will change
            if (MAXFRAMES < 1)
            {
                throw new Exception("Attempting to bowl no rounds at all, maybe try rummy");
            }

            Frames = new Frame[MAXFRAMES];
            for (int i = 0; i < MAXFRAMES; i++)
            {
                Frame newFrame = new Frame(this);
                newFrame.FrameNo = i + 1;
                newFrame.LastFrame = i == 0 ? null : Frames[(i - 1)];
                Frames[i] = newFrame;
            }

            //Set the next frame here, we could do it in the loop above
            //but the code gets harder to maintain
            for (int i = 0;i<Frames.Length-1;i++)
            {
                Frames[i].NextFrame = Frames[i + 1];
            }
        }

        //this Frame is 1 based, the array is 0 based
        public void AddScore(int Frame, int Ball, int Pins)
        {
            var working = Frames[Frame - 1];
            if (working.CanAddScore(Ball))
            {
                working.SetScore(Ball, Pins);
            }
        }

    }
}
