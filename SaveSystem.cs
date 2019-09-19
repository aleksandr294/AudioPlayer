using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
namespace AudioP
{
    [System.Serializable]
    public class PlayList
    {
        public List<string> music = new List<string>();
        public List<string> songs = new List<string>();

    }

    public class SaveSystem
    {

        public void SaveParams(List<string> pl, List<string> songs)
        {
            PlayList playlist = new PlayList();
            XmlSerializer xml = new XmlSerializer(typeof(PlayList));

            playlist.music.AddRange(pl.ToArray());
            playlist.songs.AddRange(songs.ToArray());
            using (var stream = new FileStream("Music.xml", FileMode.Create, FileAccess.Write))
            {
                xml.Serialize(stream, playlist);
            }
        }
        public List<string> LoadParams(List<string> pl, List<string> songs)
        {


            var xml = new XmlSerializer(typeof(PlayList));
            var playlist = new PlayList();


            using (var stream = new FileStream("Music.xml", FileMode.Open, FileAccess.Read))
            {
                
                playlist = xml.Deserialize(stream) as PlayList;
            }

           
            pl.AddRange(playlist.music.ToArray());

            songs.AddRange(playlist.songs.ToArray());
            return pl;
            return songs;
        }

    }
}
