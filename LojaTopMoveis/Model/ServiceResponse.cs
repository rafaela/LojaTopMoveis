namespace LojaTopMoveis.Model
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Sucess { get; set; } = true;
        public string Token { get; set; } = string.Empty;
        public int Total { get; set; } = 0;
    }
}
