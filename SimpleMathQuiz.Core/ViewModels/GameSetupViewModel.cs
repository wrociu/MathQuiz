using SimpleMathQuiz.Core.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMathQuiz.Core.ViewModels
{
    public class GameSetupViewModel : BaseViewModel, INotifyPropertyChanged, ISimpleQuizViewModel
    {
        private MainViewModel _mainViewModel;

        public GameSetupViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


        private int _numberOfQuestion;
        public int NumberOfQuestions
        {
            get
            {
                return _numberOfQuestion;
            }

            set
            {
                _numberOfQuestion = value;
                _mainViewModel.NumberOfQuestions = _numberOfQuestion;
                RaisePropertyChanged();
            }
        }

        private int _gameLevel;
        public int GameLevel
        {
            get => _gameLevel;
            set
            {
                _gameLevel = value;
                _mainViewModel.GameLevel = _gameLevel;
                RaisePropertyChanged();
            }
        }

        public string ViewModelName
        {
            get => "GameSetupViewModel";
        }        
    }
}
