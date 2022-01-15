using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TheDailyPost.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AuthUser class
    [Table("AspNetUsers")]
    public class AuthUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName="nVarchar(100)")]
        public string Firstname { get; set; }

        [PersonalData]
        [Column(TypeName = "nVarchar(100)")]
        public string Lastname { get; set; }

        [PersonalData]
        [Column(TypeName = "nVarchar(1000)")]
        public string ImageUrl { get; set; }
    }
}
