using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afx.Model
{
    /// <summary>
    /// Represents AfxObject of type Image.
    /// </summary>
    public class AfxImage : AfxObject
    {
        int SegmentSize
        { get; set; }

        int ChunksPerSegment
        { get; set; }

        string Compression
        { get; set; }

        int SegmentCount
        { get; set; }

        long Size
        { get; set; }


    }
}
