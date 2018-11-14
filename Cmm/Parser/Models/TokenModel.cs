namespace Parser.Models
{
    public class TokenModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public TokenModel(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public TokenModel(string name)
        {
            Name = name;
        }
    }
}