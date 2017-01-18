//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonLD;
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
        public string Id;


        /// <summary>
        /// Represent a type of the object. See <seealso cref="AfxTypes"/> for known types.
        /// </summary>
        public string Type
        {
            get { return Attributes[AfxAttributes.Type]; }
            set { Attributes[AfxAttributes.Type] = value; }
        }


        public string Interface
        {
            get; set;
        }

        public string Stored
        {
            get; set;
        }

        /// <summary>
        /// A dictionary of key, value pairs specifying object attributes following RDF schema.
        /// </summary>
        public Dictionary<string, string> Attributes;

        /// <summary>
        /// Gets the JSON-LD representation of the current Object.
        /// </summary>
        /// <returns></returns>
        public string GetJsonLD()
        {
            throw new NotImplementedException();
        }            
    }
}
