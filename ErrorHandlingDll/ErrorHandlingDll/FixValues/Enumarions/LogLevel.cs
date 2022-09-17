using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.FixTypes.Enumarions
{
  public enum LogLevel
  {
    //
    // Summary:
    //     Debug.
    [EnumMember(Value = "debug")]
    Debug,
    //
    // Summary:
    //     Informational.
    [EnumMember(Value = "info")]
    Info,
    //
    // Summary:
    //     Warning.
    [EnumMember(Value = "warning")]
    Warning,
    //
    // Summary:
    //     Error.
    [EnumMember(Value = "error")]
    Error,
    //
    // Summary:
    //     Fatal.
    [EnumMember(Value = "fatal")]
    Fatal
  }
}
