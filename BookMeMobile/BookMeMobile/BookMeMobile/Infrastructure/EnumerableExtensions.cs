using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static int IndexOf(this IEnumerable self, object obj)
        {
            int index = -1;

            var enumerator = self.GetEnumerator();
            enumerator.Reset();
            int i = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == obj)
                {
                    index = i;
                    break;
                }

                i++;
            }

            return index;
        }
    }
}