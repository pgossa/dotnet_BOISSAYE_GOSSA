using System;
using System.Collections.Generic;
using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Model;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Library.Services
{
    public class DataInitializer : IDataInitializer
    {
        private List<string> _firstNames => new List<string>
        {
            "Sang", 
            "Anne",
            "Boris",
            "Pierre",
            "Laura",
            "Hadrien",
            "Camille",
            "Louis",
            "Alicia"
        };
        private List<string> _lastNames => new List<string>
        {
            "Schuck",
            "Arbousset",
            "Lopasso",
            "Jubert",
            "Lebrun",
            "Dutaud",
            "Sarrazin",
            "Vu Dinh"
        };

        private List<string> _Tels => new List<string>
        {
            "0612456787",
            "0789543212",
            "0634236513",
            "0768594253",
            "0697845321",
            "0658349275",
            "0768495321",
            "0697842726"
        };
        
        // Générateur aléatoire
        private readonly Random _random;

        // ID de ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(
            ILogger<DataInitializer> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _random = new Random();
        }

        // Générateur de prénom
        private string RandomFirstName => 
            _firstNames[_random.Next(_firstNames.Count)];
        // Générateur de nom
        private string RandomLastName => 
            _lastNames[_random.Next(_lastNames.Count)];

        // Générateur de numéro de téléphone
        private string RandomTel => 
            _Tels[_random.Next(_Tels.Count)];

        // Generateur de services
        private Service RandomService
        {
            get
            {
                var services = _context.ServiceCollection.ToList();
                return services[_random.Next(services.Count)];
            }
        }

        private List<Role> RandomRole
        {
            get
            {
                var roles = _context.RoleCollection.ToList();
                roles.OrderBy(x => _random.Next()).ToList();
                List<Role> returns = roles.OrderBy(x => _random.Next()).ToList().GetRange(0, _random.Next(roles.Count-1)+1);
                return returns;
            }
        }

        // Générateur de date
        private DateTime RandomDate =>
            new DateTime(_random.Next(1980, 2010), 1, 1)
                .AddDays(_random.Next(0, 365));


        // Générateur de personne
        private Person RandomPerson => new Person()
        {
            FirstName = RandomFirstName,
            LastName = RandomLastName,
            DateOfBirth = RandomDate,
            NoTel = RandomTel,
            Service = RandomService,
            rolepersons = new MyCollection<RolePerson>()
        };

        // Genrerateur de roleperson
        private void GetRole(Person parg)
        {
            var randomRole = RandomRole;
            foreach (var _role in randomRole){
                var roleperson = new RolePerson(){ person = parg, role = _role};
                parg.rolepersons.Add(roleperson);
                _role.rolepersons.Add(roleperson);
            }
        }
        // Générateur de personnes
        public List<Person> GetPersons(int size)
        {
            var persons = new List<Person>();
            for(var i = 0 ; i < size ; i++)
            {
                persons.Add(RandomPerson);
                persons[i].Email = persons[i].FirstName.Replace(" ", String.Empty).ToLower() + "." + persons[i].LastName.Replace(" ", String.Empty).ToLower() + "@isen.yncrea.fr";
                GetRole(persons[i]);
            }
            return persons;
        }

        public List<Service> GetServices()
        {
            return new List<Service>
            {
                new Service { service = "Marketing"},
                new Service { service = "Production"},
                new Service { service = "Dev"},
                new Service { service = "HR"}
            };
        }

        public List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { role = "Utilisateur", rolepersons = new MyCollection<RolePerson>()},
                new Role { role = "Manageur", rolepersons = new MyCollection<RolePerson>()},
                new Role { role = "Administrateur", rolepersons = new MyCollection<RolePerson>()},
                new Role { role = "Super-Administrateur", rolepersons = new MyCollection<RolePerson>()}
            };
        }


        public void DropDatabase()
        {
            _logger.LogWarning("Dropping database");
            _context.Database.EnsureDeleted();
        }
            

        public void CreateDatabase()
        {
            _logger.LogWarning("Creating database");
            _context.Database.EnsureCreated();
        }

        public void AddPersons()
        {
            _logger.LogWarning("Adding persons...");
            // S'il y a déjà des personnes dans la base -> ne rien faire
            if (_context.PersonCollection.Any()) return;
            // Générer des personnes
            var persons = GetPersons(50);
            // Les ajouter au contexte
            _context.AddRange(persons);
            // Sauvegarder le contexte
            _context.SaveChanges();
        }

        public void AddServices()
        {
            _logger.LogWarning("Adding services...");
            if (_context.ServiceCollection.Any()) return;
            var services = GetServices();
            _context.AddRange(services);
            _context.SaveChanges();
        }

        public void AddRoles()
        {
            _logger.LogWarning("Adding roles...");
            if (_context.RoleCollection.Any()) return;
            _logger.LogWarning("resgergseg");
            var roles = GetRoles();
            _context.AddRange(roles);
            _context.SaveChanges();
            _logger.LogWarning("OUIIIIII");
        }
    }
}