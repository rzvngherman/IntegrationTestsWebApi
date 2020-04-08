using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Api.Models.Error.SeedWork
{
    [Serializable]
    class InvalidCommandException : Exception
    {
        public Dictionary<string, List<string>> Errors;

        public InvalidCommandException(Dictionary<string, List<string>> errors)
        {
            Errors = errors;
        }
    }
}
