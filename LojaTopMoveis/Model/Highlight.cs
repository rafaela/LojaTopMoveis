namespace LojaTopMoveis.Model
{
    public class Highlight
    {
        public Guid Id { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public bool? Inactive { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangeDate { get; set; } = DateTime.Now.ToLocalTime();
        public byte[]? Imagem { get; set; }
    }
}
