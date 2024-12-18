using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace Library
{
    public class Books
    {
        public List<Book> books = new List<Book>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(Book b)
        {
            books.Add(b);
        }

        public void Del(int index)
        {
            Book book = Find(index);
            if (book != null) { books.Remove(book); }
            else { throw new Exception("Книга не найдена!"); }
        }

        public Book Find(int index)
        {
            Book book = books.FirstOrDefault(b => b.index_ == index);
            return book;
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Books_catalog\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7),
                            reader.IsDBNull(8) ? null : (byte[])reader[8], reader.GetString(9));
                        Add(book);
                    }
                }

                connection.Close();
            }
        }

        public void DelDb(int index)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Books_catalog\" WHERE index = " + index, connection))
                {
                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Запись не удалена!");
                    }
                }

                connection.Close();
            }

            Del(index);
        }

        public void UpdDb(string set, int index)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Books_catalog\" SET " + set + " WHERE index = " + index, connection))
                {
                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Запись не изменена!");
                    }
                }

                connection.Close();
            }
        }

        public void AddDb(Book book)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Books_catalog\" VALUES( " + book.index_ + ", '" +
                    book.mark + "', '" + book.title_ + "', '" + book.place + "', '" + book.info + "', " + book.volume_ + ", " +
                    book.total + ", " + book.now + ", '" + book.cover_ + "', '" + book.date + "' )", connection))
                {
                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Запись не добавлена!");
                    }
                }

                connection.Close();
            }

            books.Add(book);
        }
    }
}
