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
            //create a type of var called allSounds for get the souds list.
            var allSounds = getSounds();
            //stop play the sound 
            sounds.Clear();
            allSounds.ForEach(p => sounds.Add(p));
        }

        public static void GetSoundsByCAtegory(ObservableCollection<Sound> sounds, SoundCategory soundCategory)
        {
            //create a type of var called allSounds for get the souds list.
            var allSounds = getSounds();
            //create a type of var called filteredSounds for get all souds list.
            //And this line means find where soundfile anme equal to 
            var filteredSounds = allSounds.Where(p => p.Category == soundCategory).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        //This method called GetSuundsByName that means 
        public static void GetSuundsByName(ObservableCollection<Sound> sounds, string name)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds.Where(p => p.Name == name).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }


        //This method called getSounds means get all sounds from the file.
        //And in this method , there is a type of var called sounds that is a list for get all soundsfile.
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
            sounds.Add(new Sound("Morse", SoundCategory.Cartoons));
            sounds.Add(new Sound("UFO", SoundCategory.Cartoons));
            sounds.Add(new Sound("Motorcycle", SoundCategory.Cartoons));

            sounds.Add(new Sound("Clock", SoundCategory.Life));
            sounds.Add(new Sound("Baby", SoundCategory.Life));
            sounds.Add(new Sound("Car", SoundCategory.Life));
            sounds.Add(new Sound("Laser", SoundCategory.Life));
            sounds.Add(new Sound("Ocean", SoundCategory.Life));
            sounds.Add(new Sound("Telephone", SoundCategory.Life));
            sounds.Add(new Sound("Windy", SoundCategory.Life));

            sounds.Add(new Sound("Siren", SoundCategory.Warnings));
            sounds.Add(new Sound("Ship", SoundCategory.Warnings));
       
            return sounds;
        }
    }
}
