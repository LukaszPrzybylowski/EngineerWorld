﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EngineerWorld.Model.Exception
{
    public class ApiException
    {
        public string StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString() 
        {
            return JsonConvert.SerializeObject(this);
        }
    
    }
}
