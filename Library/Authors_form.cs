using System;
using System.Drawing;
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
                Books.Enabled = true;
                textBox2.Text = "Список авторов";
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
                    textBox2.Text = "Список авторов книги";
                    textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                    Books.Enabled = false;
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
            if (index != 0)
            {
                DialogResult result = MessageBox.Show("Вы хотите добавить уже существующего автора?", "Выбор действия",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    textBox2.Text = "Список авторов, не принадлежащих данной книге";
                    textBox1.Text = "Выберите автора из списка ниже и нажмите Enter для добавления.";
                    Update.Enabled = false;
                    Insert.Enabled = false;
                    Delete.Enabled = false;
                    Books.Enabled = false;

                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < authors.authors.Count; i++)
                    {
                        if (!a_b.Find(authors.authors[i].id, index))
                        {
                            dataGridView1.Rows.Add(authors.authors[i].id, authors.authors[i].surname, authors.authors[i].name,
                                authors.authors[i].patronymic);
                        }
                    }

                    // Настройка DataGridView
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Полное выделение строки
                    dataGridView1.MultiSelect = false; // Запрет множественного выделения

                    // Обработчик события KeyDown для DataGridView
                    dataGridView1.KeyDown += (sender1, e1) =>
                    {
                        if (e1.KeyCode == Keys.Enter)
                        {
                            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, выбрана ли строка
                            {
                                var selectedRow = dataGridView1.SelectedRows[0];
                                int authorNumber = (int)selectedRow.Cells["id"].Value; // Замените "IdColumn" на имя вашего столбца ID

                                if (authors.Find(authorNumber) != null)
                                {
                                    if (!a_b.Find(authorNumber, index))
                                    {
                                        int i = a_b.FindMaxId();
                                        Author_book a = new Author_book(i + 1, authorNumber, index);
                                        a_b.AddDb(a);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Автор с идентификатором " + authorNumber + " был ранее добавлен данной книге");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Автора с идентификатором " + authorNumber + " не существует");
                                }

                                // Обновление текста и включение кнопок
                                textBox2.Text = "Список авторов книги";
                                textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                                Update.Enabled = true;
                                Insert.Enabled = true;
                                Delete.Enabled = true;
                                Books.Enabled = true;
                                DataUpdated();
                            }
                            e1.Handled = true; // Отменяем дальнейшую обработку события
                            return;
                        }
                    };
                }

                else if (result == DialogResult.No)
                {
                    Author_form form = new Author_form(0, authors, index, a_b);
                    form.DataUpdated += DataUpdated;
                    form.Show();
                }
            }
            else
            {
                Author_form form = new Author_form(0, authors, index, a_b);
                form.DataUpdated += DataUpdated;
                form.Show();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                if (index != 0)
                {
                    result = MessageBox.Show("Вы хотите удалить  автора только у книги?", "Выбор действия",
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        if (select == 0)
                        {
                            throw new Exception("Строка не выбрана");
                        }

                        // Запрос подтверждения у пользователя
                        result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
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
                    else if (result == DialogResult.Yes)
                    {
                        if (select == 0)
                        {
                            throw new Exception("Строка не выбрана");
                        }
                        a_b.DelDb(select, index);
                        DataUpdated();
                    }
                }
                else
                {
                    if (select == 0)
                    {
                        throw new Exception("Строка не выбрана");
                    }

                    // Запрос подтверждения у пользователя
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
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
