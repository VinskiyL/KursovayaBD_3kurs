using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Users
    {
        public List<User> users = new List<User>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(User u)
        {
            users.Add(u);
        }

        public void Del(int id)
        {
            User u = Find(id);
            if (u != null) { users.Remove(u); }
            else { throw new Exception("Читатель не найден! Для решения этой проблемы свяжитесь со специалистом"); }
        }

        public User Find(int id)
        {
            User user = users.FirstOrDefault(u => u.id_ == id);
            return user;
        }

        public int FindMaxId()
        {
            int max = 0;
            foreach (User u in users)
            {
                if (u.id_ > max)
                {
                    max = u.id_;
                }
            }
            return max;
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Users_catalog\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User u = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        Add(u);
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
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Users_catalog\" WHERE id = " + index, connection))
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
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Users_catalog\" SET " + set + " WHERE id = " + index, connection))
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

        public void AddDb(User user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Users_catalog\" VALUES( " + user.id_ + ", '" +
                    user.name + "', '" + user.password_ + "' )", connection))
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

            users.Add(user);
        }
    }
}
