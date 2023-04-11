    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTW.Helper
{
    internal interface IResponseModel
    {

        string Message { get; set; }
        bool Success { get; set; }
    }

    public class ResponseModel<T> : IResponseModel
    {
        public T data { get; set; }
        public String positive_score{ get; set; }
       
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
