using System;
using System.Collections.Generic;

namespace Isen.Dotnet.Library.Model
{
    public class RolePerson{
        public int roleId {get;set;}
        public Role role {get;set;}
        public int personId {get;set;}
        public Person person {get;set;}
    }
}
