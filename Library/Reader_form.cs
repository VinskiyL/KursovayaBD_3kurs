using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Library
{
    public partial class Reader_form : Form
    {
        public Reader_form()
        {
            InitializeComponent();
        }

        Readers readers = new Readers();
        int select = 0;
        public event Action DataUpdated;

        public Reader_form(int select, Readers readers)
        {
            this.select = select;
            this.readers = readers;
            InitializeComponent();
        }

        private void Reader_form_Load(object sender, EventArgs e)
        {
            if (select != 0)
            {
                Reader reader = readers.Find(select);
                surname.Text = reader.surname_;
                name.Text = reader.name_;
                patronymic.Text = reader.patronymic_;
                birthday.Text = reader.birthday_.ToString("yyyy-MM-dd");
                education.Text = reader.education_;
                profession.Text = reader.profession_;
                educational_inst.Text = reader.institut;
                city.Text = reader.city_;
                street.Text = reader.street_;
                house.Text = reader.house_.ToString();
                building_house.Text = reader.building;
                flat.Text = reader.flat_ == 0 ? "" : reader.flat_.ToString();
                phone.Text = reader.phone_;
                passport_series.Text = reader.series.ToString();
                passport_number.Text = reader.number.ToString();
                issued_by_whom.Text = reader.issued;
                date_issue.Text = reader.date.ToString("yyyy-MM-dd");
                consists_of.Text = reader.consists.ToString("yyyy-MM-dd");
                re_registration.Text = reader.re == default ? "" : reader.re.ToString("yyyy-MM-dd");
            }
            house.MaxLength = 3;
            flat.MaxLength = 3;
            passport_series.MaxLength = 4;
            passport_number.MaxLength = 6;
            phone.MaxLength = 18;
            re_registration.MaxLength = 10;
            date_issue.MaxLength = 10;
            consists_of.MaxLength = 10;
            birthday.MaxLength = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reader reader;
            if (select != 0)
            {
                if (check_TextBox())
                {
                    return;
                }
                reader = readers.Find(select);
                List<string> set = new List<string>();
                if (surname.Text != reader.surname_)
                {
                    set.Add("surname = '" + surname.Text + "'");
                    reader.surname_ = surname.Text;
                }
                if (name.Text != reader.name_)
                {
                    set.Add("name = '" + name.Text + "'");
                    reader.name_ = name.Text;
                }
                if (patronymic.Text != reader.patronymic_)
                {
                    reader.patronymic_ = (patronymic.Text == "" || patronymic.Text == null) ? null : patronymic.Text;
                    set.Add("patronymic = '" + reader.patronymic_ + "'");
                    
                }
                if (birthday.Text != reader.birthday_.ToString("yyyy-MM-dd"))
                {
                    set.Add("birthday = '" + birthday.Text + "'");
                    reader.birthday_ = DateTime.Parse(birthday.Text);
                }
                if (education.Text != reader.education_)
                {
                    set.Add("education = '" + education.Text + "'");
                    reader.education_ = education.Text;
                }
                if (profession.Text != reader.profession_)
                {
                    reader.profession_ = (profession.Text == "" || profession.Text == null) ? null : profession.Text;
                    set.Add("profession = '" + reader.profession_ + "'");
                    
                }
                if (educational_inst.Text != reader.institut)
                {
                    reader.institut = (educational_inst.Text == "" || educational_inst.Text == null) ? null : educational_inst.Text;
                    set.Add("educational_inst = '" + reader.institut + "'");
                    
                }
                if (city.Text != reader.city_)
                {
                    set.Add("city = '" + city.Text + "'");
                    reader.city_ = city.Text;
                }
                if (street.Text != reader.street_)
                {
                    set.Add("street = '" + street.Text + "'");
                    reader.street_ = street.Text;
                }
                if (house.Text != reader.house_.ToString())
                {
                    set.Add("house = " + house.Text);
                    reader.house_ = int.Parse(house.Text);
                }
                if (building_house.Text != reader.building)
                {
                    reader.building = (building_house.Text == "" || building_house.Text == null) ? null : building_house.Text;
                    set.Add("building_house = '" + reader.building + "'");
                    
                }
                if (flat.Text != reader.flat_.ToString())
                {
                    reader.flat_ = (flat.Text == "" || flat.Text == null) ? 0 : int.Parse(flat.Text);
                    set.Add("flat = " + reader.flat_);
                    
                }
                if (phone.Text != reader.phone_)
                {
                    set.Add("phone = '" + phone.Text + "'");
                    reader.phone_ = phone.Text;
                }
                if (passport_series.Text != reader.series.ToString())
                {
                    set.Add("passport_series = " + passport_series.Text);
                    reader.series = int.Parse(passport_series.Text);
                }
                if (passport_number.Text != reader.number.ToString())
                {
                    set.Add("passport_number = " + passport_number.Text);
                    reader.number = int.Parse(passport_number.Text);
                }
                if (issued_by_whom.Text != reader.issued)
                {
                    set.Add("issued_by_whom = '" + issued_by_whom.Text + "'");
                    reader.issued = issued_by_whom.Text;
                }
                if (date_issue.Text != reader.date.ToString("yyyy-MM-dd"))
                {
                    set.Add("date_issue = '" + date_issue.Text + "'");
                    reader.date = DateTime.Parse(date_issue.Text);
                }
                if (consists_of.Text != reader.consists.ToString("yyyy-MM-dd"))
                {
                    set.Add("consists_of = '" + consists_of.Text + "'");
                    reader.consists = DateTime.Parse(consists_of.Text);
                }
                if (re_registration.Text != reader.re.ToString("yyyy-MM-dd"))
                {
                    reader.re = (re_registration.Text == "" || re_registration.Text == null) ? default : DateTime.Parse(re_registration.Text);
                    set.Add("re_registration = '" + reader.re + "'");
                   
                }
                if (set.Count > 0)
                {
                    string result = string.Join(", ", set);
                    readers.UpdDb(result, select);
                }
            }
            else
            {
                int index_ = readers.FindMaxId() + 1;
                if (check_TextBox())
                {
                    return;
                }
                string surname_ = surname.Text;
                string name_ = name.Text;
                string patronymic_ = (patronymic.Text == "" || patronymic.Text == null) ? null : patronymic.Text;
                DateTime birthday_ = DateTime.Parse(birthday.Text);
                string education_ = education.Text;
                string profession_ = (profession.Text == "" || profession.Text == null) ? null : profession.Text;
                string educational_inst_ = (educational_inst.Text == "" || educational_inst.Text == null) ? null : educational_inst.Text;
                string city_ = city.Text;
                string streeet_ = street.Text;
                int house_ = int.Parse(house.Text);
                string building_house_ = (building_house.Text == "" || building_house.Text == null) ? null : building_house.Text;
                int flat_ = (flat.Text == "" || flat.Text == null) ? 0 : int.Parse(flat.Text);
                string phone_ = phone.Text;
                int passport_series_ = int.Parse(passport_series.Text);
                int passport_number_ = int.Parse(passport_number.Text);
                string issued_by = issued_by_whom.Text;
                DateTime date_issue_ = DateTime.Parse(date_issue.Text);
                DateTime consists_of_ = DateTime.Parse(consists_of.Text);
                DateTime re_registration_ = (re_registration.Text == "" || re_registration.Text == null) ? default : DateTime.Parse(re_registration.Text);
                reader = new Reader(index_, surname_, name_, patronymic_, birthday_, education_, profession_, educational_inst_,
                    city_, streeet_, house_, building_house_, flat_, phone_, passport_series_, passport_number_, issued_by,
                    date_issue_, consists_of_, re_registration_);
                readers.AddDb(reader);
            }
            DataUpdated?.Invoke();
            this.Close();
        }

        private bool check_TextBox()
        {
            if (string.IsNullOrWhiteSpace(surname.Text))
            {
                MessageBox.Show("Поле 'Фамилия' должно быть заполено!");
                surname.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Поле 'Имя' должно быть заполено!");
                name.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(birthday.Text))
            {
                MessageBox.Show("Поле 'Дата рождения' должно быть заполено!");
                birthday.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(education.Text))
            {
                MessageBox.Show("Поле 'Образование' должно быть заполено!");
                education.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(city.Text))
            {
                MessageBox.Show("Поле 'Город' должно быть заполено!");
                city.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(street.Text))
            {
                MessageBox.Show("Поле 'Улица' должно быть заполено!");
                street.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(house.Text))
            {
                MessageBox.Show("Поле 'Дом' должно быть заполено!");
                house.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(phone.Text))
            {
                MessageBox.Show("Поле 'Телефон' должно быть заполено!");
                phone.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(passport_series.Text))
            {
                MessageBox.Show("Поле 'Серия паспорта' должно быть заполено!");
                passport_series.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(passport_number.Text))
            {
                MessageBox.Show("Поле 'Номер паспорта' должно быть заполено!");
                passport_number.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(issued_by_whom.Text))
            {
                MessageBox.Show("Поле 'Кем выдан' должно быть заполено!");
                issued_by_whom.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(date_issue.Text))
            {
                MessageBox.Show("Поле 'Когда выдан' должно быть заполено!");
                date_issue.Focus();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(consists_of.Text))
            {
                MessageBox.Show("Поле 'Состоит читателем библиотеки с' должно быть заполено!");
                consists_of.Focus();
                return true;
            }
            else if (!DateTime.TryParseExact(birthday.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Дата имеет неверный формат!  Напишите дату в формате гггг-мм-дд");
                birthday.Focus();
                return true;
            }
            else if (!DateTime.TryParseExact(date_issue.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Дата имеет неверный формат!  Напишите дату в формате гггг-мм-дд");
                date_issue.Focus();
                return true;
            }
            else if (!DateTime.TryParseExact(consists_of.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Дата имеет неверный формат!  Напишите дату в формате гггг-мм-дд");
                consists_of.Focus();
                return true;
            }
            else if (!DateTime.TryParseExact(re_registration.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) && re_registration.Text.Length > 0)
            {
                MessageBox.Show("Дата имеет неверный формат! Напишите дату в формате гггг-мм-дд");
                re_registration.Focus();
                return true;
            }
            return false;
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

        private void birthday_KeyPress(object sender, KeyPressEventArgs e)
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

        private void education_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void house_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void building_house_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void flat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void phone_KeyPress(object sender, KeyPressEventArgs e)
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
            if (e.KeyChar == '-' || e.KeyChar == '(' || e.KeyChar == ' ' || e.KeyChar == ')' || e.KeyChar == '+')
            {
                return;
            }

            // Если символ не соответствует требованиям, отменяем ввод
            e.Handled = true;
        }

        private void passport_series_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void passport_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void issued_by_whom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
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

        private void consists_of_KeyPress(object sender, KeyPressEventArgs e)
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

        private void re_registration_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Add();
            Reader reader = readers.Find(select);
            string name = reader.surname_ + select.ToString();
            PasswordHasher hasher = new PasswordHasher();
            MessageBox.Show("Имя пользователя: " + name);
            if (select != 0)
            {
                User user = users.Find(select);
                if (user != null)
                {
                    string oldpassword = user.password_;
                    using (var passwordForm = new PasswordInput_form())
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            string password = passwordForm.password.Text.Trim();
                            if(password == "" || password == null)
                            {
                                MessageBox.Show("Введите пароль!");
                                passwordForm.password.Focus();
                                return;
                            }
                            if(hasher.VerifyPassword(password, oldpassword))
                            {
                                MessageBox.Show("Новый пароль не должен совпадать со старым паролем!");
                                return;
                            }
                            else
                            {
                                password = hasher.HashPassword(password);
                                user.password_ = password;
                                users.UpdDb("password = '" + password + "'", select);
                            }
                        }
                    }
                }
                else
                {
                    using (var passwordForm = new PasswordInput_form())
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            string password = passwordForm.password.Text.Trim();
                            if (password == "" || password == null)
                            {
                                MessageBox.Show("Поле 'Пароль' не может быть пустым!");
                                return;
                            }
                            password = hasher.HashPassword(password);
                            user = new User(select, name, password);
                            users.AddDb(user);
                        }
                    }
                }
            }
        }
    }
}
