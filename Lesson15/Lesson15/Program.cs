using Newtonsoft.Json;

namespace LinqLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = DeserializePersonsFromJsonFile("data.json");

            Person farthestNorth = persons.OrderByDescending(p => p.Latitude).First();
            Person farthestSouth = persons.OrderBy(p => p.Latitude).First();
            Person farthestWest = persons.OrderBy(p => p.Longitude).First();
            Person farthestEast = persons.OrderByDescending(p => p.Longitude).First();

            Console.WriteLine("Farthest North: " + farthestNorth.Name);
            Console.WriteLine("Farthest South: " + farthestSouth.Name);
            Console.WriteLine("Farthest West: " + farthestWest.Name);
            Console.WriteLine("Farthest East: " + farthestEast.Name);

            double maxDistance = 0;
            double minDistance = double.MaxValue;

            for (int i = 0; i < persons.Count; i++)
            {
                for (int j = i + 1; j < persons.Count; j++)
                {
                    double distance = CalculateDistance(persons[i].Latitude, persons[i].Longitude,
                                                       persons[j].Latitude, persons[j].Longitude);
                    maxDistance = Math.Max(maxDistance, distance);
                    minDistance = Math.Min(minDistance, distance);
                }
            }

            Console.WriteLine("Max Distance: " + maxDistance);
            Console.WriteLine("Min Distance: " + minDistance);

            var personsWithCommonWords = persons
                .Select(p => new
                {
                    Person = p,
                    CommonWordsCount = CountCommonWords(persons, p)
                })
                .OrderByDescending(p => p.CommonWordsCount)
                .Take(2)
                .ToList();

            Console.WriteLine("Persons with most common words in 'about':");
            foreach (var item in personsWithCommonWords)
            {
                Console.WriteLine("Person: " + item.Person.Name);
                Console.WriteLine("Common Words Count: " + item.CommonWordsCount);
                Console.WriteLine();
            }

            var personsWithSameFriends = new List<Person>();

            foreach (var person in persons)
            {
                var commonFriends = persons
                    .Where(p => p.Id != person.Id)
                    .SelectMany(p => p.Friends)
                    .Where(f => person.Friends.Any(pf => pf.Name == f.Name))
                    .ToList();

                if (commonFriends.Any())
                {
                    personsWithSameFriends.Add(person);
                    personsWithSameFriends.AddRange(persons.Where(p => p.Friends.Intersect(commonFriends).Any()));
                }
            }

            Console.WriteLine("Persons with the same friends:");
            foreach (var person in personsWithSameFriends)
            {
                Console.WriteLine("Person: " + person.Name);
                Console.WriteLine("Friends:");
                foreach (var friend in person.Friends)
                {
                    Console.WriteLine("  " + friend.Name);
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        static List<Person> DeserializePersonsFromJsonFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Person>>(json);
        }

        static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371;
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        static int CountCommonWords(List<Person> persons, Person person)
        {
            string[] aboutWords = person.About.Split(new[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            int commonWordsCount = 0;

            foreach (var otherPerson in persons)
            {
                if (otherPerson.Id != person.Id)
                {
                    string[] otherAboutWords = otherPerson.About.Split(new[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                    commonWordsCount += aboutWords.Intersect(otherAboutWords).Count();
                }
            }

            return commonWordsCount;
        }
    }
}
