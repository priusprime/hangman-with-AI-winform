using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace hangman
{
    public partial class Form1 : Form
    {
        private string message;
        private string sessionId;
        private int numberOfWordsToGuess;
        private int numberOfGuessAllowedForEachWord;

        public Form1()
        {
            InitializeComponent();
        }

            private void Button1_Click(object sender, EventArgs e)
        {
            var request = WebRequest.CreateHttp("https://strikingly-hangman.herokuapp.com/game/on");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(new
                {
                    playerId="Bruce.fan19910501@hotmail.com",
                    action="startGame"
                });

                streamWriter.Write(jsonString);
            }

            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
                JObject jo = JObject.Parse(result);
                message = jo["message"].ToString();
                sessionId = jo["sessionId"].ToString();
                numberOfWordsToGuess = Convert.ToInt32(jo["data"]["numberOfWordsToGuess"]);
                numberOfGuessAllowedForEachWord = Convert.ToInt32(jo["data"]["numberOfGuessAllowedForEachWord"]);
            }

            Form2 newForm2 = new Form2(message, sessionId, numberOfWordsToGuess, numberOfGuessAllowedForEachWord);
            newForm2.Owner = this;
            this.Hide();
            newForm2.ShowDialog();
            Application.ExitThread();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
