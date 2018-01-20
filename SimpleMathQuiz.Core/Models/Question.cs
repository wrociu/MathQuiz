using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMathQuiz.Core.Models
{
    public class Question
    {

        public string QuestionTxt { get; set; }
        public Dictionary<int, Answer> Answers { get; set; }
        public Answer CorrectAnswer { get; set; }

        #region Constructors
        public Question(string questionTxt)
        {
            this.QuestionTxt = questionTxt;
            this.Answers = new Dictionary<int, Answer>();
        }
        #endregion

        #region Methods

        protected int GetMaxKey()
        {
            Dictionary<int, Answer>.KeyCollection keyColl = Answers.Keys;
            int result = (keyColl.Count == 0) ? 1 : keyColl.Max() + 1;
            return result;
        }

        public bool IsAnswerCorrect(int userAnswerIndex)
        {
            Answers.TryGetValue(userAnswerIndex, out Answer userAnser);
            return (userAnser == CorrectAnswer) ? true : false;
        }

        public void AddAnswer(Answer answer, bool isCorrectAnswer)
        {
            int key = GetMaxKey();
            this.Answers.Add(key, answer);
            if (isCorrectAnswer)
            {
                CorrectAnswer = answer;
            }
        }
        #endregion
    }
}
