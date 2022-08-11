using SimpleApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Repositories.Registration
{
   public  interface IRegistrationRepository
    {
         void AddUser(RegistrationModel registration);
         void UpdateUser(RegistrationModel registration);
        void DeleteUser(int id);
    }
}
