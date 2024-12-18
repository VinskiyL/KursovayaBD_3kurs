namespace Library
{
    public class Author
    {
        public int id;
        public string name;
        public string surname;
        public string patronymic;

        public Author(int id, string surname, string name, string patronymic)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
        }
    }
}
