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
    public class GameViewModel : BaseViewModel, INotifyPropertyChanged, ISimpleQuizViewModel
    {
        private Game _game;

        private Answer[] _answers;
        public bool IsLastQuestionAnswered;
               

        public GameViewModel(int numberOfQuestions, int gameLevel)
        {
            _game = new Game(numberOfQuestions, gameLevel);
            _game.OnLastQuestionAnswered += Game_onLastQuestionAnswered;
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

        private int _numberOfCorrectAnswers;
        public int NumberOfCorrectAnswers
        {
            get { return _numberOfCorrectAnswers; }
            set
            {
                _numberOfCorrectAnswers = value;
                RaisePropertyChanged();
            }
        }

        private int _numberOfWrongAnswers;
        public int NumberOfWrongAnswers
        {
            get { return _numberOfWrongAnswers; }
            set
            {
                _numberOfWrongAnswers = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Methods

        private void GoToStep(int stepNumber)
        {
            QuestionText = _game.GetCurrentQuestionText();
            _answers = _game.GetCurrentQuestionAnswersArray();
            RaisePropertyChanged("Answers");            
        }

        private void HandleAnswer(object param)
        {
            bool bOk = _game.IsAnswerCorrect(new Answer(param.ToString()));
            NumberOfCorrectAnswers = _game.NumberOfCorrectAnswers;
            NumberOfWrongAnswers = _game.NumberOfWrongAnswers;
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
