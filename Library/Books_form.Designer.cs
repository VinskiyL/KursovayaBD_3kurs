namespace Library
{
    partial class Books_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Books_form));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authors_mark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.place_publication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.information_publication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_publication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity_remaining = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Update = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.authors_mark,
            this.title,
            this.place_publication,
            this.information_publication,
            this.date_publication,
            this.volume,
            this.quantity_total,
            this.quantity_remaining});
            this.dataGridView1.Location = new System.Drawing.Point(12, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1416, 399);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // index
            // 
            this.index.HeaderText = "Индекс";
            this.index.MinimumWidth = 6;
            this.index.Name = "index";
            this.index.ReadOnly = true;
            this.index.Width = 125;
            // 
            // authors_mark
            // 
            this.authors_mark.HeaderText = "Авторская метка";
            this.authors_mark.MinimumWidth = 6;
            this.authors_mark.Name = "authors_mark";
            this.authors_mark.ReadOnly = true;
            this.authors_mark.Width = 125;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.title.HeaderText = "Название";
            this.title.MinimumWidth = 6;
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // place_publication
            // 
            this.place_publication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.place_publication.HeaderText = "Место публикации";
            this.place_publication.MinimumWidth = 6;
            this.place_publication.Name = "place_publication";
            this.place_publication.ReadOnly = true;
            // 
            // information_publication
            // 
            this.information_publication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.information_publication.HeaderText = "Информация об издании";
            this.information_publication.MinimumWidth = 6;
            this.information_publication.Name = "information_publication";
            this.information_publication.ReadOnly = true;
            // 
            // date_publication
            // 
            this.date_publication.HeaderText = "Дата публикации";
            this.date_publication.MinimumWidth = 6;
            this.date_publication.Name = "date_publication";
            this.date_publication.ReadOnly = true;
            this.date_publication.Width = 125;
            // 
            // volume
            // 
            this.volume.HeaderText = "Количество страниц";
            this.volume.MinimumWidth = 6;
            this.volume.Name = "volume";
            this.volume.ReadOnly = true;
            this.volume.Width = 125;
            // 
            // quantity_total
            // 
            this.quantity_total.HeaderText = "Всего книг";
            this.quantity_total.MinimumWidth = 6;
            this.quantity_total.Name = "quantity_total";
            this.quantity_total.ReadOnly = true;
            this.quantity_total.Width = 125;
            // 
            // quantity_remaining
            // 
            this.quantity_remaining.HeaderText = "Сейчас книг";
            this.quantity_remaining.MinimumWidth = 6;
            this.quantity_remaining.Name = "quantity_remaining";
            this.quantity_remaining.ReadOnly = true;
            this.quantity_remaining.Width = 125;
            // 
            // Update
            // 
            this.Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Update.Location = new System.Drawing.Point(12, 481);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(167, 65);
            this.Update.TabIndex = 1;
            this.Update.Text = "Редактировать";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // Books_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 552);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Books_form";
            this.Text = "Книги";
            this.Load += new System.EventHandler(this.Books_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn authors_mark;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn place_publication;
        private System.Windows.Forms.DataGridViewTextBoxColumn information_publication;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_publication;
        private System.Windows.Forms.DataGridViewTextBoxColumn volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity_remaining;
        private System.Windows.Forms.Button Update;
    }
}