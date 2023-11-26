namespace angle_of_line_to_be_drawn
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            buttonAddLine = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(10, 10);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(458, 424);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // buttonAddLine
            // 
            buttonAddLine.Location = new Point(147, 475);
            buttonAddLine.Name = "buttonAddLine";
            buttonAddLine.Size = new Size(170, 34);
            buttonAddLine.TabIndex = 1;
            buttonAddLine.Text = "Add Line";
            buttonAddLine.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 544);
            Controls.Add(buttonAddLine);
            Controls.Add(pictureBox);
            Name = "MainForm";
            Padding = new Padding(10, 10, 10, 110);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private Button buttonAddLine;
    }
}
