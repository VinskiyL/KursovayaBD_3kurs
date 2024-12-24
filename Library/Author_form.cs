using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Library
{
    public partial class Author_form : Form
    {

        int select = 0;

        Authors authors;

        public event Action DataUpdated;

        Authors_books a_b;

        int index = 0;

        public Author_form(int select, Authors authors, int index, Authors_books a_b)
        {
            this.select = select;
            this.authors = authors;
            this.index = index;
            this.a_b = a_b;
            InitializeComponent();
        }

        public Author_form(int select, Authors authors)
        {
            this.select = select;
            this.authors = authors;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Author author;
            if (select != 0)
            {
                author = authors.Find(select);
                List<string> set = new List<string>();
                if (surname.Text != author.surname)
                {
                    set.Add("author_surname = '" + surname.Text + "'");
                    author.surname = surname.Text;
                }
                if (name.Text != author.name)
                {
                    set.Add("author_name = '" + name.Text + "'");
                    author.name = name.Text;
                }
                if (patronymic.Text != author.patronymic)
                {
                    set.Add("author_patronymic = '" + patronymic.Text + "'");
                    author.patronymic = patronymic.Text;
                }
                string result = string.Join(", ", set);
                authors.UpdDb(result, select);
            }
            else
            {
                check_TextBox();
                int id_ = authors.FindMaxId() + 1;
                id.Text = id_.ToString();
                if (authors.Find(id_) != null)
                {
                    MessageBox.Show("Автор с таким идентификатором уже существует!");
                    id.Focus();
                }
                string surname_ = surname.Text;
                string name_ = name.Text;
                string patronymic_ = patronymic.Text;
                author = new Author(id_, surname_, name_, patronymic_);
                authors.AddDb(author);
                if (index != 0)
                {
                    int l = a_b.FindMaxId();
                    Author_book a = new Author_book(l + 1, id_, index);
                    a_b.AddDb(a);
                }
            }

            DataUpdated?.Invoke();
            this.Close();
        }

        private void Author_form_Load(object sender, EventArgs e)
        {
            if (select != 0)
            {
                Author author = authors.Find(select);
                id.Text = select.ToString();
                surname.Text = author.surname;
                name.Text = author.name;
                patronymic.Text = author.patronymic;
            }
        }

        private void check_TextBox()
        {
            if (string.IsNullOrWhiteSpace(surname.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                surname.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                name.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(patronymic.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                patronymic.Focus();
                return;
            }
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void surname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void patronymic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(id.Text, out int value1))
            {
                if (value1 <= 0)
                {
                    MessageBox.Show("Значение в поле 'Идентификатор' должно быть целым числом больше нуля");
                    id.Text = ""; // Сбрасываем значение
                    id.Focus(); // Перемещаем фокус на первый TextBox
                }
            }
        }
    }
}
