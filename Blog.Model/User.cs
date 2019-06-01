using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Repository.Models
{
 
        public class User : IdentityUser
        {
        
            public string Name { get; set; }
            public string Surname { get; set; }
        public int? Age { get; set; }

        #region dodatkowe pole, ktore nie bedzie mapowane przy tworzeniu bazy

        [NotMapped]
            [Display(Name="Pan/Pani")]
            public string FullName
             {
            get { return Name + " " + Surname; }
             }
        #endregion
        public virtual ICollection<Post> Posts { get; private set; }
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
            {
                // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Dodaj tutaj niestandardowe oświadczenia użytkownika
                return userIdentity;
            }
        }
 }
