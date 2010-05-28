using System;
using System.Collections;
using System.Collections.Generic;

namespace Bin4Net.Consuming
{
    public class Bin : IEnumerable<RepositoryItem>
    {
        public IEnumerator<RepositoryItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}