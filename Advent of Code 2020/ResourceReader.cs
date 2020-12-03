using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Advent_of_Code_2020.Day2;

namespace Advent_of_Code_2020
{
    public class ResourceReader<T>
    {
        private string resourceName;

        public ResourceReader(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public IEnumerable<T> LineReader(Func<string, T> lineParser)
        {
            using var stream = Assembly
                                   .GetExecutingAssembly()
                                   .GetManifestResourceStream(resourceName) 
                               ?? throw new Exception($"{resourceName} not read, forgot embedded resource?");
            using var reader = new StreamReader(stream);
            
            var values = new List<T>();
            
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                values.Add(lineParser.Invoke(line));
            }

            return values;
        }

        public T ReadFully(Func<string, T> parser)
        {
            using var stream = Assembly
                                   .GetExecutingAssembly()
                                   .GetManifestResourceStream(resourceName) 
                               ?? throw new Exception($"{resourceName} not read");
            using var reader = new StreamReader(stream);

            return parser.Invoke(reader.ReadToEnd());
        }
    }
}