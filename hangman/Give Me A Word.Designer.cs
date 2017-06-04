namespace hangman
{
    partial class Form2
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
            this.wordLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.guessButton = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.triesLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.usedLabel = new System.Windows.Forms.Label();
            this.nogafewLabel = new System.Windows.Forms.Label();
            this.nowtgLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.gmawButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.playIDrecordLabel = new System.Windows.Forms.Label();
            this.datetimerecordLabel = new System.Windows.Forms.Label();
            this.scorerecordLabel = new System.Windows.Forms.Label();
            this.aiguessButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wordLabel.Location = new System.Drawing.Point(34, 195);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(0, 36);
            this.wordLabel.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(175, 282);
            this.textBox1.MaxLength = 1;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(29, 27);
            this.textBox1.TabIndex = 1;
            // 
            // guessButton
            // 
            this.guessButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guessButton.Location = new System.Drawing.Point(210, 282);
            this.guessButton.Name = "guessButton";
            this.guessButton.Size = new System.Drawing.Size(61, 27);
            this.guessButton.TabIndex = 2;
            this.guessButton.Text = "Guess";
            this.guessButton.UseVisualStyleBackColor = true;
            this.guessButton.Click += new System.EventHandler(this.GuessButton_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(28, 32);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 12);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "tries:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(28, 57);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(47, 12);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "length:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(28, 80);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 12);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "used:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(241, 57);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(131, 12);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "numberOfWordsToGuess:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(241, 32);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(197, 12);
            this.Label4.TabIndex = 7;
            this.Label4.Text = "numberOfGuessAllowedForEachWord:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(241, 80);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(41, 12);
            this.Label6.TabIndex = 8;
            this.Label6.Text = "score:";
            // 
            // triesLabel
            // 
            this.triesLabel.AutoSize = true;
            this.triesLabel.Location = new System.Drawing.Point(92, 32);
            this.triesLabel.Name = "triesLabel";
            this.triesLabel.Size = new System.Drawing.Size(0, 12);
            this.triesLabel.TabIndex = 9;
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(92, 57);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(0, 12);
            this.lengthLabel.TabIndex = 10;
            // 
            // usedLabel
            // 
            this.usedLabel.AutoSize = true;
            this.usedLabel.Location = new System.Drawing.Point(92, 80);
            this.usedLabel.Name = "usedLabel";
            this.usedLabel.Size = new System.Drawing.Size(0, 12);
            this.usedLabel.TabIndex = 11;
            // 
            // nogafewLabel
            // 
            this.nogafewLabel.AutoSize = true;
            this.nogafewLabel.Location = new System.Drawing.Point(444, 32);
            this.nogafewLabel.Name = "nogafewLabel";
            this.nogafewLabel.Size = new System.Drawing.Size(17, 12);
            this.nogafewLabel.TabIndex = 12;
            this.nogafewLabel.Text = "10";
            // 
            // nowtgLabel
            // 
            this.nowtgLabel.AutoSize = true;
            this.nowtgLabel.Location = new System.Drawing.Point(444, 57);
            this.nowtgLabel.Name = "nowtgLabel";
            this.nowtgLabel.Size = new System.Drawing.Size(0, 12);
            this.nowtgLabel.TabIndex = 13;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(444, 80);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(0, 12);
            this.scoreLabel.TabIndex = 14;
            // 
            // gmawButton
            // 
            this.gmawButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gmawButton.Location = new System.Drawing.Point(277, 282);
            this.gmawButton.Name = "gmawButton";
            this.gmawButton.Size = new System.Drawing.Size(101, 27);
            this.gmawButton.TabIndex = 15;
            this.gmawButton.Text = "Give me a word";
            this.gmawButton.UseVisualStyleBackColor = true;
            this.gmawButton.Click += new System.EventHandler(this.gmawButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(466, 311);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(61, 23);
            this.submitButton.TabIndex = 16;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // playIDrecordLabel
            // 
            this.playIDrecordLabel.AutoSize = true;
            this.playIDrecordLabel.Location = new System.Drawing.Point(420, 149);
            this.playIDrecordLabel.Name = "playIDrecordLabel";
            this.playIDrecordLabel.Size = new System.Drawing.Size(0, 12);
            this.playIDrecordLabel.TabIndex = 17;
            // 
            // datetimerecordLabel
            // 
            this.datetimerecordLabel.AutoSize = true;
            this.datetimerecordLabel.Location = new System.Drawing.Point(420, 185);
            this.datetimerecordLabel.Name = "datetimerecordLabel";
            this.datetimerecordLabel.Size = new System.Drawing.Size(0, 12);
            this.datetimerecordLabel.TabIndex = 18;
            // 
            // scorerecordLabel
            // 
            this.scorerecordLabel.AutoSize = true;
            this.scorerecordLabel.Location = new System.Drawing.Point(420, 219);
            this.scorerecordLabel.Name = "scorerecordLabel";
            this.scorerecordLabel.Size = new System.Drawing.Size(0, 12);
            this.scorerecordLabel.TabIndex = 19;
            // 
            // aiguessButton
            // 
            this.aiguessButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiguessButton.Location = new System.Drawing.Point(13, 311);
            this.aiguessButton.Name = "aiguessButton";
            this.aiguessButton.Size = new System.Drawing.Size(144, 23);
            this.aiguessButton.TabIndex = 20;
            this.aiguessButton.Text = "AI guess for a word";
            this.aiguessButton.UseVisualStyleBackColor = true;
            this.aiguessButton.Click += new System.EventHandler(this.aiguessButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 337);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.aiguessButton);
            this.Controls.Add(this.scorerecordLabel);
            this.Controls.Add(this.datetimerecordLabel);
            this.Controls.Add(this.playIDrecordLabel);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.gmawButton);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.nowtgLabel);
            this.Controls.Add(this.nogafewLabel);
            this.Controls.Add(this.usedLabel);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.triesLabel);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.guessButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.wordLabel);
            this.Name = "Form2";
            this.Text = "hangman";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label wordLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button guessButton;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label triesLabel;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label usedLabel;
        private System.Windows.Forms.Label nogafewLabel;
        private System.Windows.Forms.Label nowtgLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Button gmawButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label playIDrecordLabel;
        private System.Windows.Forms.Label datetimerecordLabel;
        private System.Windows.Forms.Label scorerecordLabel;
        private System.Windows.Forms.Button aiguessButton;
        private System.Windows.Forms.Button button1;
    }
}