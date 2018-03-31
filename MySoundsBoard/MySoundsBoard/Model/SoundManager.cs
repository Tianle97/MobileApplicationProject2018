using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoundsBoard.Model
{
    public class SoundManager
    {
        //Let that collectionso bjection model great and
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allSounds = getSounds();
            sounds.Clear();
            allSounds.ForEach(p => sounds.Add(p));

        }

        public static void GetSoundsByCAtegory(ObservableCollection<Sound> sounds, SoundCategory soundCategory)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds.Where(p => p.Category == soundCategory).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }


        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();

            sounds.Add(new Sound("Cow", SoundCategory.Animals));
            sounds.Add(new Sound("Chicken", SoundCategory.Animals));
            sounds.Add(new Sound("Dog", SoundCategory.Animals));
            sounds.Add(new Sound("Dolphin", SoundCategory.Animals));
            sounds.Add(new Sound("Horse", SoundCategory.Animals));
            sounds.Add(new Sound("Sheep", SoundCategory.Animals));
            sounds.Add(new Sound("Wolf", SoundCategory.Animals));

            sounds.Add(new Sound("Gun", SoundCategory.Cartoons));
            sounds.Add(new Sound("Spring", SoundCategory.Cartoons));

            sounds.Add(new Sound("Clock", SoundCategory.Life));

            sounds.Add(new Sound("Siren", SoundCategory.Warnings));
            sounds.Add(new Sound("Ship", SoundCategory.Warnings));
       
            return sounds;
        }
    }
}
