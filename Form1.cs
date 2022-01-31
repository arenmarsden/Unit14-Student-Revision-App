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
                "10 * 10"
            };

            var random = new Random();
            int questionNum = 0;

            const int pointX = 30;
            const int pointY = 40;

            var prevPointY = 0;

            while (questionNum <= 10)
            {
                Label label = new();
                label.Text = questions[random.Next(questions.Length)];

                int newPointY = prevPointY == 0 ? pointY : prevPointY + 40;
                prevPointY = newPointY;

                label.Location = new Point(pointX, prevPointY == 0 ? pointY : newPointY);

                label.AutoSize = true;
                label.Size = new Size(100, 200);
                label.BackColor = Color.LightGray;
                label.Font = new Font("Arial", 14);

                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Location = new Point(label.Location.X + 300, label.Location.Y);
                textBox.BackColor = Color.LightGray;
                
                Controls.Add(textBox);
                _keyValuePairs[label] = textBox;
                questionNum++;
            }
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            Dictionary<Label, int> correctAnswers = new();
            var givenAnswers = new Dictionary<Label, int>();
            foreach (var pair in _keyValuePairs)
            {
                if (pair.Value == null || pair.Value.Text == null || pair.Value.Text.Length == 0)
                {
                    MessageBox.Show("You are missing text in one or more boxes!");
                    break;
                }
                var label = pair.Key;
                string[] numberArray = label.Text.Split('*');

                if (numberArray.Length > 0)
                {
                    int num1 = int.Parse(numberArray[0]);
                    int num2 = int.Parse(numberArray[1]);

                    correctAnswers.Add(label, num1 * num2);
                    givenAnswers.Add(label, int.Parse(pair.Value.Text));
                }
            }

            foreach (var givenAnswer in givenAnswers)
            {
                foreach (var answer in correctAnswers)
                {
                    if (givenAnswer.Key == answer.Key)
                    {
                        if (givenAnswer.Value != answer.Value )
                        {
                            var associatedTextBox = _keyValuePairs[givenAnswer.Key];
                            Controls.Remove(associatedTextBox);

                            associatedTextBox.BackColor = Color.Red;

                            Controls.Add(associatedTextBox);
                        } 
                        else
                        {
                            var associatedTextBox = _keyValuePairs[givenAnswer.Key];
                            Controls.Remove(associatedTextBox);

                            associatedTextBox.BackColor = Color.Green;

                            Controls.Add(associatedTextBox);
                        }
                    }
                }
            }

        }
    }
}
