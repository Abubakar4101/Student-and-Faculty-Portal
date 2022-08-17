using Bunifu.Framework.UI;

namespace CuOnline_Portal
{
    partial class Splash
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuPictureBox1 = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.bar = new Bunifu.UI.WinForms.BunifuGradientPanel();
            this.bunifuShapes3 = new Bunifu.UI.WinForms.BunifuShapes();
            this.bunifuShapes1 = new Bunifu.UI.WinForms.BunifuShapes();
            this.bunifuShapes2 = new Bunifu.UI.WinForms.BunifuShapes();
            this.bunifuShapes4 = new Bunifu.UI.WinForms.BunifuShapes();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuPictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(482, 235);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(280, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "CuOnline Portal";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuPictureBox1
            // 
            this.bunifuPictureBox1.AllowFocused = false;
            this.bunifuPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bunifuPictureBox1.AutoSizeHeight = true;
            this.bunifuPictureBox1.BorderRadius = 0;
            this.bunifuPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuPictureBox1.Image")));
            this.bunifuPictureBox1.IsCircle = false;
            this.bunifuPictureBox1.Location = new System.Drawing.Point(573, 283);
            this.bunifuPictureBox1.Name = "bunifuPictureBox1";
            this.bunifuPictureBox1.Size = new System.Drawing.Size(99, 99);
            this.bunifuPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuPictureBox1.TabIndex = 1;
            this.bunifuPictureBox1.TabStop = false;
            this.bunifuPictureBox1.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Square;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bar);
            this.panel1.Location = new System.Drawing.Point(381, 371);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 36);
            this.panel1.TabIndex = 4;
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.Transparent;
            this.bar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bar.BackgroundImage")));
            this.bar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bar.BorderRadius = 30;
            this.bar.GradientBottomLeft = System.Drawing.Color.Aqua;
            this.bar.GradientBottomRight = System.Drawing.Color.Teal;
            this.bar.GradientTopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bar.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bar.Location = new System.Drawing.Point(3, 3);
            this.bar.Name = "bar";
            this.bar.Quality = 10;
            this.bar.Size = new System.Drawing.Size(39, 28);
            this.bar.TabIndex = 0;
            // 
            // bunifuShapes3
            // 
            this.bunifuShapes3.Angle = 0F;
            this.bunifuShapes3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuShapes3.BorderColor = System.Drawing.Color.Silver;
            this.bunifuShapes3.BorderThickness = 2;
            this.bunifuShapes3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.bunifuShapes3.FillShape = true;
            this.bunifuShapes3.Location = new System.Drawing.Point(-81, -100);
            this.bunifuShapes3.Name = "bunifuShapes3";
            this.bunifuShapes3.Shape = Bunifu.UI.WinForms.BunifuShapes.Shapes.Circle;
            this.bunifuShapes3.Sides = 5;
            this.bunifuShapes3.Size = new System.Drawing.Size(347, 347);
            this.bunifuShapes3.TabIndex = 14;
            this.bunifuShapes3.Text = "bunifuShapes3";
            // 
            // bunifuShapes1
            // 
            this.bunifuShapes1.Angle = 0F;
            this.bunifuShapes1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuShapes1.BorderColor = System.Drawing.Color.Silver;
            this.bunifuShapes1.BorderThickness = 2;
            this.bunifuShapes1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.bunifuShapes1.FillShape = true;
            this.bunifuShapes1.Location = new System.Drawing.Point(999, 415);
            this.bunifuShapes1.Name = "bunifuShapes1";
            this.bunifuShapes1.Shape = Bunifu.UI.WinForms.BunifuShapes.Shapes.Circle;
            this.bunifuShapes1.Sides = 5;
            this.bunifuShapes1.Size = new System.Drawing.Size(323, 323);
            this.bunifuShapes1.TabIndex = 15;
            this.bunifuShapes1.Text = "bunifuShapes1";
            // 
            // bunifuShapes2
            // 
            this.bunifuShapes2.Angle = 0F;
            this.bunifuShapes2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuShapes2.BorderColor = System.Drawing.Color.Silver;
            this.bunifuShapes2.BorderThickness = 2;
            this.bunifuShapes2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.bunifuShapes2.FillShape = true;
            this.bunifuShapes2.Location = new System.Drawing.Point(987, -67);
            this.bunifuShapes2.Name = "bunifuShapes2";
            this.bunifuShapes2.Shape = Bunifu.UI.WinForms.BunifuShapes.Shapes.Circle;
            this.bunifuShapes2.Sides = 5;
            this.bunifuShapes2.Size = new System.Drawing.Size(347, 347);
            this.bunifuShapes2.TabIndex = 16;
            this.bunifuShapes2.Text = "bunifuShapes2";
            // 
            // bunifuShapes4
            // 
            this.bunifuShapes4.Angle = 0F;
            this.bunifuShapes4.BackColor = System.Drawing.Color.Transparent;
            this.bunifuShapes4.BorderColor = System.Drawing.Color.Silver;
            this.bunifuShapes4.BorderThickness = 2;
            this.bunifuShapes4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.bunifuShapes4.FillShape = true;
            this.bunifuShapes4.Location = new System.Drawing.Point(-61, 415);
            this.bunifuShapes4.Name = "bunifuShapes4";
            this.bunifuShapes4.Shape = Bunifu.UI.WinForms.BunifuShapes.Shapes.Circle;
            this.bunifuShapes4.Sides = 5;
            this.bunifuShapes4.Size = new System.Drawing.Size(347, 347);
            this.bunifuShapes4.TabIndex = 17;
            this.bunifuShapes4.Text = "bunifuShapes4";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 60;
            this.guna2Elipse1.TargetControl = this;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1250, 700);
            this.ControlBox = false;
            this.Controls.Add(this.bunifuShapes4);
            this.Controls.Add(this.bunifuShapes2);
            this.Controls.Add(this.bunifuShapes1);
            this.Controls.Add(this.bunifuShapes3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bunifuPictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(13, 0);
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.bunifuPictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuPictureBox bunifuPictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuGradientPanel bar;
        private Bunifu.UI.WinForms.BunifuShapes bunifuShapes3;
        private Bunifu.UI.WinForms.BunifuShapes bunifuShapes1;
        private Bunifu.UI.WinForms.BunifuShapes bunifuShapes2;
        private Bunifu.UI.WinForms.BunifuShapes bunifuShapes4;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}

