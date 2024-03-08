namespace LojaTopMoveis.Model
{
    public class LinkPagamento
    {
        public string Access_Token { get; set; } = "";
        public string? Token_Type { get; set; }
        public string? Expires_in { get; set; }
    }
}
