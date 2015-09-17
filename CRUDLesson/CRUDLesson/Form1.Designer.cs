namespace CRUDLesson
{
    partial class frm_main
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
            this.btn_Create = new System.Windows.Forms.Button();
            this.btn_Read = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_DeleteMultiple = new System.Windows.Forms.Button();
            this.lst_persons = new System.Windows.Forms.ListView();
            this.Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.City = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Height = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(25, 35);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(97, 23);
            this.btn_Create.TabIndex = 0;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // btn_Read
            // 
            this.btn_Read.Location = new System.Drawing.Point(25, 65);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(97, 23);
            this.btn_Read.TabIndex = 1;
            this.btn_Read.Text = "Read";
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(25, 95);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(97, 23);
            this.btn_Update.TabIndex = 2;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(25, 124);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(97, 23);
            this.btn_Delete.TabIndex = 3;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_DeleteMultiple
            // 
            this.btn_DeleteMultiple.Location = new System.Drawing.Point(25, 153);
            this.btn_DeleteMultiple.Name = "btn_DeleteMultiple";
            this.btn_DeleteMultiple.Size = new System.Drawing.Size(97, 23);
            this.btn_DeleteMultiple.TabIndex = 4;
            this.btn_DeleteMultiple.Text = "Delete Multiple";
            this.btn_DeleteMultiple.UseVisualStyleBackColor = true;
            this.btn_DeleteMultiple.Click += new System.EventHandler(this.btn_DeleteMultiple_Click);
            // 
            // lst_persons
            // 
            this.lst_persons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name,
            this.Age,
            this.City,
            this.Height});
            this.lst_persons.FullRowSelect = true;
            this.lst_persons.Location = new System.Drawing.Point(128, 12);
            this.lst_persons.Name = "lst_persons";
            this.lst_persons.Size = new System.Drawing.Size(416, 352);
            this.lst_persons.TabIndex = 5;
            this.lst_persons.UseCompatibleStateImageBehavior = false;
            this.lst_persons.View = System.Windows.Forms.View.Details;
            // 
            // Name
            // 
            this.Name.Text = "Name";
            this.Name.Width = 76;
            // 
            // Age
            // 
            this.Age.Text = "Age";
            this.Age.Width = 73;
            // 
            // City
            // 
            this.City.Text = "City";
            this.City.Width = 74;
            // 
            // Height
            // 
            this.Height.Text = "Height";
            this.Height.Width = 82;
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 376);
            this.Controls.Add(this.lst_persons);
            this.Controls.Add(this.btn_DeleteMultiple);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Read);
            this.Controls.Add(this.btn_Create);
            //this.Name = "frm_main";
            this.Text = "frm_main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_DeleteMultiple;
        private System.Windows.Forms.ListView lst_persons;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader Age;
        private System.Windows.Forms.ColumnHeader City;
        private System.Windows.Forms.ColumnHeader Height;
    }
}

