namespace Library
{
    public class Order
    {
        public int id_;
        public string title_;
        public string surname;
        public string name;
        public string patronymic;
        public int quantity_;
        public int reader;
        public string date;

        public Order(int id_, string title_, string surname, string name, string patronymic, int quantity_, int reader, string date)
        {
            this.id_ = id_;
            this.title_ = title_;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.quantity_ = quantity_;
            this.reader = reader;
            this.date = date;
        }
    }
}
