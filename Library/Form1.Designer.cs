namespace Library
{
    partial class Menu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.Readers = new System.Windows.Forms.Button();
            this.Books = new System.Windows.Forms.Button();
            this.Bookings = new System.Windows.Forms.Button();
            this.Orders = new System.Windows.Forms.Button();
            this.Authors = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Readers
            // 
            this.Readers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Readers.Location = new System.Drawing.Point(12, 45);
            this.Readers.Name = "Readers";
            this.Readers.Size = new System.Drawing.Size(178, 59);
            this.Readers.TabIndex = 0;
            this.Readers.Text = "Читатели";
            this.Readers.UseVisualStyleBackColor = true;
            this.Readers.Click += new System.EventHandler(this.Reg_Click);
            // 
            // Books
            // 
            this.Books.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Books.Location = new System.Drawing.Point(160, 143);
            this.Books.Name = "Books";
            this.Books.Size = new System.Drawing.Size(178, 59);
            this.Books.TabIndex = 3;
            this.Books.Text = "Книги";
            this.Books.UseVisualStyleBackColor = true;
            this.Books.Click += new System.EventHandler(this.Books_Click);
            // 
            // Bookings
            // 
            this.Bookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Bookings.Location = new System.Drawing.Point(317, 45);
            this.Bookings.Name = "Bookings";
            this.Bookings.Size = new System.Drawing.Size(178, 59);
            this.Bookings.TabIndex = 4;
            this.Bookings.Text = "Бронирование";
            this.Bookings.UseVisualStyleBackColor = true;
            this.Bookings.Click += new System.EventHandler(this.Bookings_Click);
            // 
            // Orders
            // 
            this.Orders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Orders.Location = new System.Drawing.Point(610, 45);
            this.Orders.Name = "Orders";
            this.Orders.Size = new System.Drawing.Size(178, 59);
            this.Orders.TabIndex = 5;
            this.Orders.Text = "Заказы";
            this.Orders.UseVisualStyleBackColor = true;
            this.Orders.Click += new System.EventHandler(this.Orders_Click);
            // 
            // Authors
            // 
            this.Authors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Authors.Location = new System.Drawing.Point(472, 143);
            this.Authors.Name = "Authors";
            this.Authors.Size = new System.Drawing.Size(178, 59);
            this.Authors.TabIndex = 6;
            this.Authors.Text = "Авторы";
            this.Authors.UseVisualStyleBackColor = true;
            this.Authors.Click += new System.EventHandler(this.Authors_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(776, 27);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "Выберите действие";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 237);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Authors);
            this.Controls.Add(this.Orders);
            this.Controls.Add(this.Bookings);
            this.Controls.Add(this.Books);
            this.Controls.Add(this.Readers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "Меню";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Readers;
        private System.Windows.Forms.Button Books;
        private System.Windows.Forms.Button Bookings;
        private System.Windows.Forms.Button Orders;
        private System.Windows.Forms.Button Authors;
        private System.Windows.Forms.TextBox textBox1;
    }
}

