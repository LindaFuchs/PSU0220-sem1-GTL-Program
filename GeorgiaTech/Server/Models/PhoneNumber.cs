﻿using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class PhoneNumber
    {
        public int MemberId { get; set; }
        public string Number { get; set; }

        public virtual Member Member { get; set; }
    }
}
