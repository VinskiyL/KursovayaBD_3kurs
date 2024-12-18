using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library
{
    public partial class Book_form : Form
    {
        int select = 0;

        Books books;

        Authors_books a_b;

        int id = 0;

        public event Action DataUpdated;

        public Book_form(int select, Books books, int id, Authors_books a_b)
        {
            this.select = select;
            this.books = books;
            this.id = id;
            this.a_b = a_b;
            InitializeComponent();
        }

        public Book_form(int select, Books books)
        {
            this.select = select;
            this.books = books;
            InitializeComponent();
        }

        private void Book_form_Load(object sender, EventArgs e)
        {
            if (select != 0)
            {
                Book book = books.Find(select);
                index.Text = select.ToString();
                authors_mark.Text = book.mark;
                title.Text = book.title_;
                place_publication.Text = book.place;
                information_publication.Text = book.info;
                date_publication.Text = book.date;
                volume.Text = book.volume_.ToString();
                quantity_total.Text = book.total.ToString();
                quantity_remaining.Text = book.now.ToString();
                if (book.cover_ != null)
                {
                    using (var ms = new MemoryStream(book.cover_))
                    {
                        var image = System.Drawing.Image.FromStream(ms);
                        pictureBox1.Image = image;
                    }
                }
            }
            authors_mark.MaxLength = 3;
            date_publication.MaxLength = 4;
            volume.MaxLength = 5;
            quantity_total.MaxLength = 6;
            quantity_remaining.MaxLength = 6;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book book;
            if (select != 0)
            {
                book = books.Find(select);
                List<string> set = new List<string>();
                if (index.Text != select.ToString())
                {
                    int index_ = int.Parse(index.Text);
                    if (books.Find(index_) != null)
                    {
                        MessageBox.Show("Книга с таким идексом уже существует!");
                        index.Focus();
                        return;
                    }
                    set.Add("index = " + index.Text);
                    book.index_ = int.Parse(index.Text);
                }
                if (authors_mark.Text != book.mark)
                {
                    set.Add("authors_mark = '" + authors_mark.Text + "'");
                    book.mark = authors_mark.Text;
                }
                if (title.Text != book.title_)
                {
                    set.Add("title = '" + title.Text + "'");
                    book.title_ = title.Text;
                }
                if (place_publication.Text != book.place)
                {
                    set.Add("place_publication = '" + place_publication.Text + "'");
                    book.place = place_publication.Text;
                }
                if (information_publication.Text != book.info)
                {
                    set.Add("information_publication = '" + information_publication.Text + "'");
                    book.info = information_publication.Text;
                }
                if (date_publication.Text != book.date)
                {
                    set.Add("date_publication = '" + date_publication.Text + "'");
                    book.date = date_publication.Text;
                }
                if (volume.Text != book.volume_.ToString())
                {
                    set.Add("volume = " + volume.Text);
                    book.volume_ = int.Parse(volume.Text);
                }
                if (quantity_total.Text != book.total.ToString())
                {
                    set.Add("quantity_total = " + quantity_total.Text);
                    book.total = int.Parse(quantity_total.Text);
                }
                if (quantity_remaining.Text != book.now.ToString())
                {
                    set.Add("quantity_remaining = " + quantity_remaining.Text);
                    book.now = int.Parse(quantity_remaining.Text);
                }
                if (pictureBox1.Image != null)
                {
                    System.Drawing.Image image = pictureBox1.Image;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // Выберите нужный формат
                        byte[] blob = memoryStream.ToArray(); // Возвращаем массив байтов
                        string byteaString = BitConverter.ToString(blob).Replace("-", "");
                        if (book.cover_ == null)
                        {
                            set.Add("cover = (decode('" + byteaString + "','hex'))");
                            book.cover_ = blob;
                        }
                        else if (!blob.SequenceEqual(book.cover_))
                        {
                            set.Add("cover = (decode('" + byteaString + "','hex'))");
                            book.cover_ = blob;
                        }
                    }
                }
                string result = string.Join(", ", set);
                books.UpdDb(result, select);
            }
            else
            {
                check_TextBox();
                int index_ = int.Parse(index.Text);
                if(books.Find(index_) != null)
                {
                    MessageBox.Show("Книга с таким индексом уже существует!");
                    index.Focus();
                }
                string mark = authors_mark.Text;
                string title_ = title.Text;
                string place = place_publication.Text;
                string info = information_publication.Text;
                string date = date_publication.Text;
                int volume_ = int.Parse(volume.Text);
                int now = int.Parse(quantity_remaining.Text);
                int total = int.Parse(quantity_total.Text);
                byte[] cover_ = null;
                if (pictureBox1.Image != null)
                {
                    System.Drawing.Image image = pictureBox1.Image;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // Выберите нужный формат
                        cover_ = memoryStream.ToArray(); // Возвращаем массив байтов 
                    }
                }
                book = new Book(index_, mark, title_, place, info, volume_, total, now, cover_, date);
                books.AddDb(book);
                if (id != 0)
                {
                    int l = a_b.a_b.Count + 1;
                    Author_book a = new Author_book(l, id, index_);
                    a_b.AddDb(a);
                }
            }
            DataUpdated?.Invoke();
        }

        private void check_TextBox()
        {
            if (string.IsNullOrWhiteSpace(index.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                index.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(authors_mark.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                authors_mark.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(title.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                title.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(place_publication.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                place_publication.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(information_publication.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                information_publication.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(date_publication.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                date_publication.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(volume.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                volume.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(quantity_remaining.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                quantity_remaining.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(quantity_total.Text))
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены!");
                quantity_total.Focus();
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загружаем изображение в PictureBox
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void index_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void authors_mark_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void place_publication_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void information_publication_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
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

        private void volume_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void quantity_total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void quantity_remaining_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void quantity_remaining_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(quantity_remaining.Text, out int value1) && int.TryParse(quantity_total.Text, out int value2))
            {
                if (value1 > value2)
                {
                    MessageBox.Show("Значение в поле 'Сейчас книг' не должно превышать значение в поле 'Всего книг'");
                    quantity_remaining.Text = ""; // Сбрасываем значение
                    quantity_remaining.Focus(); // Перемещаем фокус на первый TextBox
                }
            }
        }

        private void index_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(index.Text, out int value1))
            {
                if (value1 <= 0)
                {
                    MessageBox.Show("Значение поля 'Индекс' должно быть целым числом больше нуля");
                    index.Text = ""; // Сбрасываем значение
                    index.Focus(); // Перемещаем фокус на первый TextBox
                }
            }
        }
    }
}
