using System.Text.Json.Serialization;

namespace dotnetRpgApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Warrior = 1,
        Mage = 2,
        Archer = 3,
        Rogue = 4,
        Cleric = 5
    }
}