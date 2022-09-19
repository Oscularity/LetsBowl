using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsBowl.WPF
{ 
    public class Frame : BaseViewModel
    {
        public event EventHandler FrameCompleted;
        public event EventHandler ScoreUpdated;
        protected virtual void OnScoreUpdated(EventArgs e)
        {
            EventHandler handler = ScoreUpdated;
            handler?.Invoke(this, e);
        }
        protected virtual void OnFrameCompleted(EventArgs e)
        {
            EventHandler handler = FrameCompleted;
            handler?.Invoke(this, e);
        }

        public Frame(Bowler parent)
        {
            Parent = parent;
        }

        public int SetScore(int BallToScore, int PinsToBragAbout)
        {
            if (PinsToBragAbout < 0 || PinsToBragAbout > 10)
                throw new ArgumentOutOfRangeException("Value must be between 0 and 10");

            if (BallToScore < 1 || BallToScore > 3)
                throw new ArgumentOutOfRangeException("Value must be between 1 and 3");

            //Yes, we are ignoring the else here...
            if (CanAddScore(BallToScore))
            {
                switch (BallToScore)
                {
                    case 1:
                        Ball1 = PinsToBragAbout;
                        break;
                    case 2:
                        Ball2 = PinsToBragAbout;
                        break;
                    case 3:
                        Ball3 = PinsToBragAbout;
                        break;
                    default:
                        throw new Exception("Too many balls in my bowling alley");
                }
                //Here I would send through a custome EA with the Frame and the score and stuff, you know, be helpful            
                OnScoreUpdated(new EventArgs());
            }

            return (int)Score;
        }

        public bool CanAddScore(int BallToScore)
        {
            if (BallToScore < 1 || BallToScore > 3)
            {
                throw new Exception("Value must be between 1 and 3"); //I would make this more useful, 1/2 except on the last frame
            }

            //should this be an exception or just return false?
            if ((BallToScore == 3) && (NextFrame != null))
            {
                throw new Exception("Can only roll three balls in the last frame");
            }

            if (BallToScore == 1) return true;

            if (NextFrame != null)  //all frames but the last one we dont score second ball with a strike
            {
                if (Ball1 == null) return false;
                return (Ball1 != 10);
            }

            if (BallToScore == 2)
            {
                return (Ball1 != null);
            }

            //checking the third ball here
            return (Ball1 != null && Ball2 != null);

        }

        public bool IsFrameComplete
        {
            get
            {
                if (Ball1 == null) return false;

                if (NextFrame == null)//Last frame so we have to check if all balls are thrown
                {
                    //if last frame and we strike with the first ball we have to have both ball scores to be complete
                    if (Ball1 == 10) return (!(Ball2 == null || Ball3 == null));

                    //here we have a spare so we need all three again
                    if (Ball1 + (Ball2 ?? 0) == 10) return Ball3 != null;

                    //otherwise we need both values
                    return (Ball1 != null && Ball2 != null);
                }
                var isframecomplete = IsAStrike ? true : (Ball1 != null && Ball2 != null);
                if (isframecomplete)
                {
                    Parent.ReScore();
                    OnPropertyChanged("RunningScore");
                }
                return isframecomplete;

            }
        }

        public bool IsAStrike
        {
            get { return (Ball1 ?? 0) == 10; }
        }

        public bool IsASpare
        {
            get
            {
                if ((Ball1 == null) || (Ball2 == null) || Ball1 == 10)
                    return false;

                return Ball1 + Ball2 == 10;
            }
        }

        public int? Score
        {
            get
            {
                int response = 0;
                if (IsAStrike)
                {
                    response = 10;
                    if (FrameNo == 10)
                    {
                        response += (Ball2 ?? 0) + (Ball3 ?? 0);
                    }
                    else
                    {
                        if (FrameNo == 9)
                        {
                            response += (NextFrame?.Ball1 ?? 0) + (NextFrame?.Ball2 ?? 0);
                        }
                        else
                        {
                            response += (NextFrame?.Ball1 ?? 0);
                            if (NextFrame.IsAStrike)
                            {
                                response += (NextFrame?.NextFrame.Ball1 ?? 0);
                            }
                            else
                            {
                                response += (NextFrame?.Ball2 ?? 0);
                            }
                        }

                    }
                }
                else if (IsASpare)
                {
                    response = 10;
                    if (FrameNo==10)
                    {
                        response += (Ball2 ?? 0) + (Ball3 ?? 0);
                    }
                    else
                    {
                        response += (NextFrame?.Ball1 ?? 0);
                    }
                }
                else
                {
                    response = (Ball1 ?? 0) + (Ball2 ?? 0);
                }

                return response;
            }
        }

        private int? ball1;
        public int? Ball1
        {
            get => ball1;
            set
            {
                if (value == null)
                {
                    SetProperty(ref ball1, null);
                }
                else if ((value < 0) || (value > 10))
                {
                    SetProperty(ref ball1, null);
                    return;
                }
                SetProperty(ref ball1, value);
                OnPropertyChanged("IsAStrike");
                OnPropertyChanged("Score");
                OnPropertyChanged("IsFrameComplete");

            }
        }

        private int? ball2;
        public int? Ball2
        {
            get => ball2;
            set
            {
                //if the value is null or if the total of the two balls is more than 10 or this ball is more that 10 or less that 0
                if ((value == null) || ((value < 0) || (value > 10)))
                {
                    SetProperty(ref ball2, null);
                }
                else if ((FrameNo != 10) && (ball1 ?? 0) + (int)value > 10)
                {
                    SetProperty(ref ball2, null);
                }
                else
                {
                    SetProperty(ref ball2, value);
                }
                OnPropertyChanged("IsASpare");
                OnPropertyChanged("Score");
                OnPropertyChanged("IsFrameComplete");
            }
        }

        private int? ball3;
        public int? Ball3
        {
            get => ball3;
            set
            {
                if ((value == null) || (FrameNo != 10))
                {
                    SetProperty(ref ball3, null);
                } 
                else if ((value < 0) || (value > 10))
                {
                    SetProperty(ref ball3, null);
                    return;
                }
                SetProperty(ref ball3, value);
                OnPropertyChanged("Score");
                OnPropertyChanged("IsFrameComplete");
            }
        }

        private int runningScore;
        public int RunningScore
        {
            get => runningScore;
            set => SetProperty(ref runningScore, value);
        }

        public int FrameNo { get; set; }

        public Frame LastFrame { get; set; }    // Used to calculate score
        public Frame NextFrame { get; set; }    // Used to calculate score
        private Bowler Parent { get; set; } //To get info about frames and stuff

    }
}
