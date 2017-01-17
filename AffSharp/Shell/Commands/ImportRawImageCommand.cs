//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleToolkit.CommandLineInterpretation.ConfigurationAttributes;
using ConsoleToolkit.ConsoleIO;

namespace Shell.Commands
{
    /// <summary>
    /// This command imports the source raw image file to the specified target volume.   
    /// </summary>
    [Command]
    class ImportRawImageCommand
    {
        string m_sourceFile;
        string m_targetVolume;

        [Positional]
        [Description("Path to the source image file.")]
        public string SourceFile { set { m_sourceFile = value; } }

        [Positional]
        [Description("URN specification of the Volume Object in which the Image Object is to be stored.")]
        public string TargetVolume { set { m_targetVolume = value; } }


        [CommandHandler]
        public void Handle(IConsoleAdapter console, IErrorAdapter error)
        {
                
        }
    }
}
