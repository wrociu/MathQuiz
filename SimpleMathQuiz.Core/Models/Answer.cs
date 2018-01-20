using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMathQuiz.Core.Models
{
    public class Answer
    {
        public string AnswerTxt { get; set; }

        public Answer(string answerTxt)
        {
            this.AnswerTxt = answerTxt;
        }

        public override string ToString()
        {
            return AnswerTxt;
        }
    }
}
