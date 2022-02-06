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
        private readonly Dictionary<Label, TextBox> _keyValuePairs = new();
        private int Score = 0;
        private readonly Label ScoreLabel = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScoreLabel.Text = "0";
            ScoreLabel.AutoSize = true;
            ScoreLabel.Location = new Point(900, 0);
            ScoreLabel.Font = new Font("Arial", 14);
            ScoreLabel.TabIndex = 0;
            ScoreLabel.BackColor = SystemColors.Highlight;

            Controls.Add(ScoreLabel);

            var random = new Random();
            int questionNum = 0;

            const int pointX = 30;
            const int pointY = 40;

            var prevPointY = 0;

            while (questionNum <= 10)
            {
                Label label = new();
                int randomNumber1 = random.Next(1, 12);
                int randomNumber2 = random.Next(1, 12);
                label.Text = String.Format("{0} * {1}", randomNumber1, randomNumber2);

                int newPointY = prevPointY == 0 ? pointY : prevPointY + 40;
                prevPointY = newPointY;

                label.Location = new Point(pointX, prevPointY == 0 ? pointY : newPointY);

                label.AutoSize = true;
                label.Size = new Size(100, 200);
                label.BackColor = Color.LightGray;
                label.Font = new Font("Arial", 14);

                this.Controls.Add(label);

                TextBox textBox = new();
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

                if (int.TryParse(pair.Value.Text, out int num))
                {
                    var label = pair.Key;
                    string[] numberArray = label.Text.Split('*');

                    if (numberArray.Length > 0)
                    {
                        int num1 = int.Parse(numberArray[0]);
                        int num2 = int.Parse(numberArray[1]);

                        correctAnswers.Add(label, num1 * num2);
                        givenAnswers.Add(label, num);
                    }
                } else
                {
                    MessageBox.Show("Please only enter integers!");
                    return;
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

                            Score += 10;
                            Controls.Remove(ScoreLabel);

                            ScoreLabel.Text = Score.ToString();
                            ScoreLabel.AutoSize = true;
                            ScoreLabel.Location = new Point(900, 0);
                            ScoreLabel.Font = new Font("Arial", 14);
                            ScoreLabel.TabIndex = 0;
                            ScoreLabel.BackColor = SystemColors.Highlight;

                            Controls.Add(ScoreLabel);

                            Controls.Add(associatedTextBox);
                        }
                    }
                }
            }
        }

    }
}
