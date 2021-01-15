using Bookah.Data.Interfaces;
using Bookah.Data.Utilities;
using Bookah.Models;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookah.Data
{
    public class UserRepo : IUserRepo
    {
        public async Task Create(AppUser appUser)
        {

            AppHelper.client = new FireSharp.FirebaseClient(AppHelper.config);
            var data = appUser;
            FirebaseResponse response = await AppHelper.client.PushAsync("User/", data);
        }
    }
}
