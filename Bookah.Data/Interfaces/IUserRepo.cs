using Bookah.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookah.Data.Interfaces
{
    public interface IUserRepo
    {
        Task Create(AppUser appUser);
    }
}
