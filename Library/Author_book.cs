using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Author_book
    {
        public int id;
        public int book_id;
        public int author_id;

        public Author_book(int id, int author_id, int book_id)
        {
            this.id = id;
            this.book_id = book_id;
            this.author_id = author_id;
        }
    }
}
