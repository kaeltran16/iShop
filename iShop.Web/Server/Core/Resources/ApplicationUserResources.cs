using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ApplicationUserResources
    {
       
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }

        public ApplicationUserResources()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
