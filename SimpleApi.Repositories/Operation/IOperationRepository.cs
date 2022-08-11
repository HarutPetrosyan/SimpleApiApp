using SimpleApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Repositories.Operation
{
   public  interface IOperationRepository
    {
        RegistrationModel Get(string FirstName, string SecondName);
    }
}
