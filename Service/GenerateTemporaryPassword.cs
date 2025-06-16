namespace IA_AbansiBabayiSystemBlazor.Service
{
    public class GenerateTemporaryPassword
    {
        public static string GenerateTempPassword(int length = 10)
        {
            const string uppercase = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string lowercase = "abcdefghijkmnopqrstuvwxyz";
            const string digits = "23456789";
            const string symbols = "!@#$%^&*";

            var random = new Random();
            var chars = new List<char>
        {
            uppercase[random.Next(uppercase.Length)],
            lowercase[random.Next(lowercase.Length)],
            digits[random.Next(digits.Length)],
            symbols[random.Next(symbols.Length)]
        };

            string allChars = uppercase + lowercase + digits + symbols;
            while (chars.Count < length)
            {
                chars.Add(allChars[random.Next(allChars.Length)]);
            }

            return new string(chars.OrderBy(_ => random.Next()).ToArray());
        }
    }
}
