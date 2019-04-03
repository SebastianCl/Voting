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

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>
                {
                    new City { Name = "Medellín" },
                    new City { Name = "Bogotá" },
                    new City { Name = "Calí" }
                };
                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });                
                await this.context.SaveChangesAsync();
            }

            var user = await this.userHelper.GetUserByEmailAsync("cardonaloaizasebastian112@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Sebastian",
                    LastName = "Cardona",
                    Occupation = "Coder",
                    Stratum = 3,
                    Gender = "Male",
                    Birthdate = DateTime.Now,
                    Email = "cardonaloaizasebastian112@gmail.com",
                    UserName = "cardonaloaizasebastian112@gmail.com",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };
                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }
            /*if (!this.context.Events.Any())
            {
                this.AddEvent("Election of student representative", user);
                this.AddEvent("Do you agree with the new metro line?", user);
                this.AddEvent("Do you prefer Rock or Reggaeton?", user);
                await this.context.SaveChangesAsync();
            }*/

            if (!this.context.Events.Any())
            {
                var candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        Name = "Medellín",
                        Proposal = "Proposal of Medellin",
                        ImageUrl = "~/images/Candidates/Medellin.jpg"

                    },
                    new Candidate
                    {
                        Name = "Bogotá",
                        Proposal = "Proposal of Bogota",
                        ImageUrl = "~/images/Candidates/Bogota.jpg",
                    },
                    new Candidate
                    {
                        Name = "Calí",
                        Proposal = "Proposal of Cali",
                        ImageUrl = "~/images/Candidates/Cali.jpg"
                    },
                    new Candidate
                    {
                        Name = "Pereira",
                        Proposal = "Proposal of Pereira",
                        ImageUrl = "~/images/Candidates/Pereira.jpg"
                    }
                };

                this.context.Events.Add(new Event
                {
                    Name = "What is the best city?",
                    Description = "Description to event ",
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Now,
                    User = user,
                    Candidates = candidates
                });
                await this.context.SaveChangesAsync();
            }
        }
        /*private void AddEvent(string name, User user)
        {
            this.context.Events.Add(new Event
            {
                Name = name,
                Description = "Description to event " + name,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now,
                User = user
            });
        }*/
    }
}
