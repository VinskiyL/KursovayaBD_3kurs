using System;
using System.Windows.Forms;

namespace Library
{
    public partial class Bookings_form : Form
    {
        public Bookings_form()
        {
            InitializeComponent();
        }

        Bookings bookings = new Bookings();
        Readers readers = new Readers();
        Books books = new Books();
        int id_ = 0;
        int select = 0;

        public Bookings_form(int id)
        {
            this.id_ = id;
            InitializeComponent();
        }

        private void Reader_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана! Пожалуйста нажмите на нужную строку");
                }
                Booking booking = bookings.Find(select);
                Readers_form form = new Readers_form(booking.reader);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Update_Click(sender, e)));
                return;
            }
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана! Пожалуйста нажмите на нужную строку");
                }
                Booking_form form = new Booking_form(select, bookings);
                form.DataUpdated += DataUpdated;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataUpdated()
        {
            dataGridView1.Rows.Clear();
            if (id_ == 0)
            {
                for (int i = 0; i < bookings.bookings.Count; i++)
                {
                    var book = books.Find(bookings.bookings[i].index_);
                    var title_ = book.title_;
                    var reader = readers.Find(bookings.bookings[i].reader);
                    var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                    var Value1 = bookings.bookings[i].issued ? bookings.bookings[i].issue.ToString() : "";
                    var Value2 = bookings.bookings[i].returned ? bookings.bookings[i].return_.ToString() : "";
                    dataGridView1.Rows.Add(bookings.bookings[i].id_, fio_, title_, bookings.bookings[i].index_, bookings.bookings[i].reader,
                        bookings.bookings[i].quantity_, Value1, Value2);
                }
            }
            else
            {
                for (int i = 0; i < bookings.bookings.Count; i++)
                {
                    if (bookings.FindReader(id_, bookings.bookings[i].id_))
                    {
                        var book = books.Find(bookings.bookings[i].index_);
                        var title_ = book.title_;
                        var reader = readers.Find(bookings.bookings[i].reader);
                        var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                        var Value1 = bookings.bookings[i].issued ? bookings.bookings[i].issue.ToString() : "";
                        var Value2 = bookings.bookings[i].returned ? bookings.bookings[i].return_.ToString() : "";
                        dataGridView1.Rows.Add(bookings.bookings[i].id_, fio_, title_, bookings.bookings[i].index_, bookings.bookings[i].reader,
                            bookings.bookings[i].quantity_, Value1, Value2);
                    }
                }
            }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // Действия для создания новой книги
            Booking_form form = new Booking_form(0, bookings);
            form.DataUpdated += DataUpdated;
            form.Show();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана! Пожалуйста нажмите на нужную строку");
                }

                // Запрос подтверждения у пользователя
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
                                                        "Подтверждение удаления",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Выполняем удаление
                    bookings.DelDb(select);
                    select = 0;
                    DataUpdated();
                }
                else
                {
                    // Пользователь отменил действие
                    MessageBox.Show("Удаление отменено");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bookings_form_Load(object sender, EventArgs e)
        {
            try
            {
                bookings.Add();
                readers.Add();
                books.Add();
                Reader.Enabled = true;
                Update.Enabled = true;
                Insert.Enabled = true;
                Delete.Enabled = true;
                textBox2.Text = "Список бронирования";
                if (bookings == null)
                {
                    throw new Exception("Нет информации о бронировании");
                }
                if (id_ == 0)
                {
                    for (int i = 0; i < bookings.bookings.Count; i++)
                    {
                        var book = books.Find(bookings.bookings[i].index_);
                        var title_ = book.title_;
                        var reader = readers.Find(bookings.bookings[i].reader);
                        var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                        var Value1 = bookings.bookings[i].issued ? bookings.bookings[i].issue.ToString() : "";
                        var Value2 = bookings.bookings[i].returned ? bookings.bookings[i].return_.ToString() : "";
                        dataGridView1.Rows.Add(bookings.bookings[i].id_, fio_, title_, bookings.bookings[i].index_, bookings.bookings[i].reader,
                            bookings.bookings[i].quantity_, Value1, Value2);
                    }
                }
                else
                {
                    textBox2.Text = "Бронирования читателя";
                    textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                    Reader.Enabled = false;
                    for (int i = 0; i < bookings.bookings.Count; i++)
                    {
                        if (bookings.FindReader(id_, bookings.bookings[i].id_))
                        {
                            var book = books.Find(bookings.bookings[i].index_);
                            var title_ = book.title_;
                            var reader = readers.Find(id_);
                            var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                            var Value1 = bookings.bookings[i].issued ? bookings.bookings[i].issue.ToString() : "";
                            var Value2 = bookings.bookings[i].returned ? bookings.bookings[i].return_.ToString() : "";
                            dataGridView1.Rows.Add(bookings.bookings[i].id_, fio_, title_, bookings.bookings[i].index_, bookings.bookings[i].reader,
                                bookings.bookings[i].quantity_, Value1, Value2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                select = (int)selectedRow.Cells["id"].Value;
            }
        }
    }
}
