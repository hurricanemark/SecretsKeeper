namespace SecretsKeeper.Models
{
    public class Secret
    {
        // shortcut: type `prop` then tab twice.
        public int Id { get; set; }
        public string Name { get; set; }
        public string eMail { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Question1 { get; set; }
        public string Answer1 { get; set; }
        public string Question2 { get; set; }
        public string Answer2 { get; set; }
        public string Question3 { get; set; }
        public string Answer3 { get; set; }
        public string PIN { get; set; }
        public string Note { get; set; }

        // shortcut: type `ctor` then tab twice.
        public Secret()
        {

        }
    }
}
