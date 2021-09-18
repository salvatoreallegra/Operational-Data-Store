using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.DTOs
{
    public class CreateLogDTO
    {
        [Required]
        public string DrName { get; set; }

        [Required]
        public int CenterId { get; set; }

        [Required]
        public int MatchId { get; set; }
    }
}
