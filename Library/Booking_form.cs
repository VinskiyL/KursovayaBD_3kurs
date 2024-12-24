using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (select != 0)
            {
                Booking booking = bookings.Find(select);
                index.Text = booking.index_.ToString();
                reader_id.Text = booking.reader.ToString();
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
                    List<string> set = new List<string>();
                    if (index.Text != booking.index_.ToString())
                    {
                        if (books.Find(int.Parse(index.Text)) == null)
                        {
                            throw new Exception("Книги с таким индексом не существует!");
                        }
                        set.Add("index = " + index.Text);
                        booking.index_ = int.Parse(index.Text);
                    }
                    if (reader_id.Text != booking.reader.ToString())
                    {
                        if (readers.Find(int.Parse(reader_id.Text)) == null)
                        {
                            throw new Exception("Читателя с таким инденитификатором не существует!");
                        }
                        set.Add("reader_id = " + reader_id.Text);
                        booking.reader = int.Parse(reader_id.Text);
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
                    string result = string.Join(", ", set);
                    bookings.UpdDb(result, select);
                }
                else
                {
                    int id_ = bookings.FindMaxId() + 1;
                    if (check_TextBox())
                    {
                        return;
                    }
                    int index_ = int.Parse(index.Text);
                    if (books.Find(index_) == null)
                    {
                        throw new Exception("Книги с таким индексом не существует");
                    }
                    int quantity_ = int.Parse(quantity.Text);
                    int reader_ = int.Parse(reader_id.Text);
                    if (readers.Find(reader_) == null)
                    {
                        throw new Exception("Читателя с таким инденитификатором не существует!");
                    }
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

        private bool check_TextBox()
        {
            if (string.IsNullOrWhiteSpace(index.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                index.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(reader_id.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                reader_id.Focus();
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

        private void index_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
