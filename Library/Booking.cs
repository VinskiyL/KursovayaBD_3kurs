using System;

namespace Library
{
    public class Booking
    {
        public int index_;
        public int reader;
        public int quantity_;
        public DateTime issue;
        public DateTime return_;
        public int id_;
        public bool issued;
        public bool returned;

        public Booking(int index_, int reader, int quantity_, DateTime issue, DateTime return_, int id_, bool issued, bool returned)
        {
            this.index_ = index_;
            this.reader = reader;
            this.quantity_ = quantity_;
            this.issue = issue;
            this.return_ = return_;
            this.id_ = id_;
            this.issued = issued;
            this.returned = returned;
        }
    }
}
