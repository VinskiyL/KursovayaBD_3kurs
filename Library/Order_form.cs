using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        private void InitializeComboBox()
        {
            if (this.InvokeRequired)
            {
                // Если мы не в UI потоке, вызываем метод снова через Invoke
                this.Invoke(new Action(InitializeComboBox));
                return;
            }
            Readers readers = new Readers();
            readers.Add();
            List<string> fios = new List<string>();
            foreach (Reader r in readers.readers)
            {
                fios.Add(r.surname_ + " " + r.name_ + " " + r.patronymic_ ?? string.Empty);
            }
            // Устанавливаем источник данных для ComboBox
            fio.DataSource = fios;
            // Включаем автозаполнение

        }

        private void Order_form_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeComboBox();
                if (select != 0)
                {
                    Readers readers = new Readers();
                    readers.Add();
                    Order order = orders.Find(select);
                    Reader reader = readers.Find(order.reader);
                    title.Text = order.title_;
                    author_surname.Text = order.surname;
                    author_name.Text = order.name;
                    author_patronymic.Text = order.patronymic;
                    quantity.Text = order.quantity_.ToString();
                    fio.Text = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                    date_publication.Text = order.date;
                }
                quantity.MaxLength = 1;
                date_publication.MaxLength = 4;
            }
            catch(Exception ex) { throw new Exception(ex.Message + "\nДля решения этой проблемы свяжитесь со специалистом"); }
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
                MessageBox.Show(ex.Message + "\nДля решения этой проблемы свяжитесь со специалистом");
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
                    Reader reader = readers.Find(order.reader);
                    string[] fiol = fio.Text.Split(' ');
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
                    if (order.name == null && (author_name.Text != "" || author_name.Text != null))
                    {
                        set.Add("author_name = '" + author_name.Text + "'");
                        order.name = author_name.Text;
                    }
                    else if (author_name.Text != order.name.ToString())
                    {
                        set.Add("author_name = '" + author_name.Text + "'");
                        order.name = author_name.Text;
                    }
                    if(order.patronymic == null && (author_patronymic.Text != "" || author_patronymic.Text != null))
                    {
                        set.Add("author_patronymic = '" + author_patronymic.Text + "'");
                        order.patronymic = author_patronymic.Text;
                    }
                    else if (author_patronymic.Text != order.patronymic.ToString())
                    {
                        set.Add("author_patronymic = '" + author_patronymic.Text + "'");
                        order.patronymic = author_patronymic.Text;
                    }
                    if (quantity.Text != order.quantity_.ToString())
                    {
                        set.Add("quantity = " + quantity.Text);
                        order.quantity_ = int.Parse(quantity.Text);

                    }
                    if (fiol[0] != reader.surname_ || fiol[1] != reader.name_)
                    {
                        Reader reader1 = readers.readers.FirstOrDefault(r => r.surname_ == fiol[0] && r.name_ == fiol[1]);
                        if (reader1 == null) { throw new Exception("Читателя с таким ФИО не существует! Пожалуйста выберите ФИО из списка читателей"); }
                        set.Add("reader_id = " + reader1.id_);
                        order.reader = reader1.id_;
                    }
                    if (date_publication.Text != order.date.ToString())
                    {
                        set.Add("date_publication = '" + date_publication.Text + "'");
                        order.date = date_publication.Text;
                    }
                    if (set.Count > 0)
                    {
                        string result = string.Join(", ", set);
                        orders.UpdDb(result, select);
                    }
                }
                else
                {
                    int id_ = orders.FindMaxId() + 1;
                    if (check_TextBox())
                    {
                        return;
                    }
                    int quantity_ = int.Parse(quantity.Text);
                    string[] fiol = fio.Text.Split(' ');
                    Reader reader1 = readers.readers.FirstOrDefault(r1 => r1.surname_ == fiol[0] && r1.name_ == fiol[1]);
                    if (reader1 == null) { throw new Exception("Читателя с таким ФИО не существует! Пожалуйста выберите ФИО из списка читателей"); }
                    int reader_ = reader1.id_;
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

        private bool IsValidFIO(string fio)
        {
            // Регулярное выражение для проверки формата ФИО
            string pattern = @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$"; // Для ФИО
            string pattern1 = @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$"; // Для ФИ

            return Regex.IsMatch(fio, pattern) || Regex.IsMatch(fio, pattern1);
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
            else if (string.IsNullOrWhiteSpace(fio.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                fio.Focus();
                return true;
            }
            else if (!IsValidFIO(fio.Text))
            {
                MessageBox.Show("Формат ФИО неверен! Пожалуйста, введите фамилию, имя и отчество через пробел");
                fio.Focus();
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

        private void date_publication_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void fio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
