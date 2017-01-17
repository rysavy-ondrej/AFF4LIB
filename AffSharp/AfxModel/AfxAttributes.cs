//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afx.Model
{
    /// <summary>
    /// Defines a collection of known AFX Attributes.
    /// </summary>
    public static class AfxAttributes
    {
        public const string Type = "afx:type";
        public const string Interface = "afx:interface";
        public const string Timestamp = "afx:timestamp";
        public const string Size = "afx:size";
        public const string Stored = "afx:stored";
        public const string Compression = "afx:compression";
        public const string ChunkSize = "afx:chunk_size";
        public const string ChunksInSegment = "afx:chunks_in_segment";
    }
}
