using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.DTO
{
    public class UpdateUserDTO
    {
        // "?" means that it's optional to assign the property. This means that it's allowed to store null values even when sending requests to the API.
        // This means if you only want to change the Username for example, you only need to mention the Username in the request and won't receive any errors due the "?" sign.

        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
