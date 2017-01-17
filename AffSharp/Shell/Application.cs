//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using ConsoleToolkit.ApplicationStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Application : CommandDrivenApplication
    {
        protected override void Initialise()
        {
            base.HelpCommand<HelpCommand>(c => c.Topic);
            base.Initialise();
        }
    }
}
