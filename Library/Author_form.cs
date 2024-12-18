using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Author_form : Form
    {

        int select = 0;

        Authors authors;

        public event Action DataUpdated;

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
                if (id.Text != select.ToString())
                {
                    int id_ = int.Parse(id.Text);
                    if (authors.Find(id_) != null)
                    {
                        MessageBox.Show("Автор с таким идентификатором уже существует!");
                        id.Focus();
                        return;
                    }
                    set.Add("id = " + id.Text);
                    author.id = int.Parse(id.Text);
                }
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
                int id_ = int.Parse(id.Text);
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
            }

            DataUpdated?.Invoke();
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
            if (string.IsNullOrWhiteSpace(id.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                id.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(surname.Text))
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
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
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
