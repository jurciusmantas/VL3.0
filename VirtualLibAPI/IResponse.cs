﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibAPI
{
    public interface IResponse
    {
        float Confidence { get; set; }
        string Error_message { get; set; }
    }
}
