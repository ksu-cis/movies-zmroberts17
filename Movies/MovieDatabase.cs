﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> Search(string term)
        {
            List<Movie> results = new List<Movie>();

            foreach (Movie movie in movies)
            {
                if (movie.Title.Contains(term, StringComparison.OrdinalIgnoreCase) || 
                    ((movie.Director != null) && (movie.Director.Contains(term, StringComparison.OrdinalIgnoreCase))))
                {
                    results.Add(movie);
                }


            }

            return results;
        }

        public List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if (mpaa.Contains(movie.MPAA_Rating))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public List<Movie> FilterByMinIMDB(List<Movie> movies, float min)
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && movie.IMDB_Rating >= min)
                {
                    results.Add(movie);
                }
            }

            return results;
        }
    }
}
