using System.Collections.Generic;

namespace ChipEmailer
{
    public static class StringExtensions
    {
        public static bool StartsWithAny(this string str, IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                if (str.StartsWith(value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
