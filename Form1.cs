using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static lab3_Dudka.Serializing;

namespace lab3_Dudka
{
    public partial class Form1 : Form
    {
        private Serializing.Transform transform = new Serializing.Transform();
        private List<Researcher> researchers;
        public Form1()
        {
            InitializeComponent();       
            buttonSerialize.Click += buttonSerialize_Click;

            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();
        }    

        private void DisplayData()
        {
            try
            {
                string jsonText = File.ReadAllText("data.json"); 
                var data = JsonConvert.DeserializeObject<dynamic>(jsonText); 

                foreach (var item in data)
                {
                    dataGridView1.Rows.Add(item.id, item.name, item.surname, item.degree, item.dateofreceiving); 
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл data.json не знайдено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void chooseJSON_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = (@"C:\Users\ACER\source\repos\lab3_Dudka\lab3_Dudka\data.json");

                    researchers = transform.DeserializeResearcherFromJsonFile(filePath);
                    
                    dataGridView1.Rows.Clear();

                    foreach (var item in researchers)
                    {
                        dataGridView1.Rows.Add(item.id, item.name, item.surname, item.degree, item.dateofreceiving); 
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Файл не знайдено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}");
                }
            }
        }
       
        private void buttonSerialize_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Users\ACER\source\repos\lab3_Dudka\lab3_Dudka\data.json";
            List<Serializing.Researcher> researchers = new List<Serializing.Researcher>
            {
                new Researcher { id = "1", name = "John", surname = "Taylor", degree = "Psy.D.", dateofreceiving = "25/03/2005" },
                new Researcher { id = "2", name = "Danil", surname = "Miller", degree = "Ed.D.", dateofreceiving = "18/06/2008" },
                new Researcher { id = "3", name = "David", surname = "Evans", degree = "Eng.D.", dateofreceiving = "17/08/2013" },
                new Researcher { id = "4", name = "Michael", surname = "Davis", degree = "Psy.D.", dateofreceiving = "15/09/2014" },
                new Researcher { id = "5", name = "Rachel", surname = "Miller", degree = "M.D.", dateofreceiving = "27/11/2012" },
            };

            transform.SerializeResearcherToJsonFile(researchers, filePath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Form2 = new Form();
            Form2.Text = "Про програму";
            var TextBox1 = new System.Windows.Forms.Label();
            TextBox1.Text = "Дудка Дарина, група К25, 2 курс\n" +
                            " \n" +
                            "Як користуватись програмою... 😮\n" +
                            " \n" +
                            "- Щоб вивести дані з json файлу - натисни на `choose json file` та в диспетчері обери файл `data.json`, дані будуть виведені в таблиці\n" +
                            " \n" +
                            "- Потрібно відфільтрувати за критеріями? Заповни на основі даних в таблиці текстові поля  id, name або surname(заповнення всіх критерій теж допускається), натискаєш кнопку `search`- таблиця змінена\n" +
                            " \n" +
                            "- Цікаво як серіалізуються дані в файл json - нажми `serialize` та відкрий файл data.json в проекті"; 
            
            TextBox1.Dock = DockStyle.Fill;
            Form2.Controls.Add(TextBox1);
            Form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Id = idTextBox.Text;
            string Name = nameTextBox.Text;
            string Surname = surnameTextBox.Text;
            var researchers_filt = researchers.Where(researcher => (Id == string.Empty? true: researcher.id == Id) 
                                                    && (Name == string.Empty ? true : researcher.name == Name)
                                                    && (Surname == string.Empty ? true : researcher.surname == Surname))
                                                    .ToList();
            dataGridView1.Rows.Clear();
           
            foreach (var researcher in researchers_filt)
            {
                dataGridView1.Rows.Add(researcher.id, researcher.name, researcher.surname, researcher.degree, researcher.dateofreceiving);
            }
        }   
    }
}