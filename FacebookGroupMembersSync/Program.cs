using Facebook;
using FacebookGroupMembersSync.Model;
using FacebookGroupMembersSync.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacebookGroupMembersSync
{
    class Program
    {
        private static string FacebookAccessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
        private static string FacebookGroupId = ConfigurationManager.AppSettings["FacebookGroupId"];
	private static string Limit = ConfigurationManager.AppSettings["Limit"];

        static void Main(string[] args)
        {
            var client = new FacebookClient(FacebookAccessToken);
            var endpoint = string.Format("{0}/members?limit={1}", FacebookGroupId, Limit);
            
            var repository = new MongoRepository(new RepositoryKeys(), ConfigurationManager.AppSettings["MongoConnection"], ConfigurationManager.AppSettings["MongoDBName"]);

            while (true)
            {
                dynamic members = client.Get(endpoint);
                var actualUsers = new List<User>();

                foreach (dynamic member in (JsonArray)members["data"])
                {
                    string id = member.id;
                    User user = repository.Single<User>(id) ?? new User() ;

                    user._id = id;
                    user.FriendlyName = user.Username = member.name;
                    user.IsAdmin = member.administrator;

                    repository.Save<User>(user);
                    actualUsers.Add(user);
                }

                var savedUsers = repository.All<User>().Select(x => x._id);

                foreach(var user in savedUsers.Where(x => !actualUsers.Any(y => y._id == x)))
                {
                    repository.Delete<User>(user);
                }
                Thread.Sleep(600000);
            }    
        }
    }
}
