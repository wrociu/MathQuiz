using SimpleMathQuiz.Core.Commands;
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
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ICommand _startGameCommand;
        private ICommand _restartGameCommand;

        private ISimpleQuizViewModel _currentPageViewModel;

        public ISimpleQuizViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            CurrentPageViewModel = new GameSetupViewModel();
        }

        public ICommand StartGameCommand
        {
            get
            {
                if (_startGameCommand == null)
                {
                    _startGameCommand = new RelayCommand(StartNewGame, () => CurrentPageViewModel is GameSetupViewModel);
                }

                return _startGameCommand;
            }
        }

        public ICommand RestartGameCommand
        {
            get
            {
                if (_restartGameCommand == null)
                {
                    _restartGameCommand = new RelayCommand(StartNewGame, () => CurrentPageViewModel is GameViewModel);
                }

                return _restartGameCommand;
            }
        }

        public void StartNewGame()
        {
            CurrentPageViewModel = new GameViewModel();
        }




    }
}
