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
    public partial class Authors_form : Form
    {
        public Authors_form(int index)
        {
            this.index = index;
            InitializeComponent();
        }
        public Authors_form()
        {
            InitializeComponent();
        }

        int index = 0;
        Authors authors = new Authors();
        int select = 0;
        Authors_books a_b = new Authors_books();

        private void Authors_form_Load(object sender, EventArgs e)
        {
            try
            {
                authors.Add();
                if (authors == null)
                {
                    throw new Exception("Нет информации об авторах");
                }
                if (index == 0)
                {
                    for (int i = 0; i < authors.authors.Count; i++)
                    {
                        dataGridView1.Rows.Add(authors.authors[i].id, authors.authors[i].surname, authors.authors[i].name,
                            authors.authors[i].patronymic);
                    }
                }
                else
                {
                    a_b.Add();
                    for (int i = 0; i < authors.authors.Count; i++)
                    {
                        if (a_b.Find(authors.authors[i].id, index))
                        {
                            dataGridView1.Rows.Add(authors.authors[i].id, authors.authors[i].surname, authors.authors[i].name,
                                authors.authors[i].patronymic);
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

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Author_form form = new Author_form(select, authors);
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
            if (index == 0)
            {
                for (int i = 0; i < authors.authors.Count; i++)
                {
                    dataGridView1.Rows.Add(authors.authors[i].id, authors.authors[i].surname, authors.authors[i].name,
                        authors.authors[i].patronymic);
                }
            }
            else
            {
                for (int i = 0; i < authors.authors.Count; i++)
                {
                    if (a_b.Find(authors.authors[i].id, index))
                    {
                        dataGridView1.Rows.Add(authors.authors[i].id, authors.authors[i].surname, authors.authors[i].name,
                            authors.authors[i].patronymic);
                    }
                }
            }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            Author_form form = new Author_form(0, authors);
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
                    authors.DelDb(select);
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

        private void Books_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Books_form form = new Books_form(select);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
