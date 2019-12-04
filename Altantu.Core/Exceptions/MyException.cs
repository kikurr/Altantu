using System;

namespace Altantu.Core.Exceptions
{
    public class MyException : Exception
    {
        #region Constructors

        public MyException(string message = null, bool isWarning = false) : base(message)
        {
            this.IsWarning = isWarning;
        }

        #endregion

        #region Properties

        public bool IsWarning { get; private set; }

        #endregion
    }
}
