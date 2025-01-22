using System.Security.Cryptography.X509Certificates;

namespace API.Models
{
    public class HackerNewsUser
    {
        public string id { get; set; }
        public int created { get; set; }
        public int karma { get; set; }
        public string about { get; set; }
        public int[] submitted { get; set; }
    }
}
