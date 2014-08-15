using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGroupMembersSync.Model
{
    public class User
    {
        public string Username { get; set; }
        public string FriendlyName { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public string FacebookAccessToken { get; set; }
        public string FacebookId { get { return _id; } set { _id = value; } }
        public string _id { get; set; }
        
    }
}
