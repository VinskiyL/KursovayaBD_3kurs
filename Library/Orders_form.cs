using System;
using System.Windows.Forms;

namespace Library
{
    public partial class Orders_form : Form
    {
        public Orders_form()
        {
            InitializeComponent();
        }

        Orders orders = new Orders();
        Readers readers = new Readers();
        int id_ = 0;
        int select = 0;

        public Orders_form(int id)
        {
            this.id_ = id;
            InitializeComponent();
        }

        private void Orders_form_Load(object sender, EventArgs e)
        {
            try
            {
                orders.Add();
                readers.Add();
                Reader.Enabled = true;
                Update.Enabled = true;
                Insert.Enabled = true;
                Delete.Enabled = true;
                textBox2.Text = "Список заказов";
                if (orders == null)
                {
                    throw new Exception("Нет информации о заказах");
                }
                if (id_ == 0)
                {
                    for (int i = 0; i < orders.orders.Count; i++)
                    {
                        var reader = readers.Find(orders.orders[i].reader);
                        var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                        dataGridView1.Rows.Add(orders.orders[i].id_, fio_, orders.orders[i].title_, orders.orders[i].surname, orders.orders[i].name,
                                orders.orders[i].patronymic, orders.orders[i].quantity_, orders.orders[i].reader, orders.orders[i].date);
                    }
                }
                else
                {
                    textBox2.Text = "Заказы читателя";
                    textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                    Reader.Enabled = false;
                    for (int i = 0; i < orders.orders.Count; i++)
                    {
                        if (orders.FindReader(id_))
                        {
                            var reader = readers.Find(orders.orders[i].reader);
                            var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                            dataGridView1.Rows.Add(orders.orders[i].id_, fio_, orders.orders[i].title_, orders.orders[i].surname, orders.orders[i].name,
                                    orders.orders[i].patronymic, orders.orders[i].quantity_, orders.orders[i].reader, orders.orders[i].date);
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

        private void Insert_Click(object sender, EventArgs e)
        {
            // Действия для создания новой книги
            Order_form form = new Order_form(0, orders);
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
                    orders.DelDb(select);
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

        private void DataUpdated()
        {
            dataGridView1.Rows.Clear();
            if (id_ == 0)
            {
                for (int i = 0; i < orders.orders.Count; i++)
                {
                    var reader = readers.Find(orders.orders[i].reader);
                    var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                    dataGridView1.Rows.Add(orders.orders[i].id_, fio_, orders.orders[i].title_, orders.orders[i].surname, orders.orders[i].name,
                            orders.orders[i].patronymic, orders.orders[i].quantity_, orders.orders[i].reader, orders.orders[i].date);
                }
            }
            else
            {
                for (int i = 0; i < orders.orders.Count; i++)
                {
                    if (orders.FindReader(id_))
                    {
                        var reader = readers.Find(orders.orders[i].reader);
                        var fio_ = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                        dataGridView1.Rows.Add(orders.orders[i].id_, fio_, orders.orders[i].title_, orders.orders[i].surname, orders.orders[i].name,
                                orders.orders[i].patronymic, orders.orders[i].quantity_, orders.orders[i].reader, orders.orders[i].date);
                    }
                }
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
                Order_form form = new Order_form(select, orders);
                form.DataUpdated += DataUpdated;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reader_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Order order = orders.Find(select);
                Readers_form form = new Readers_form(order.reader);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
