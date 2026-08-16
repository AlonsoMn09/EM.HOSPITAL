using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EM.Hospital.Infraestructure.Configuration.Authentication.Entities
{
    public class UserIdentity : IdentityUser
    {
        public Guid PatientId { get; set; }

        [StringLength(150)]
        public string PatientFullName { get; set; } = default!;
    } 
}
