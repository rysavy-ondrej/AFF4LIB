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
    /// Represents AFX Object that has the unique idnetifier and lives in AFX universe.
    /// </summary>
    public class AfxObject
    {
        /// <summary>
        /// A unique name of the object represented in URN notation.
        /// </summary>
        public string Urn;
        /// <summary>
        /// A dictionary of key, value pairs specifying object attributes following RDF schema.
        /// </summary>
        public Dictionary<string, string> Attributes;
    }
}
