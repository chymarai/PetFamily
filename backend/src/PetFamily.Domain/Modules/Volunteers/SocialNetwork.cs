using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Volunteers
{
    public record SocialNetwork
    {
        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }
        public string Name { get; } = default!;
        public string Url { get; } = default!;

        public static Result<SocialNetwork> Create(string name, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Name can not be empty";


            if (string.IsNullOrWhiteSpace(url))
                return "Url can not be empty";

            var socialNetwork = new SocialNetwork(name, url);

            return socialNetwork;
        }
    }
}
