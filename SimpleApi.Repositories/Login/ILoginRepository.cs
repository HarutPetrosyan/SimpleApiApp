using SimpleApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Repositories.Login
{
    public  interface ILoginRepository
    {
        LoginModel Get(string username, string password);
    }
}
