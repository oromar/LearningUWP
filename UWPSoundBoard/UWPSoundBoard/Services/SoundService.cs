﻿using System.Collections.Generic;
using System.Linq;
using UWPSoundBoard.Models;

namespace UWPSoundBoard.Services
{
    public static class SoundService
    {
        public static List<Sound> GetAllSounds()
        {
            return new List<Sound>
            {
                new Sound("Cat", SoundCategory.Animals),
                new Sound("Cow", SoundCategory.Animals),

                new Sound("Gun", SoundCategory.Cartoons),
                new Sound("Spring", SoundCategory.Cartoons),

                new Sound("Clock", SoundCategory.Taunts),
                new Sound("LOL", SoundCategory.Taunts),

                new Sound("Ship", SoundCategory.Warnings),
                new Sound("Siren", SoundCategory.Warnings)
            };
        }

        public static List<Sound> GetSounds(SoundCategory category)
        {
            return GetAllSounds().Where(a => a.Category == category).ToList();
        }

        public static List<Sound> GetSounds(string name)
        {
            return GetAllSounds().Where(a => a.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
