namespace LojaTopMoveis.Model
{
    public class Configuration
    {

        public static string alfanumericoAleatorio(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 @\"~!@#$%^&*():;[]{}<>,.?/\\|\"";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public static string PrivateKey { get; set; } = alfanumericoAleatorio(30);
    }
}
