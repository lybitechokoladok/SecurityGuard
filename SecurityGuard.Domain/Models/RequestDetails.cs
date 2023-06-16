﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class RequestDetails
    {
        public int Id { get; set; }
        public int RequestStateId { get; set; }
        public DateTime RequestTimesatmp { get; set; }
        public int UserId { get; set; }
    }
}
