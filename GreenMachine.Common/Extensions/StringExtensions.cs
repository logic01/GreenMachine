using System;
using System.Linq;
using System.Text.RegularExpressions;
using TURSS.Common.Security;
using TURSS.SmartMove.Common.Enums;
using TURSS.SmartMove.Common.Security;

namespace GreenMachine.Common.Extensions
{
	public static class StringExtensions
	{
	  /// <summary>
		/// Null-safe trim for a string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string SafeTrim(this string str)
		{
			return (str != null ? str.Trim() : null);
		}
		
     /// <summary>
    /// Prevent a string from exceeding a given number of characters. 
    /// If input string is too long, it will be unceremoniously truncated to the specified length.
    /// </summary>
    public static string LimitTo(this string str, int maxLength)
    {
        if (str.IsNullOrEmpty() || str.Length <= maxLength)
            return str;
        return str.Substring(0, maxLength);
    }
	}
}
