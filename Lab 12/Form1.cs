using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace Alarm_Form_L12
{
    public partial class Form1 : Form
    {
        private DateTime targetTime;
        private Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                targetTime = DateTime.ParseExact(txtTime.Text, "HH:mm:ss", null);
                timer1.Start();
                Start.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Change background color
            this.BackColor = Color.FromArgb(
                rand.Next(256),
                rand.Next(256),
                rand.Next(256)
            );

            if (DateTime.Now >= targetTime)
            {
                timer1.Stop();
                this.BackColor = SystemColors.Control;
                MessageBox.Show("ALARM! Wake up");
                Start.Enabled = true;
            }
        }

        private void txtTime_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DateTime.TryParseExact(txtTime.Text, "HH:mm:ss", null,
        System.Globalization.DateTimeStyles.None, out DateTime target))
            {
                errorProvider1.SetError(txtTime, "Invalid HH:MM:SS format");
                e.Cancel = true;
            }
            else if (target.TimeOfDay <= DateTime.Now.TimeOfDay)
            {
                errorProvider1.SetError(txtTime, "Time must be in future");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtTime, "");
            }
        }

        private void txtTime_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
