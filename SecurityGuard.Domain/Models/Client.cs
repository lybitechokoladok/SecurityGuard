using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronomic { get; set; } = string.Empty;
        public string? Organization { get; set; } = null;
        public string Phone { get;set; } = string.Empty;
        public string PasswordNumber { get; set; } = string.Empty;
        public string PasswordSeries { get;set; } = string.Empty;
    }
}
