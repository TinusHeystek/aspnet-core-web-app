using System;

namespace Example.App.Common
{
    public static class PetTypes
    {
        private static readonly string[] Pets = 
        {
            "Dog",
            "Cat",
            "fish",
            "Bird",
            "Rabbit",
            "Guinea-pig",
            "Mouse",
            "Hamster",
            "Ferret",
            "Lizard",
            "Snake",
            "Turtle",
            "Horse"
        };

        public static string GetRandom()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return Pets[random.Next(0, Pets.Length - 1)];
        }
    }
}


