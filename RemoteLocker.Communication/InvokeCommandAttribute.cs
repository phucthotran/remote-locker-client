using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication
{
    /// <summary>
    /// Action's attribute for routing action
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited=false)]
    public class InvokeCommandAttribute : System.Attribute
    {
        /// <summary>
        /// Command type
        /// </summary>
        public String CommandType { get; set; }

        /// <summary>
        /// Has input data or not
        /// </summary>
        public bool HasInput { get; set; }

        /// <summary>
        /// Has output or return data or not
        /// </summary>
        public bool HasOutput { get; set; }

        /// <summary>
        /// Invoke a Command type when Output is 'True'
        /// </summary>
        public String OnOutputTrue { get; set; }

        /// <summary>
        /// Invoke a Command type when Output is 'False'
        /// </summary>
        public String OnOutputFalse { get; set; }

        /// <summary>
        /// Default instance with Command type
        /// </summary>
        /// <param name="CommandType">Command type</param>
        public InvokeCommandAttribute(String CommandType, bool HasInput = false, bool HasOutput = false, String OnOutputTrue = "", String OnOutputFalse = "")
        {
            this.CommandType = CommandType;
            this.HasInput = HasInput;
            this.HasOutput = HasOutput;
            this.OnOutputTrue = OnOutputTrue;
            this.OnOutputFalse = OnOutputFalse;
        }
    }
}
