namespace AuthMicroservice.Entity.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHashed { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
