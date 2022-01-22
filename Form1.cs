using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StudentRevisionApp
{
    public partial class Form1 : Form
    {
        private Dictionary<Label, TextBox> _keyValuePairs = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] questions =
            {
                "9 * 10",
                "10 * 12",
                "8 * 4",
                "2 * 12",
                "(0.25 * 100) / 10",
                "0.072 * 32",
            };

            var random = new Random();
            var questionNum = 0;

            const int pointX = 30;
            const int pointY = 40;

            var prevPointY = 0;

            do
            {
                Label label = new();
                label.Text = questions[random.Next(questions.Length)];
                label.Location = new Point(pointX, prevPointY == 0 ? pointY : prevPointY + 40);
                label.AutoSize = true;
                label.BackColor = Color.LightGray;
                label.Font = new Font("Arial", 14);

                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Location = new Point(label.Location.X + 200, label.Location.Y);
                textBox.BackColor = Color.LightGray;
                
                Controls.Add(textBox);
                _keyValuePairs[label] = textBox;
                prevPointY = pointY;
                questionNum++;
            } while (questionNum != 5);
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            foreach (var pair in _keyValuePairs)
            {
                var label = pair.Key;
                string[] numberArray = label.Text.Split('*');
                
                

            }
        }
    }
}
