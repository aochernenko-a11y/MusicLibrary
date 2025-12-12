using MusicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicLibrary
{
    public class Artist : IPrintable
    {
        public int Id { get; }
        public string Name { get; }
        public string Country { get; }

        public Artist(int id, string name, string country)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Country = country ?? string.Empty;
        }

        public string GetDisplayText()
        {
            return $"Artist: {Name} ({Country}), Id = {Id}";
        }
    }
}