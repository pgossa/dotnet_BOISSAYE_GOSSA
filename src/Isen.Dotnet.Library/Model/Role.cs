using System;
using System.Collections.Generic;

namespace Isen.Dotnet.Library.Model
{
    public class Role : BaseEntity{
        public string role {get;set;}
        public ICollection<RolePerson> rolepersons {get;set;}
    }
}
