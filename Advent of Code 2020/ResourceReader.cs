using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
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

        public IEnumerable<T> ParagraphReader(Func<string, T> paragraphParser)
        {
            using var stream = Assembly
                                   .GetExecutingAssembly()
                                   .GetManifestResourceStream(resourceName) 
                               ?? throw new Exception($"{resourceName} not read, forgot embedded resource?");
            using var reader = new StreamReader(stream);
            
            var values = new List<T>();

            string line;
            var paragraph = new StringBuilder();
            
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim() == "")
                {
                    paragraph.Remove(paragraph.Length - 1, 1);
                    values.Add(paragraphParser.Invoke(paragraph.ToString()));
                    paragraph.Clear();
                    continue;
                }

                paragraph.Append(line);
                paragraph.Append("\n");
            }

            if (paragraph.Length <= 0) return values;
            
            paragraph.Remove(paragraph.Length - 1, 1);
            values.Add(paragraphParser.Invoke(paragraph.ToString()));

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