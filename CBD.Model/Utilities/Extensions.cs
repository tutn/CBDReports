using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CBD.Model.Utilities
{
    public static class Extensions
    {
        public static string SplitString(string input,char c, int index)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                var arr = input.Split(c);
                result = arr[index];

            }
            return result;
        }

        public static string SplitString(string input, string[] st, int index)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                var arr = input.Split(st,StringSplitOptions.None);
                result = arr[index];

            }
            return result;
        }
    }
}
