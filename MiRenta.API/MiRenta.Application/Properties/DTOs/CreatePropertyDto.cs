using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Application.Properties.DTOs
{
    public class CreatePropertyDto
    {
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal MonthlyRent { get; set; }
    }
}
