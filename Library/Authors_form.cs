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
                    textBox1.Text = "Напишите идентификаторы авторов в текстовом поле внизу окна";
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
                    System.Windows.Forms.TextBox readOnlyTextBox = new System.Windows.Forms.TextBox();
                    readOnlyTextBox.ReadOnly = true;
                    readOnlyTextBox.BorderStyle = BorderStyle.None;
                    readOnlyTextBox.Font = new Font(readOnlyTextBox.Font.FontFamily, 12);
                    readOnlyTextBox.Text = "Напишите идентификаторы авторов через запятую и нажмите Enter";
                    readOnlyTextBox.Size = new Size(535, 30); // Установите размер
                    readOnlyTextBox.Location = new Point(704, 481);

                    // Создание второго текстового бокса для ввода
                    System.Windows.Forms.TextBox inputTextBox = new System.Windows.Forms.TextBox();
                    inputTextBox.Font = new Font(inputTextBox.Font.FontFamily, 12);
                    inputTextBox.Size = new Size(535, 30); // Установите размер
                    inputTextBox.Location = new Point(704, 518);
                    inputTextBox.ScrollBars = ScrollBars.Horizontal;

                    inputTextBox.KeyPress += (sender0, e0) =>
                    {
                        if (!char.IsControl(e0.KeyChar) && !char.IsDigit(e0.KeyChar) && e0.KeyChar != ',')
                        {
                            // Если нет, отменяем событие
                            e0.Handled = true;
                        }
                    };

                    // Обработчик события KeyDown для второго текстового бокса
                    inputTextBox.KeyDown += (sender1, e1) =>
                    {
                        if (e1.KeyCode == Keys.Enter)
                        {
                            // Логика для обработки введенных данных
                            string inputText = inputTextBox.Text;

                            // Здесь вы можете добавить код для обработки введенных номеров книг
                            string[] bookNumbers = inputText.Split(',');

                            // Пример: поиск и добавление книг по введенным номерам
                            foreach (var number in bookNumbers)
                            {
                                if (int.TryParse(number.Trim(), out int authorNumber))
                                {
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
                                            MessageBox.Show("Автор с идентификатором " + authorNumber + " была ранее добавлен данной книге");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Автора с идентификатором " + authorNumber + " не существует");
                                    }
                                }
                            }

                            // Выход из метода или закрытие формы
                            DataUpdated();
                            textBox2.Text = "Список аторов книги";
                            textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                            Update.Enabled = true;
                            Insert.Enabled = true;
                            Delete.Enabled = true;
                            Books.Enabled = true;
                            this.Controls.Remove(inputTextBox);
                            this.Controls.Remove(readOnlyTextBox);
                            return;
                        }
                    };

                    // Добавление текстовых боксов на форму
                    this.Controls.Add(inputTextBox);
                    this.Controls.Add(readOnlyTextBox);
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
