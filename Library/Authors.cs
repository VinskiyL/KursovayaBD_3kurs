using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Authors
    {
        public List<Author> authors = new List<Author>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(Author a)
        {
            authors.Add(a);
        }

        public void Del(int id)
        {
            Author author = Find(id);
            if (author != null) { authors.Remove(author); }
            else { throw new Exception("Автор не найден!"); }
        }

        public Author Find(int id)
        {
            Author author = authors.FirstOrDefault(a => a.id == id);
            return author;
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Authors_catalog\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Author author = new Author(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        Add(author);
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
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Authors_catalog\" WHERE id = " + index, connection))
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
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Authors_catalog\" SET " + set + " WHERE id = " + index, connection))
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

        public void AddDb(Author author)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Authors_catalog\" VALUES( " + author.id + ", '" +
                    author.surname + "', '" + author.name + "', '" + author.patronymic + "' )", connection))
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

            authors.Add(author);
        }
    }
}
