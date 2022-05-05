using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.UserEntities
{
    public class UsersRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UsersRolesId { get; set; }

        [ForeignKey("RoleId")]
        public Guid? RoleId { get; set; }

        public Role Role { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }

        public User User { get; set; }
    }
}
