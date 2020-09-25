using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.QueueModel
{
    public class ErrorQuestionQueueModel
    {
        public string SpecialtyCode { get; set; }

        public int QuestionType { get; set; }

        public string QuestionId { get; set; }

        public string ErrorDesc { get; set; }

        public string ErrorTag { get; set; }

        public string SubmitUserId { get; set; }

        public string SubmitDate { get; set; }

        public string SchoolCode { get; set; }

    }
}
