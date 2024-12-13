using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public int index_;
        public string mark;
        public string title_;
        public string place;
        public string info;
        public int volume_;
        public int total;
        public int now;
        public byte[] cover_;
        public string date;

        public Book(int index_, string mark, string title_, string place, string info, int volume_, int total, int now, byte[] cover_, string date)
        {
            this.index_ = index_;
            this.mark = mark;
            this.title_ = title_;
            this.place = place;
            this.info = info;
            this.volume_ = volume_;
            this.total = total;
            this.now = now;
            this.cover_ = cover_;
            this.date = date;
        }
    }
}
