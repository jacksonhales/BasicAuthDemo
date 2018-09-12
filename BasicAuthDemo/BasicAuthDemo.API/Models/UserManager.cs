using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthDemo.API.Models
{
    public class UserManager
    {
        private DemoDBEntities _Context;

        public UserManager()
        {
            _Context = new DemoDBEntities();
        }

        public bool ValidateUser(string userName, string password)
        {
            var result = _Context.UserMasters.SingleOrDefault(x =>
                x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.Password == password);
            return result != null ? true : false;
        }

    }
}