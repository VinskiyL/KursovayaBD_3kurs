using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Readers
    {
        public List<Reader> readers = new List<Reader>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(Reader r)
        {
            readers.Add(r);
        }

        public void Del(int id)
        {
            Reader r = Find(id);
            if (r != null) { readers.Remove(r); }
            else { throw new Exception("Читатель не найден! Для решения этой проблемы свяжитесь со специалистом"); }
        }

        public Reader Find(int id)
        {
            Reader reader = readers.FirstOrDefault(r => r.id_ == id);
            return reader;
        }

        public int FindMaxId()
        {
            int max = 0;
            foreach (Reader r in readers)
            {
                if (r.id_ > max)
                {
                    max = r.id_;
                }
            }
            return max;
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Readers_catalog\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reader r = new Reader(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.IsDBNull(3) ? null : reader.GetString(3), 
                            reader.GetDateTime(4), reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6), reader.IsDBNull(7) ? null : reader.GetString(7), 
                            reader.GetString(8), reader.GetString(9), reader.GetInt32(10), reader.IsDBNull(11) ? null : reader.GetString(11), reader.IsDBNull(12) ? 0 : reader.GetInt32(12), 
                            reader.GetString(19), reader.GetInt32(13),reader.GetInt32(14), reader.GetString(15), 
                            reader.GetDateTime(16),reader.GetDateTime(17), reader.IsDBNull(18) ? default : reader.GetDateTime(18));
                        Add(r);
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
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Readers_catalog\" WHERE id = " + index, connection))
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

            Del(index);
        }

        public void UpdDb(string set, int index)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Readers_catalog\" SET " + set + " WHERE id = " + index, connection))
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

        public void AddDb(Reader reader)
        {
            string re = reader.re.ToString("yyyy-MM-dd");
            string consists = reader.consists.ToString("yyyy-MM-dd");
            string date = reader.date.ToString("yyyy-MM-dd");
            string birthday = reader.birthday_.ToString("yyyy-MM-dd");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Readers_catalog\" VALUES( " + reader.id_ + ", '" +
                    reader.surname_ + "', '" + reader.name_ + "', '" + reader.patronymic_ +  "', '" + birthday + "', '" +
                    reader.education_ + "', '" + reader.profession_ + "', '" + reader.institut + "', '" + reader.city_ + "', '" + reader.street_ + "', " +
                    reader.house_ + ", '" + reader.building + "', " + reader.flat_ + ", " + reader.series + ", " + reader.number + 
                    ", '" + reader.issued + "', '" + date + "', '" + consists + "', '" + re + "', '" + reader.phone_ + "' )", connection))
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

            readers.Add(reader);
        }
    }
}
