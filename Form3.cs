using System;
using System.Windows.Forms;
namespace WorkingWithTheDatabase
{
    public partial class Form3 : Form
    {
        public TextBox TextBox1
        {
            get { return textBox1; }
        }
        public TextBox TextBox2
        {
            get { return textBox2; }
        }
        public TextBox TextBox3
        {
            get { return textBox3; }
        }
        public TextBox TextBox4
        {
            get { return textBox4; }
        }
        public TextBox TextBox5
        {
            get { return textBox5; }
        }
        public Form3(string imya, string familiya, string otchestvo, string godRozhdeniya, string age)
        {
            InitializeComponent();
            textBox1.Text = imya;
            textBox2.Text = familiya;
            textBox3.Text = otchestvo;
            textBox4.Text = godRozhdeniya;
            textBox5.Text = age;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустые значения
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }
            // Установка DialogResult.OK, чтобы указать, что данные были введены
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
