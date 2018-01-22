using SimpleMathQuiz.Core.Commands;
using SimpleMathQuiz.Core.Models;
using SimpleMathQuiz.Core.ViewModels.Interfaces;
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
    public class GameViewModel : INotifyPropertyChanged, ISimpleQuizViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Game _game;

        private Answer[] _answers;
        public bool IsLastQuestionAnswered;
               

        public GameViewModel()
        {
            _game = new Game();
            _game.onLastQuestionAnswered += Game_onLastQuestionAnswered;
            _game.MoveToNextQuestion();
            IsLastQuestionAnswered = false;
            GoToStep(_game.CurrentQuestionNumber);            
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

        public string ViewModelName
        {
            get => "GameViewModel";
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
            QuestionText = _game.GetCurrentQuestionText();
            _answers = _game.GetCurrentQuestionAnswersArray();
            RaisePropertyChanged("Answers");            
        }

        private void HandleAnswer(object param)
        {
            UserAnswer = _game.IsAnswerCorrect(new Answer(param.ToString()));
            _game.MoveToNextQuestion();
            if (!IsLastQuestionAnswered)
            {
                GoToStep(_game.CurrentQuestionNumber);
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
