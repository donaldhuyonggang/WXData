using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXDataUI.Models
{
    /// <summary>
    /// 返回结果类
    /// </summary>
    public class ReturnResult
    {
        public ReturnResult()
        {
            Result = true;
        }
        public ReturnResult(string ErrorCode,string ErrorMsg,bool Result)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMsg = ErrorMsg;
            this.Result = Result;
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Result { get; set; }
    }
}