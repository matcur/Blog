using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core
{
    public static class Randomizer
    {
        public const string AlphanumericChars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789";

        public static string GetString(int length = 16)
        {
            if (length < 0)
                throw new ArgumentException($"Length must be more than zero, {length} given");

            var builder = new StringBuilder();
            while (builder.Length != length)
            {
                var str = AlphanumericChars[new Random().Next(AlphanumericChars.Length)];
                builder.Append(str);
            }

            return builder.ToString();
        }
    }
}
