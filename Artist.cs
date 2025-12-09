using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Artist : IPrintable
    {
        private int id;
        private string name;
        private string country;

        public int Id
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Name
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Country
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Artist(int id, string name, string country)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayText()
        {
            throw new NotImplementedException();
        }
    }
}
