using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Order_form : Form
    {
        public Order_form()
        {
            InitializeComponent();
        }

        Orders orders = new Orders();
        int select = 0;
        public event Action DataUpdated;

        public Order_form(int select, Orders orders)
        {
            this.select = select;
            this.orders = orders;
            InitializeComponent();
        }

        private void Order_form_Load(object sender, EventArgs e)
        {
            if (select != 0)
            {
                Order order = orders.Find(select);
                title.Text = order.title_;
                author_surname.Text = order.surname;
                author_name.Text = order.name;
                author_patronymic.Text = order.patronymic;
                quantity.Text = order.quantity_.ToString();
                reader_id.Text = order.reader.ToString();
                date_publication.Text = order.date;
            }
            quantity.MaxLength = 1;
            date_publication.MaxLength = 4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Readers_form form = new Readers_form();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order;
            Readers readers = new Readers();
            readers.Add();
            try
            {
                if (select != 0)
                {
                    if (check_TextBox())
                    {
                        return;
                    }
                    order = orders.Find(select);
                    List<string> set = new List<string>();
                    if (title.Text != order.title_.ToString())
                    {
                        set.Add("title = '" + title.Text + "'");
                        order.title_ = title.Text;
                    }
                    if (author_surname.Text != order.surname.ToString())
                    {
                        set.Add("author_surname = '" + author_surname.Text + "'");
                        order.surname = author_surname.Text;
                    }
                    if (author_name.Text != order.name.ToString())
                    {
                        set.Add("author_name = '" + author_name.Text + "'");
                        order.name = author_name.Text;
                    }
                    if (author_patronymic.Text != order.patronymic.ToString())
                    {
                        set.Add("author_patronymic = '" + author_patronymic.Text + "'");
                        order.patronymic = author_patronymic.Text;
                    }
                    if (quantity.Text != order.quantity_.ToString())
                    {
                        set.Add("quantity = " + quantity.Text);
                        order.quantity_ = int.Parse(quantity.Text);

                    }
                    if (reader_id.Text != order.reader.ToString())
                    {
                        if (readers.Find(int.Parse(reader_id.Text)) == null)
                        {
                            throw new Exception("Читателя с таким инденитификатором не существует!");
                        }
                        set.Add("reader_id = " + reader_id.Text);
                        order.reader = int.Parse(reader_id.Text);
                    }
                    if (date_publication.Text != order.date.ToString())
                    {
                        set.Add("date_publication = '" + date_publication.Text + "'");
                        order.date = date_publication.Text;
                    }
                    string result = string.Join(", ", set);
                    orders.UpdDb(result, select);
                }
                else
                {
                    int id_ = orders.FindMaxId() + 1;
                    if (check_TextBox())
                    {
                        return;
                    }
                    int quantity_ = int.Parse(quantity.Text);
                    int reader_ = int.Parse(reader_id.Text);
                    if (readers.Find(reader_) == null)
                    {
                        throw new Exception("Читателя с таким инденитификатором не существует!");
                    }
                    string surname = author_surname.Text;
                    string name = author_name.Text;
                    string patronymic = author_patronymic.Text;
                    string title_ = title.Text;
                    string date = date_publication.Text;
                    order = new Order(id_, title_, surname, name, patronymic, quantity_, reader_, date);
                    orders.AddDb(order);
                }
                DataUpdated?.Invoke();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool check_TextBox()
        {
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                MessageBox.Show("Поле 'Название книги' должно быть заполнено!");
                title.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(author_surname.Text))
            {
                MessageBox.Show("Поле 'Фамилия автора' должно быть заполнено!");
                author_surname.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(quantity.Text))
            {
                MessageBox.Show("Поле 'Количество' должно быть заполнено!");
                quantity.Focus();
                return true;
            }
            else if (int.Parse(quantity.Text) <= 0 || int.Parse(quantity.Text) >= 5)
            {
                MessageBox.Show("Количество должно быть больше 0 и меньше 5!");
                quantity.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(reader_id.Text))
            {
                MessageBox.Show("Поле 'Идентификатор читателя' должно быть заполнено!");
                reader_id.Focus();
                return true;
            }
            return false;
        }

        private void author_surname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void author_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void author_patronymic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void reader_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void date_publication_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
