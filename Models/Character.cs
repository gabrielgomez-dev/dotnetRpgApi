using dotnetRpgApi.Enums;

namespace dotnetRpgApi.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Unknown";
        public int HitPoints { get; set; } = 1;
        public int Strength { get; set; } = 1;
        public int Defense { get; set; } = 1;
        public int Intelligence { get; set; } = 1;
        public RpgClass Class { get; set; } = RpgClass.Warrior;
        public User? user { get; set; }
    }
}