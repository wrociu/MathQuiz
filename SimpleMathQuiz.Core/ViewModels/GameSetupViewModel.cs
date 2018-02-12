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
                RaisePropertyChanged();
            }
        }

        public string ViewModelName
        {
            get => "GameSetupViewModel";
        }



    }
}
