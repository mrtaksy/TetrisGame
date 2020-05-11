namespace TetrisOyun
{
    partial class OyunForm
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
            this.PicYenidenBaslat = new System.Windows.Forms.PictureBox();
            this.PicBaslat = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicYenidenBaslat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBaslat)).BeginInit();
            this.SuspendLayout();
            // 
            // PicYenidenBaslat
            // 
            this.PicYenidenBaslat.Image = global::TetrisOyun.Properties.Resources.ResBtn_Off;
            this.PicYenidenBaslat.Location = new System.Drawing.Point(366, 467);
            this.PicYenidenBaslat.Name = "PicYenidenBaslat";
            this.PicYenidenBaslat.Size = new System.Drawing.Size(81, 85);
            this.PicYenidenBaslat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicYenidenBaslat.TabIndex = 2;
            this.PicYenidenBaslat.TabStop = false;
            this.PicYenidenBaslat.Click += new System.EventHandler(this.PicYenidenBaslat_Click);
            // 
            // PicBaslat
            // 
            this.PicBaslat.Image = global::TetrisOyun.Properties.Resources.StartBtn_On;
            this.PicBaslat.Location = new System.Drawing.Point(331, 127);
            this.PicBaslat.Name = "PicBaslat";
            this.PicBaslat.Size = new System.Drawing.Size(158, 138);
            this.PicBaslat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBaslat.TabIndex = 1;
            this.PicBaslat.TabStop = false;
            this.PicBaslat.Click += new System.EventHandler(this.PicBaslat_Click);
            // 
            // OyunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 600);
            this.Controls.Add(this.PicYenidenBaslat);
            this.Controls.Add(this.PicBaslat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "OyunForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TETRIS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OyunForm_FormClosing);
            this.Load += new System.EventHandler(this.OyunForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicYenidenBaslat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBaslat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBaslat;
        private System.Windows.Forms.PictureBox PicYenidenBaslat;
    }
}

