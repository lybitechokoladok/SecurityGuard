﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public RequestType Type { get; set; }
        public string VisitingReason { get; set; } = string.Empty;
        public DateTime ArrivalDate { get; set; } 
        public DateTime CreationDate { get; set; }
        public Client Clients { get; set; }
        public int? GroupId { get; set; }

        public RequestDetails RequestDetails { get; set; }

        public override string ToString()
        {
            return Clients.FirstName + " " + Clients.LastName + " " + Clients.Patronomic;
        }
    }
}
