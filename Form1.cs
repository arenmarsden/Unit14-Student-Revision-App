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

        /// <summary>
        /// Randomly generate the equations in a range from 1 to 12 and generate the labels and text boxes accordingly.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Generate score labels
            ScoreLabel.Text = "0";
            ScoreLabel.AutoSize = true;
            ScoreLabel.Location = new Point(900, 0);
            ScoreLabel.Font = new Font("Arial", 14);
            ScoreLabel.TabIndex = 0;
            ScoreLabel.BackColor = SystemColors.Highlight;

            Controls.Add(ScoreLabel);
            
            // Ensure randomness in questions
            var random = new Random();
            int questionNum = 0;

            // Constant initialisation points for X and Y positions.
            const int pointX = 30;
            const int pointY = 40;

            var prevPointY = 0;

            // Generate 10 questions for the student to answer.
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

        /// <summary>
        /// Check the answers given compared to the actual answers based off the labels.
        /// </summary>
        private void checkButton_Click(object sender, EventArgs e)
        {
            // Dictionaries to contain the ccorrect answers and given asnwers.
            Dictionary<Label, int> correctAnswers = new();
            var givenAnswers = new Dictionary<Label, int>();

            // Iterate over the values for the labels and textboxes. The value is the text box and the key is the question.
            foreach (var pair in _keyValuePairs)
            {
                // Check if any of the value is null or empty, if so, show a popup.
                if (pair.Value == null || pair.Value.Text == null || pair.Value.Text.Length == 0)
                {
                    MessageBox.Show("You are missing text in one or more boxes!");
                    break;
                }

                // Ensure that the entered input into the textbox is an integer, and nothing else.
                if (int.TryParse(pair.Value.Text, out int num))
                {
                    var label = pair.Key;

                    // Get the two numbers making up the equation.
                    string[] numberArray = label.Text.Split('*');

                    // Ensure nothing messed up...
                    if (numberArray.Length > 0)
                    {
                        // Parse the split numbers into arrays.
                        int num1 = int.Parse(numberArray[0]);
                        int num2 = int.Parse(numberArray[1]);

                        // Add the answers and given answers to the dictionary.
                        correctAnswers.Add(label, num1 * num2);
                        givenAnswers.Add(label, num);
                    }
                } else
                {
                    // Show a popup warning to only enter ints.
                    MessageBox.Show("Please only enter integers!");
                    return;
                }
            }

            // Iterate over the given answers and correct answers and compare them.
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
