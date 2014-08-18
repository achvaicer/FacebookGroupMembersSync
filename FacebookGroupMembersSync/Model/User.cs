using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ComponentModel;
using System.IO;

namespace FacebookGroupMembersSync.Model
{
    public class User
    {
		private string[] NotCompiledProperties = new [] { "Hash", "FacebookAccessToken" };

        public string Username { get; set; }
        public string FriendlyName { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public string FacebookAccessToken { get; set; }
        public string FacebookId { get { return _id; } set { _id = value; } }
        public string _id { get; set; }
        public DateTime DateCreated { get; set; }
		public DateTime LastUpdated { get; set; }

		public string Hash { get; private set; }

		public bool HasChanged()
		{
			var generatedHash = GenerateHash ();
			if (generatedHash == Hash)
				return false;

			Hash = generatedHash;
			return true;
		}


		private string GenerateHash()
		{
			var type = typeof(User);
			var props = type.GetProperties ().Where (x => !NotCompiledProperties.Contains (x.Name)).OrderBy (x => x.Name);

			var values = string.Join (";", props.Select (x => x.GetValue (this, null) ?? string.Empty));

			return Convert.ToBase64String(MD5.Create ().ComputeHash (Encoding.ASCII.GetBytes (values)));
		}

    }
}
