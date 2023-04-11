using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GPTW.Helper
{
    public class Question
    {

        [DataMember]
        public int QuestionID { get; set; }
        
        [DataMember]
        public double Score { get; set; }
        [DataMember]
        public string QuestionText { get; set; }


    }

    public class Questions
    {

        [DataMember]
        public List<String> QuestionID { get; set; }
    }
}