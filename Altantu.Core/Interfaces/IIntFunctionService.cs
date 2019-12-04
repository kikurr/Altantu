using System.Collections.Generic;

namespace Altantu.Core.Interface
{
    public interface IIntFunctionService
    {
        #region Methods

        int GetInt(string server, string function, int timeOutSeconds, List<string> parameters);

        #endregion
    }
}
