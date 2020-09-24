using System;

namespace Decepticon.GenericResult.Tests.Models
{
    public class Client
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Contact ContactInformation {get;set;}
    }
}
