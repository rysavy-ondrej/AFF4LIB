using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afx.Model
{
    /// <summary>
    /// Represents Stream interface. It provides operations for reading/writing segments of the stream.
    /// </summary>
    public class AfxStream
    {
        AfxObject m_object;
        /// <summary>
        /// Creates a new <see cref="AfxStream"/> object.
        /// </summary>
        /// <param name="obj"><see cref="AfxObject"/> that should support stream interface.</param>
        public AfxStream(AfxObject obj)
        {
            m_object = obj;            
        }

        /// <summary>
        /// Reads specified number of segments from the specified offset.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Segment[] Read(long offset, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the passed segments from the specified offset.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="segments"></param>
        void Write(long offset, Segment[] segments)
        {
            throw new NotImplementedException();
        }
    }
}
