using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Authors_books
    {
        public List<Author_book> a_b = new List<Author_book>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(Author_book a)
        {
            a_b.Add(a);
        }

        public void Del(int id)
        {
            Author_book a = Find(id);
            if (a != null) { a_b.Remove(a); }
            else { throw new Exception("Связь автора с книгой не найдена! Для решения этой проблемы свяжитесь со специалистом"); }
        }

        public void Del(int id, int index)
        {
            Author_book a = FindII(id, index);
            if (a != null) { a_b.Remove(a); }
            else { throw new Exception("Связь автора с книгой не найдена! Для решения этой проблемы свяжитесь со специалистом"); }
        }

        public bool Find(int id, int index)
        {
            return a_b.Any(item => item.author_id == id && item.book_id == index);
        }

        public Author_book Find(int id)
        {
            Author_book ab = a_b.FirstOrDefault(a => a.id == id);
            return ab;
        }

        public Author_book FindII(int id, int index)
        {
            Author_book ab = a_b.FirstOrDefault(a => a.author_id == id && a.book_id == index);
            return ab;
        }

        public int FindMaxId()
        {
            int max = 0;
            foreach(Author_book a in a_b)
            {
                if(a.id > max)
                {
                    max = a.id;
                }
            }
            return max;
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Authors_Books\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Author_book a = new Author_book(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                        Add(a);
                    }
                }

                connection.Close();
            }
        }

        public void DelDb(int id, int index)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Authors_Books\" WHERE book_id = " + index + 
                    " and author_id = " + id, connection))
                {
                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Запись не удалена! Для решения этой проблемы свяжитесь со специалистом");
                    }
                }

                connection.Close();
            }

            Del(id, index);
        }

        public void UpdDb(string set, int index)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Authors_Books\" SET " + set + " WHERE id = " + index, connection))
                {
                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Запись не изменена! Для решения этой проблемы свяжитесь со специалистом");
                    }
                }

                connection.Close();
            }
        }

        public void AddDb(Author_book a)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Authors_Books\" VALUES( " + a.id + ", " +
                    a.author_id + ", " + a.book_id + " )", connection))
                {
                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Запись не добавлена! Для решения этой проблемы свяжитесь со специалистом");
                    }
                }

                connection.Close();
            }

            a_b.Add(a);
        }
    }
}
