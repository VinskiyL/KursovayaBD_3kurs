using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Library
{
    public partial class Booking_form : Form
    {
        public Booking_form()
        {
            InitializeComponent();
        }

        Bookings bookings = new Bookings();
        int select = 0;
        public event Action DataUpdated;

        public Booking_form(int select, Bookings bookings)
        {
            this.select = select;
            this.bookings = bookings;
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
            Books books = new Books();
            books.Add();
            List<string> fios = new List<string>();
            List<string> titles = new List<string>();
            foreach (Book b in books.books)
            {
                titles.Add(b.title_);
            }
            foreach (Reader r in readers.readers)
            {
                fios.Add(r.surname_ + " " + r.name_ + " " + r.patronymic_ ?? string.Empty);
            }
            // Устанавливаем источник данных для ComboBox
            fio.DataSource = fios;
            title.DataSource = titles;
            // Включаем автозаполнение
            
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

        private void Booking_form_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeComboBox();
                if (select != 0)
                {
                    Readers readers = new Readers();
                    Books books = new Books();
                    readers.Add();
                    books.Add();
                    Booking booking = bookings.Find(select);
                    Book book = books.Find(booking.index_);
                    Reader reader = readers.Find(booking.reader);
                    title.Text = book.title_;
                    fio.Text = reader.surname_ + " " + reader.name_ + " " + reader.patronymic_ ?? string.Empty;
                    quantity.Text = booking.quantity_.ToString();
                    date_issue.Text = booking.issue.ToString("yyyy-MM-dd");
                    date_return.Text = booking.return_.ToString("yyyy-MM-dd");
                    returned.Enabled = false;
                    if (booking.issued)
                    {
                        issued.Checked = true;
                        issued.Enabled = false;
                        returned.Enabled = true;
                    }
                    if (booking.returned)
                    {
                        returned.Checked = true;
                        returned.Enabled = false;
                    }
                }
                else
                {
                    returned.Enabled = false;
                }
                quantity.MaxLength = 1;
                date_issue.MaxLength = 10;
                date_return.MaxLength = 10;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Booking booking;
            Books books = new Books();
            books.Add();
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
                    booking = bookings.Find(select);
                    Book book = books.Find(booking.index_);
                    Reader reader = readers.Find(booking.reader);
                    string[] fiol = fio.Text.Split(' ');
                    List<string> set = new List<string>();
                    if (title.Text != book.title_)
                    {
                        Book book1 = books.books.FirstOrDefault(b => b.title_ == title.Text);
                        if (book1 == null) { throw new Exception("Книги с таким названием не существует!"); }
                        set.Add("index = " + book1.index_);
                        booking.index_ = book1.index_;
                    }
                    if (fiol[0] != reader.surname_ || fiol[1] != reader.name_)
                    {
                        Reader reader1 = readers.readers.FirstOrDefault(r => r.surname_ == fiol[0] && r.name_ == fiol[1]);
                        if(reader1 == null) { throw new Exception("Читателя с таким ФИО не существует"); }
                        set.Add("reader_id = " + reader1.id_);
                        booking.reader = reader1.id_;
                    }
                    if (quantity.Text != booking.quantity_.ToString())
                    {
                        set.Add("quantity = " + quantity.Text);
                        booking.quantity_ = int.Parse(quantity.Text);

                    }
                    if (date_issue.Text != booking.issue.ToString("yyyy-MM-dd"))
                    {
                        set.Add("date_issue = '" + date_issue.Text + "'");
                        booking.issue = DateTime.Parse(date_issue.Text);
                    }
                    if (date_return.Text != booking.return_.ToString("yyyy-MM-dd"))
                    {
                        set.Add("date_return = '" + date_return.Text + "'");
                        booking.return_ = DateTime.Parse(date_return.Text);
                    }
                    if (issued.Checked != booking.issued)
                    {
                        issued.Enabled = false;
                        returned.Enabled = true;
                        issued.Checked = true;
                        set.Add("issued = " + issued.Checked);
                        booking.issued = issued.Checked;
                    }
                    if (returned.Checked != booking.returned)
                    {
                        returned.Enabled = false;
                        returned.Checked = true;
                        set.Add("returned = " + returned.Checked);
                        booking.returned = returned.Checked;
                    }
                    if (set.Count > 0)
                    {
                        string result = string.Join(", ", set);
                        bookings.UpdDb(result, select);
                    }
                }
                else
                {
                    int id_ = bookings.FindMaxId() + 1;
                    if (check_TextBox())
                    {
                        return;
                    }
                    Book book1 = books.books.FirstOrDefault(b => b.title_ == title.Text);
                    if (book1 == null) { throw new Exception("Книги с таким названием не существует!"); }
                    int index_ = book1.index_;
                    int quantity_ = int.Parse(quantity.Text);
                    string[] fiol = fio.Text.Split(' ');
                    Reader reader1 = readers.readers.FirstOrDefault(r1 => r1.surname_ == fiol[0] && r1.name_ == fiol[1]);
                    if (reader1 == null) { throw new Exception("Читателя с таким ФИО не существует"); }
                    int reader_ = reader1.id_;
                    DateTime issue_ = DateTime.Parse(date_issue.Text);
                    DateTime return_ = DateTime.Parse(date_return.Text);
                    bool i = false;
                    bool r = false;
                    if (issued.Checked == true)
                    {
                        issued.Enabled = false;
                        returned.Enabled = true;
                        i = true;
                    }
                    if (returned.Checked == true)
                    {
                        returned.Enabled = false;
                        r = true;
                    }
                    booking = new Booking(index_, reader_, quantity_, issue_, return_, id_, i, r);
                    bookings.AddDb(booking);
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
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                title.Focus();
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
                MessageBox.Show("Формат ФИО неверен! Пожалуйста, введите фамилию, имя и отчество через пробел.");
                fio.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(quantity.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                quantity.Focus();
                return true;
            }
            else if(int.Parse(quantity.Text) <= 0 || int.Parse(quantity.Text) >= 5)
            {
                MessageBox.Show("Количество должно быть больше 0 и меньше 5!");
                quantity.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(date_issue.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                date_issue.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(date_return.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                date_return.Focus();
                return true;
            }
            else if (!DateTime.TryParseExact(date_issue.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Дата имеет неверный формат!");
                date_issue.Focus();
                return true;
            }
            else if (!DateTime.TryParseExact(date_return.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Дата имеет неверный формат!");
                date_return.Focus();
                return true;
            }
            return false;
        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void date_issue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            // Если вводимый символ является цифрой, разрешаем
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // Разрешаем ввод символов разделителей, например '/', '-' или '.'
            if (e.KeyChar == '-')
            {
                return;
            }

            // Если символ не соответствует требованиям, отменяем ввод
            e.Handled = true;
        }

        private void date_return_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            // Если вводимый символ является цифрой, разрешаем
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // Разрешаем ввод символов разделителей, например '/', '-' или '.'
            if (e.KeyChar == '-')
            {
                return;
            }

            // Если символ не соответствует требованиям, отменяем ввод
            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Books_form form = new Books_form();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fio_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
