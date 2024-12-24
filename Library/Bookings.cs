using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Bookings
    {
        public List<Booking> bookings = new List<Booking>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(Booking b)
        {
            bookings.Add(b);
        }

        public int FindMaxId()
        {
            int max = 0;
            foreach (Booking b in bookings)
            {
                if (b.id_ > max)
                {
                    max = b.id_;
                }
            }
            return max;
        }

        public void Del(int id)
        {
            Booking b = Find(id);
            if (b != null) { bookings.Remove(b); }
            else { throw new Exception("Бронь не найдена!"); }
        }

        public Booking Find(int id)
        {
            Booking booking = bookings.FirstOrDefault(b => b.id_ == id);
            return booking;
        }

        public bool FindReader(int reader)
        {
            return bookings.Any(item => item.reader == reader);
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Booking_catalog\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking b = new Booking(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetDateTime(3),
                            reader.GetDateTime(4), reader.GetInt32(5), reader.GetBoolean(6), reader.GetBoolean(7));
                        Add(b);
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
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Booking_catalog\" WHERE id = " + index, connection))
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
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Booking_catalog\" SET " + set + " WHERE id = " + index, connection))
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

        public void AddDb(Booking booking)
        {
            string issue = booking.issue.ToString("yyyy-MM-dd");
            string return_ = booking.return_.ToString("yyyy-MM-dd");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Booking_catalog\" VALUES( " + booking.index_ + ", " 
                    + booking.reader + ", " + booking.quantity_ + ", '" + issue + "', '" + return_ + "', " + booking.id_ + ", " + 
                    booking.issued + ", " + booking.returned + " )", connection))
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

            bookings.Add(booking);
        }
    }
}
