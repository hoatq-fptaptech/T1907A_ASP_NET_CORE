﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace T1907A.Models
{
    public class Token
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string StrToken { get; set; }
        public string ExpireAt { get; set; }
    }
}
