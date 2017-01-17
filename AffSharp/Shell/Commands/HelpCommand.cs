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

namespace Shell
{
    
    [Command]
    [Description("Display command line help.")]
    internal class HelpCommand
    {

        [Positional(DefaultValue = null)]
        [Description("The topic on which help is required.")]
        public string Topic { get; set; }

    }
}
