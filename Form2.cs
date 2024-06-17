using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WorkingWithTheDatabase
{
    public partial class Form2 : Form
    {
        // Строка подключения к базе данных
        private string connectionString = "server=localhost;database=people;uid=root;pwd=cDta5hdh56yupo;";
        // Создание экземпляра подключения
        private MySqlConnection connection;
        // Создание экземпляра команды для выполнения запросов
        private MySqlCommand command;
        // Создание экземпляра адаптера для чтения данных из базы данных
        private MySqlDataAdapter adapter;
        // Переменная для хранения выбранного ID записи
        private int selectedId = 0;
        public Form2()
        {
            InitializeComponent();
            // Заполнение ComboBox
            comboBox1.Items.Add("Имя");
            comboBox1.Items.Add("Фамилия");
            comboBox1.Items.Add("Отчество");
            comboBox1.Items.Add("Год рождения");
            comboBox1.Items.Add("Возраст");
            // Подключение к базе данных
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Запрос для выборки всех данных
                string query = "SELECT * FROM myPeople";
                command = new MySqlCommand(query, connection);
                // Создание адаптера
                adapter = new MySqlDataAdapter(command);
                // Заполнение DataTable данными
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                // Очистка DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                // Отображение данных в DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения данных: " + ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Получение значения из comboBox
                string searchColumn = comboBox1.SelectedItem.ToString();
                // Создание запроса на поиск данных
                string query = "";
                if (searchColumn == "Имя")
                {
                    query = "SELECT * FROM myPeople WHERE imya LIKE @search";
                }
                else if (searchColumn == "Фамилия")
                {
                    query = "SELECT * FROM myPeople WHERE familiya LIKE @search";
                }
                else if (searchColumn == "Отчество")
                {
                    query = "SELECT * FROM myPeople WHERE otchestvo LIKE @search";
                }
                else if (searchColumn == "Год рождения")
                {
                    query = "SELECT * FROM myPeople WHERE god_rozhdeniya LIKE @search";
                }
                else if (searchColumn == "Возраст")
                {
                    query = "SELECT * FROM myPeople WHERE age LIKE @search";
                }
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@search", "%" + textBox1.Text + "%");
                // Создание адаптера
                adapter = new MySqlDataAdapter(command);
                // Заполнение DataTable данными
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                // Очистка DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                // Отображение данных в DataGridView (если есть результаты)
                if (dataTable.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Совпадений не найдено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка поиска данных: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("...", "...", "...", "...", "...");
            form3.ShowDialog();
            TextBox textBox1 = form3.TextBox1;
            TextBox textBox2 = form3.TextBox2;
            TextBox textBox3 = form3.TextBox3;
            TextBox textBox4 = form3.TextBox4;
            TextBox textBox5 = form3.TextBox5;
            if (form3.DialogResult == DialogResult.OK)
            {
                // Получение данных из AddForm
                string imya = textBox1.Text;
                string familiya = textBox2.Text;
                string otchestvo = textBox3.Text;
                string godRozhdeniya = textBox4.Text;
                string age = textBox5.Text;
                try
                {
                    // Создание запроса на добавление новой записи
                    string query = "INSERT INTO myPeople (imya, familiya, otchestvo, god_rozhdeniya, age) VALUES (@imya, @familiya, @otchestvo, @god_rozhdeniya, @age)";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@imya", imya);
                    command.Parameters.AddWithValue("@familiya", familiya);
                    command.Parameters.AddWithValue("@otchestvo", otchestvo);
                    command.Parameters.AddWithValue("@god_rozhdeniya", godRozhdeniya);
                    command.Parameters.AddWithValue("@age", age);
                    // Выполнение запроса
                    command.ExecuteNonQuery();
                    // Обновление DataGridView
                    button5_Click(sender, e); // Перезагрузить все данные в DataGridView
                    MessageBox.Show("Новая запись успешно добавлена.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи: " + ex.Message);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == 0)
                {
                    MessageBox.Show("Выберите запись для обновления.");
                    return;
                }
                // Получение данных из DataGridView
                string imya = dataGridView1.CurrentRow.Cells["imya"].Value.ToString();
                string familiya = dataGridView1.CurrentRow.Cells["familiya"].Value.ToString();
                string otchestvo = dataGridView1.CurrentRow.Cells["otchestvo"].Value.ToString();
                string godRozhdeniya = dataGridView1.CurrentRow.Cells["god_rozhdeniya"].Value.ToString();
                string age = dataGridView1.CurrentRow.Cells["age"].Value.ToString();
                // Создание запроса на обновление данных
                string query = "UPDATE myPeople SET imya = @imya, familiya = @familiya, otchestvo = @otchestvo, god_rozhdeniya = @god_rozhdeniya, age = @age WHERE id = @id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@imya", imya);
                command.Parameters.AddWithValue("@familiya", familiya);
                command.Parameters.AddWithValue("@otchestvo", otchestvo);
                command.Parameters.AddWithValue("@god_rozhdeniya", godRozhdeniya);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@id", selectedId);
                // Выполнение запроса
                command.ExecuteNonQuery();
                // Обновление DataGridView
                button5_Click(sender, e); // Перезагрузить все данные в DataGridView
                MessageBox.Show("Данные успешно обновлены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления данных: " + ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка, выбрана ли строка
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите строку для удаления.");
                    return;
                }
                // Получение ID выбранной записи
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                // Создание запроса на удаление записи
                string query = "DELETE FROM myPeople WHERE id = @id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                // Выполнение запроса
                command.ExecuteNonQuery();
                // Обновление DataGridView
                button5_Click(sender, e); // Перезагрузить все данные в DataGridView
                MessageBox.Show("Запись успешно удалена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления записи: " + ex.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Получение ID выбранной записи
                selectedId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
            }
        }
    }
}
