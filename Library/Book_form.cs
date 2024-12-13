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

        public event Action DataUpdated;

        public Book_form(int select, Books books)
        {
            this.select = select;
            this.books = books;
            InitializeComponent();
        }

        private void Book_form_Load(object sender, EventArgs e)
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
            authors_mark.MaxLength = 3;
            date_publication.MaxLength = 4;
            volume.MaxLength = 5;
            quantity_total.MaxLength = 6;
            quantity_remaining.MaxLength = 6;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book book = books.Find(select);
            List<string> set = new List<string>();
            if(index.Text != select.ToString())
            {
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
            DataUpdated?.Invoke();
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
    }
}
