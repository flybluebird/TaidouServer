using System;
using System.Collections.Generic;
using System.Text;

namespace TaidouCommon
{
    public enum ReturnCode:short
    {
        Success,
        Error,
        Fall,
        Exception,
        GetTeam,
        WaitTeam
    }
}
