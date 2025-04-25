namespace Alarm_Form_L12
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            txtTime = new TextBox();
            Start = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            errorProvider1 = new ErrorProvider(components);
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // txtTime
            // 
            txtTime.Location = new Point(197, 109);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(125, 27);
            txtTime.TabIndex = 0;
            txtTime.TextChanged += txtTime_TextChanged;
            txtTime.Validating += txtTime_Validating;
            // 
            // Start
            // 
            Start.Location = new Point(209, 164);
            Start.Name = "Start";
            Start.Size = new Size(94, 29);
            Start.TabIndex = 1;
            Start.Text = "Start Alarm";
            Start.UseVisualStyleBackColor = true;
            Start.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(177, 215);
            label1.Name = "label1";
            label1.Size = new Size(171, 20);
            label1.TabIndex = 2;
            label1.Text = "Enter time in HH:MM:SS ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 490);
            Controls.Add(label1);
            Controls.Add(Start);
            Controls.Add(txtTime);
            Name = "Form1";
            Text = "Alarm Clock";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTime;
        private Button Start;
        private System.Windows.Forms.Timer timer1;
        private ErrorProvider errorProvider1;
        private Label label1;
    }
}
