namespace Voting.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.CheckRolesAsync();

            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }
                        
            await this.CheckUser("sebas@gmail.com", "Sebastian", "Loaiza", "Customer", "Male", "Student");
            var user1 = await this.CheckUser("cardonaloaizasebastian112@gmail.com", "Sebastian", "Cardona", "Admin", "Male", "Coder");
            var user2 = await this.CheckUser("sara@gmail.com", "Sara", "Cardona", "Customer", "Female", "Student");

            // Add event
            if (!this.context.Events.Any())
            {                
                await this.AddEventsAndCandidatesAsync("Best city of Colombia", 
                    new string[] { "Medellín", "Bogotá", "Cali", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" }, 
                    user1);
                await this.AddEventsAndCandidatesAsync("Do you agree with Medellin's environmental policies?",
                    new string[] { "Yes","No" },
                    user2);
            }
        }

        private async Task<User> CheckUser(string userName, string firstName, string lastName, string role, string gender, string ocupation)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role, gender, ocupation);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string firstName, string lastName, string role, string gender, string ocupation)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Occupation = ocupation,
                Stratum = 3,
                Gender = gender,
                Birthdate = DateTime.Now,
                Email = userName,
                UserName = userName,
                CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
            };

            var result = await this.userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            //var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            //await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }

        private async Task CheckRolesAsync()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
        }


        private async Task AddEventsAndCandidatesAsync(string nameEvent, string[] candidates, User user)
        {

            this.AddEvent(nameEvent, candidates, user);
            
            await this.context.SaveChangesAsync();
        }

        private void AddEvent(string name, string[] candidate, User user)
        {
            var candidates = candidate.Select(c => new Candidate {
                Name = c,
                Proposal = $"Proposal to {c} ",
                ImageUrl = $"~/images/Candidates/{c}.jpg" })
                .ToList();

            this.context.Events.Add(new Event
            {
                Name = name,
                Candidates = candidates,
                Description = "Description to " + name,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now,
                User = user

            });
        }


        
    }

}
