using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public delegate string TrackFormatter(MusicTrack track);

    public delegate bool TrackFilter(MusicTrack track);

    public delegate void VoidNotification(string message);
}
