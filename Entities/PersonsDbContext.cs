using Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public PersonsDbContext(DbContextOptions<PersonsDbContext> options) : base(options)
        {

        }

       

        public DbSet<Person> Persons { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Country>().ToTable("Countries");

            //seed data for country
            string countriesJson = File.ReadAllText("countries.json");
            List<Country>? countriesFromList = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            foreach(var country in countriesFromList)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }

            //seed data for person
            string personsJson = File.ReadAllText("persons.json");
            List<Person>? personsFromList = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);
            
            foreach(var person in personsFromList)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }
        }

        public List<Person> sp_GetAllPersons()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }
    }
}
