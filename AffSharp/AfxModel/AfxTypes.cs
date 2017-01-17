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
    /// Defines a collection of known AFX types.
    /// </summary>
    public static class AfxTypes
    {
        /// <summary>
        /// A volume provides storage container for other data objects. 
        /// This is a type of abstract volume.
        /// </summary>
        public const string Volume = "volume";
        /// <summary>
        /// Represents Zip64 volume.
        /// </summary>
        public const string ZipVolume = "volume/zip";
        /// <summary>
        /// Represents a simple directory based volume.
        /// </summary>
        public const string DirectoryVolume = "volume/directory";

        /// <summary>
        /// Image is a type that stores a single read-only forensic data set.
        /// </summary>       
        public const string Image = "image";

        /// <summary>
        /// Map is type that enables to expose stream interface for data distributed in a different locations.
        /// </summary>
        public const string Map = "map";

        /// <summary>
        /// Table is an object type that represents a database table.
        /// </summary>
        public const string Table = "table";

        /// <summary>
        /// Link is an object type that enables to create aliases, i.e., short, human readable name for objects.
        /// </summary>
        public const string Link = "link";

        /// <summary>
        /// Identity is a type of objects that represents verifiable entity by X.509 certificate.
        /// </summary>
        public const string Identity = "identity";
    }
}
