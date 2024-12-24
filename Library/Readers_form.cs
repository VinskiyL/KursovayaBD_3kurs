using System;
using System.Windows.Forms;

namespace Library
{
    public partial class Readers_form : Form
    {
        Readers readers = new Readers();
        int select = 0;
        int id_ = 0;

        public Readers_form()
        {
            InitializeComponent();
        }

        public Readers_form(int id)
        {
            this.id_ = id;
            InitializeComponent();
        }

        private void Readers_form_Load(object sender, EventArgs e)
        {
            try
            {
                readers.Add();
                Booking.Enabled = true;
                Order.Enabled = true;
                Update.Enabled = true;
                Insert.Enabled = true;
                Delete.Enabled = true;
                textBox2.Text = "Список читателей";
                if (readers == null)
                {
                    throw new Exception("Нет информации о читателях");
                }
                if (id_ == 0)
                {
                    for (int i = 0; i < readers.readers.Count; i++)
                    {
                        var reValue = readers.readers[i].re == default(DateTime) ? (DateTime?)null : readers.readers[i].re;
                        dataGridView1.Rows.Add(readers.readers[i].id_, readers.readers[i].surname_, readers.readers[i].name_,
                            readers.readers[i].patronymic_, readers.readers[i].birthday_, readers.readers[i].education_,
                            readers.readers[i].profession_, readers.readers[i].institut, readers.readers[i].city_,
                            readers.readers[i].street_, readers.readers[i].house_, readers.readers[i].building, readers.readers[i].flat_,
                            readers.readers[i].phone_, readers.readers[i].series, readers.readers[i].number, readers.readers[i].issued,
                            readers.readers[i].date, readers.readers[i].consists, reValue);
                    }
                }
                else
                {
                    textBox2.Text = "Читатель";
                    textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                    Booking.Enabled = false;
                    Order.Enabled = false;
                    Update.Enabled = false;
                    Insert.Enabled = false;
                    Delete.Enabled = false;
                    Reader reader = readers.Find(id_);
                    var reValue = reader.re == default(DateTime) ? (DateTime?)null : reader.re;
                    dataGridView1.Rows.Add(id_, reader.surname_, reader.name_, reader.patronymic_, reader.birthday_,
                        reader.education_, reader.profession_, reader.institut, reader.city_, reader.street_, reader.house_,
                        reader.building, reader.flat_, reader.phone_, reader.series, reader.number, reader.issued, reader.date,
                        reader.consists, reValue);
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

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Reader_form form = new Reader_form(select, readers);
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
                for (int i = 0; i < readers.readers.Count; i++)
                {
                    var reValue = readers.readers[i].re == default(DateTime) ? (DateTime?)null : readers.readers[i].re;
                    dataGridView1.Rows.Add(readers.readers[i].id_, readers.readers[i].surname_, readers.readers[i].name_,
                        readers.readers[i].patronymic_, readers.readers[i].birthday_, readers.readers[i].education_,
                        readers.readers[i].profession_, readers.readers[i].institut, readers.readers[i].city_,
                        readers.readers[i].street_, readers.readers[i].house_, readers.readers[i].building, readers.readers[i].flat_,
                        readers.readers[i].phone_, readers.readers[i].series, readers.readers[i].number, readers.readers[i].issued,
                        readers.readers[i].date, readers.readers[i].consists, reValue);
                }
            }
            else
            {
                Reader reader = readers.Find(id_);
                var reValue = reader.re == default(DateTime) ? (DateTime?)null : reader.re;
                dataGridView1.Rows.Add(id_, reader.surname_, reader.name_, reader.patronymic_, reader.birthday_,
                    reader.education_, reader.profession_, reader.institut, reader.city_, reader.street_, reader.house_,
                    reader.building, reader.flat_, reader.phone_, reader.series, reader.number, reader.issued, reader.date,
                    reader.consists, reValue);
            }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // Действия для создания новой книги
            Reader_form form = new Reader_form(0, readers);
            form.DataUpdated += DataUpdated;
            form.Show();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }

                // Запрос подтверждения у пользователя
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
                                                        "Подтверждение удаления",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Выполняем удаление
                    readers.DelDb(select);
                    select = 0;
                    DataUpdated();
                }
                else
                {
                    // Пользователь отменил действие
                    MessageBox.Show("Удаление отменено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Booking_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Bookings_form form = new Bookings_form(select);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Order_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Orders_form form = new Orders_form(select);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
