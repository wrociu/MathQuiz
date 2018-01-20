using SimpleMathQuiz.Core.Commands;
using SimpleMathQuiz.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleMathQuiz.Core.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Game Game { get; set; }
        private Question[] _questions;
        private Answer[] _answers;
        public bool IsLastQuestionAnswered;
               

        public GameViewModel()
        {
            
            Game = new Game();
            Game.onLastQuestionAnswered += Game_onLastQuestionAnswered;
            Game.MoveToNextQuestion();
            _questions = Game.Questions.ToArray();
            IsLastQuestionAnswered = false;
            GoToStep(Game.CurrentQuestionNumber);
        }

        #region class properties and fields
        private string _questionText;

        public string QuestionText
        {
            get
            {
                return _questionText;
            }
            set
            {
                _questionText = value;
                RaisePropertyChanged();
            }
        }        
        public object[] Answers
        {
            get
            {
                return _answers;                 
            }
        }

        private string _userAnswer;
        public string UserAnswer
        {
            get { return _userAnswer; }
            set
            {
                _userAnswer = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Methods
        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GoToStep(int stepNumber)
        {
            QuestionText = _questions[stepNumber].QuestionTxt;
            _answers = _questions[Game.CurrentQuestionNumber].Answers.Values.ToArray();
            RaisePropertyChanged("Answers");
            
        }

        private void HandleAnswer(object param)
        {
            UserAnswer = Game.IsAnswerCorrect(new Answer(param.ToString()));
            Game.MoveToNextQuestion();
            if (!IsLastQuestionAnswered)
            {
                GoToStep(Game.CurrentQuestionNumber);
            }
        }

        private void Game_onLastQuestionAnswered(object sender, EventArgs e)
        {
            QuestionText = "Last Question Answered. Thank You!";
            IsLastQuestionAnswered = true;
        }
        #endregion

        #region Commands
        private RelayCommandWithParameters answerGivenCommand;

        public ICommand AnswerGivenCommand
        {
            get
            {
                if (answerGivenCommand == null)
                {
                    answerGivenCommand = new RelayCommandWithParameters(HandleAnswer, () => !IsLastQuestionAnswered);
                }

                return answerGivenCommand;

            }
        }

        #endregion


    }
}
