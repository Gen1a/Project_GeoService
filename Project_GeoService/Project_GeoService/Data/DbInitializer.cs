using Project_GeoService.Models;
using System.Linq;

namespace Project_GeoService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Project_GeoServiceContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any countries.
            if (context.Countries.Any())
            {
                return;   // DB has been seeded
            }

            var continents = new Continent[]
            {
                new Continent { Name = "Europe" },
                new Continent { Name = "Asia" },
                new Continent { Name = "Africa" },
            };

            foreach (Continent c in continents)
            {
                context.Continents.Add(c);
            }
            context.SaveChanges();

            var countries = new Country[]
            {
                new Country {
                    Name = "Belgium",
                    Population = 11492641,
                    Surface = 30689,
                    ContinentId = continents.Single(c => c.Name == "Europe").Id
                },
                new Country {
                    Name = "France",
                    Population = 67413000,
                    Surface = 640679,
                    ContinentId = continents.Single(c => c.Name == "Europe").Id
                },
                new Country {
                    Name = "Netherlands",
                    Population = 17469635,
                    Surface = 41865,
                    ContinentId = continents.Single(c => c.Name == "Europe").Id
                },
                new Country {
                    Name = "Egypt",
                    Population = 101576517,
                    Surface = 1010408,
                    ContinentId = continents.Single(c => c.Name == "Africa").Id
                },
            };

            foreach (Country c in countries)
            {
                context.Countries.Add(c);
            }
            context.SaveChanges();

            var cities = new City[]
            {
                new City {
                    Name = "Ghent",
                    Population = 248358,
                    Surface = 156.2,
                    CountryId = countries.Single(c => c.Name == "Belgium").Id
                },
                new City {
                    Name = "Brussels",
                    Population = 174383,
                    Surface = 32.61,
                    CountryId = countries.Single(c => c.Name == "Belgium").Id
                },
                new City {
                    Name = "Antwerp",
                    Population = 498473,
                    Surface = 204.5,
                    CountryId = countries.Single(c => c.Name == "Belgium").Id
                },
                new City {
                    Name = "Amsterdam",
                    Population = 821752,
                    Surface = 219.32,
                    CountryId = countries.Single(c => c.Name == "Netherlands").Id
                },
            };

            foreach (City c in cities)
            {
                context.Cities.Add(c);
            }
            context.SaveChanges();

            var countryCapitals = new CountryCapital[]
            {
                new CountryCapital {
                    CountryId = countries.Single(c => c.Name == "Belgium").Id,
                    CityId = cities.Single(c => c.Name == "Brussels").Id
                },
                new CountryCapital {
                    CountryId = countries.Single(c => c.Name == "Netherlands").Id,
                    CityId = cities.Single(c => c.Name == "Amsterdam").Id
                },
            };

            foreach (CountryCapital cc in countryCapitals)
            {
                context.CountryCapitals.Add(cc);
            }
            context.SaveChanges();

            var rivers = new River[]
            {
                new River { Name = "Scheldt", Length = 360 },
                new River { Name = "Demer", Length = 85 },
                new River { Name = "Lesse", Length = 89 },
                new River { Name = "Rhine River", Length = 1230 },
            };

            foreach (River r in rivers)
            {
                context.Rivers.Add(r);
            }
            context.SaveChanges();

            var countryRivers = new CountryRiver[]
            {
                new CountryRiver {
                    CountryId = countries.Single(c => c.Name == "Belgium").Id,
                    RiverId = rivers.Single(r => r.Name == "Scheldt").Id
                },
                new CountryRiver {
                    CountryId = countries.Single(c => c.Name == "Belgium").Id,
                    RiverId = rivers.Single(r => r.Name == "Demer").Id
                },
                new CountryRiver {
                    CountryId = countries.Single(c => c.Name == "Belgium").Id,
                    RiverId = rivers.Single(r => r.Name == "Lesse").Id
                },
                new CountryRiver {
                    CountryId = countries.Single(c => c.Name == "Netherlands").Id,
                    RiverId = rivers.Single(r => r.Name == "Rhine River").Id
                },
            };

            foreach (CountryRiver cr in countryRivers)
            {
                context.CountryRivers.Add(cr);
            }
            context.SaveChanges();
        }
    }
}