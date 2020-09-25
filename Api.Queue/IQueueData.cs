using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue
{
    public interface IQueueData
    {
        void ToDb();
        void Enqueue(object obj);
    }
}
