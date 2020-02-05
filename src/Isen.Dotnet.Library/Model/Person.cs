using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Isen.Dotnet.Library.Model
{

    public class Person : BaseEntity
    {        
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public DateTime? DateOfBirth {get;set;}
        
        [NotMapped] // ne pas générer ce champ dans la bdd
        public int? Age => DateOfBirth.HasValue ?
            // Nb de jours entre naissance et today / 365
            (int)((DateTime.Now - DateOfBirth.Value).TotalDays / 365) :
            // Pas de date de naissance alors, un entier null
            new int?();
 
        public string Email {get;set;}
        
        public string NoTel { get; set; }
        public Service Service {get;set;}
        public int? ServiceId {get;set;}
        public ICollection<RolePerson> rolepersons {get;set;}
        
        public override string ToString() =>
            $"{FirstName} {LastName} | {DateOfBirth} ({Age}) | {Email} | {NoTel} | {Service} | {rolepersons} ";
        
    }
}