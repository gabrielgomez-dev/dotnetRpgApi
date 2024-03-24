namespace dotnetRpgApi.Dtos.Weapon
{
    public class WeaponAddDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}