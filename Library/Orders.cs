using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Orders
    {
        public List<Order> orders = new List<Order>();
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=newDB";

        public void Add(Order o)
        {
            orders.Add(o);
        }

        public bool FindReader(int reader)
        {
            return orders.Any(item => item.reader == reader);
        }

        public int FindMaxId()
        {
            int max = 0;
            foreach (Order o in orders)
            {
                if (o.id_ > max)
                {
                    max = o.id_;
                }
            }
            return max;
        }

        public void Del(int id)
        {
            Order o = Find(id);
            if (o != null) { orders.Remove(o); }
            else { throw new Exception("Заказ не найден!"); }
        }

        public Order Find(int id)
        {
            Order order = orders.FirstOrDefault(o => o.id_ == id);
            return order;
        }

        public void Add()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kursovaya.\"Order_catalog\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order o = new Order(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.IsDBNull(3) ? null : reader.GetString(3), 
                            reader.IsDBNull(4) ? null : reader.GetString(4), 
                            reader.GetInt32(5), reader.GetInt32(6),
                            reader.IsDBNull(7) ? null : reader.GetString(7));
                        Add(o);
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
                using (var command = new NpgsqlCommand("DELETE FROM kursovaya.\"Order_catalog\" WHERE id = " + index, connection))
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
                using (var command = new NpgsqlCommand("UPDATE kursovaya.\"Order_catalog\" SET " + set + " WHERE id = " + index, connection))
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

        public void AddDb(Order order)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO kursovaya.\"Order_catalog\" VALUES( " + order.id_ + ", '" + 
                    order.title_ + "', '" + order.surname + "', '" + order.name + "', '" + order.patronymic + "', " + 
                    order.quantity_ + ", " + order.reader + ", '"  + order.date + "' )", connection))
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

            orders.Add(order);
        }
    }
}
