using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    internal class BookDebtorsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            DateTime nowDate = DateTime.Now;
            Bookings bookings = new Bookings();
            bookings.Add();
            Readers readers = new Readers();
            readers.Add();
            string d = "Должники:\n";
            string message = "";
            foreach (Booking booking in bookings.bookings)
            {
                // Ищем пользователя по идентификатору
                Reader reader = readers.readers.FirstOrDefault(r => r.id_ == booking.reader && !booking.returned);
                if (reader != null)
                {
                    // Формируем сообщение
                    message = $"Фамилия: {reader.surname_}, Имя: {reader.name_}, Отчество: {reader.patronymic_}, Номер телефона: {reader.phone_}, Дата сдачи книги: {booking.return_}";
                    
                }

            }
            MessageBox.Show(d + message);
        }
    }
}
