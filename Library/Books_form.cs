using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Books_form : Form
    {
        public Books_form()
        {
            InitializeComponent();
        }

        Books books = new Books();

        int select = 0;


        private void Books_form_Load(object sender, EventArgs e)
        {
            try
            {
                books.Add();
                if (books == null)
                {
                    throw new Exception("Нет информации о книгах");
                }
                for (int i = 0; i < books.books.Count; i++)
                {
                    dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                        books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
                }
            }
            catch(Exception ex)
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

                select = (int)selectedRow.Cells["index"].Value;
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
                Book_form form = new Book_form(select, books);
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
            for (int i = 0; i < books.books.Count; i++)
            {
                dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                    books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
            }
        }
    }
}
