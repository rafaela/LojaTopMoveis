namespace LojaTopMoveis.Model
{
    public class ServiceParameter<T>
    {
        public T? Data { get; set; }
        public int Take { get; set; } = 0;
        public int Skip { get; set; } = 0;
    }
}
