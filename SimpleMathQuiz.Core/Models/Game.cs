using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMathQuiz.Core.Models
{
    public class Game
    {
        public List<Question> Questions { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
        public int NumberOfQuestionAnswers { get; set; }
        private int _currentQuestionNumber;
        private Question _currentQuestion;

        public event EventHandler onLastQuestionAnswered;

        public int CurrentQuestionNumber
        {
            get { return _currentQuestionNumber;  }
            set
            {
                _currentQuestionNumber = value;
                if (_currentQuestionNumber == NumberOfQuestions -1)
                {
                    onLastQuestionAnswered?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #region Constructors
        public Game()
        {
            Questions = new List<Question>();
            NumberOfCorrectAnswers = 0;
            NumberOfQuestions = 5;
            NumberOfQuestionAnswers = 4;
            InitializeTheGame(NumberOfQuestions, NumberOfQuestionAnswers);
            _currentQuestionNumber = -1;
            
        }
        #endregion

        #region Methods
        public void MoveToNextQuestion()
        {            
            CurrentQuestionNumber++;
            _currentQuestion = Questions.ElementAt<Question>(CurrentQuestionNumber);
        }

        public string GetCurrentQuestionText()
        {
            return _currentQuestion.QuestionTxt;
        }

        public Answer[] GetCurrentQuestionAnswersArray()
        {
            return _currentQuestion.Answers.Values.ToArray();
        }

        public string IsAnswerCorrect(Answer givenAnswer)
        {
            string result;                      

            if (givenAnswer.Equals(_currentQuestion.CorrectAnswer))
            {
                NumberOfCorrectAnswers++;
                result = "The answer is correct.";
            }
            else
            {
                result = "The answer is wrong.";
            }
            result += String.Format($" Number of correct answers: {NumberOfCorrectAnswers}");
            return result;
        }

        private int GenerateRandomAnswer(Random rnd, int expectedResult, int expectedResultIndex, int answerIndex)
        {
            int result;
            if (expectedResultIndex == answerIndex)
            {
                return expectedResult;
            }
            else
            {
                do
                {
                    result = rnd.Next(1, 10);
                } while (result == expectedResult);
            }
            return result;

        }

        public void InitializeTheGame(int numberOfQuestions, int numberOfQuestionAnswers)
        {
            this.NumberOfQuestions = numberOfQuestions;
            
            Random rnd = new Random();
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                int generatedAnswer = 0;
                int a = rnd.Next(1, 10);
                int b = rnd.Next(1, 10);
                int expectedResult = a + b;
                int expectedResultIndex = rnd.Next(1, NumberOfQuestionAnswers + 1);

                Question question = new Question(a.ToString() + " + " + b.ToString() + " = ?");

                for (int j = 1; j < NumberOfQuestionAnswers + 1; j++)
                {
                    generatedAnswer = GenerateRandomAnswer(rnd, expectedResult, expectedResultIndex, j);
                    question.AddAnswer(new Answer(generatedAnswer.ToString()), (generatedAnswer == expectedResult));
                }

                this.Questions.Add(question);
            }
        }
        #endregion




    }
}
