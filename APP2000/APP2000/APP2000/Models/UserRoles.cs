using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APP2000.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public List<UserRole> Users { get; set; }

        // blir kalt one to many relations in entity framworks
    }
}