using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie{Title="IT",Rating= 4.0f,Year=2017},
                new Movie{Title="Spider-Man",Rating= 3.8f,Year=2017},
                new Movie{Title="Nice Guys",Rating= 4.2f,Year=2016},
                new Movie{Title="Fast8",Rating= 4.5f,Year=2016}
            };

            foreach(var movie in movies.Filter(m => m.Year == 2016))
            {
                Console.WriteLine(movie.Title);
            }
            foreach (var movie in movies.Where(m => m.Year == 2016))
            {
                Console.WriteLine(movie.Title);
            }
        }
    }
}
