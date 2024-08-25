using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int RestaurantId { get; set; }
    }
}
