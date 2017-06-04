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
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace hangman
{
    public partial class Form2 : Form
    {
        private string message;
        private string sessionId;
        private int numberOfWordsToGuess;
        private int numberOfGuessAllowedForEachWord;

        private string word;
        private int totalWordCount;
        private int wrongGuessCountOfCurrentWord;

        private string usedWordEachWord;

        private int correctWordCount;
        private int totalWrongGuessCount;
        private int score;

        private string playerIDRecord;
        private string datetimeRecord;
        private int scoreRecord;

        private List<string> dictionary = new List<string>();

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string m,string s,int nowtg,int nogafew)
        {
            InitializeComponent();

            message = m;
            sessionId = s;
            numberOfWordsToGuess = nowtg;
            numberOfGuessAllowedForEachWord = nogafew;

            var request = WebRequest.CreateHttp("https://strikingly-hangman.herokuapp.com/game/on");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(new
                {
                    sessionId = this.sessionId,
                    action = "nextWord"
                });

                streamWriter.Write(jsonString);
            }

            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
                JObject jo = JObject.Parse(result);

                sessionId = jo["sessionId"].ToString();
                word = jo["data"]["word"].ToString();
                totalWordCount = Convert.ToInt32(jo["data"]["totalWordCount"]);
                wrongGuessCountOfCurrentWord = Convert.ToInt32(jo["data"]["wrongGuessCountOfCurrentWord"]);
            }

            wordLabel.Text = word;
            lengthLabel.Text = word.Length.ToString();
            triesLabel.Text = (10 - wrongGuessCountOfCurrentWord).ToString();
            nowtgLabel.Text = (80 - totalWordCount).ToString();
        }

        private void GuessButton_Click(object sender, EventArgs e)
        {
            if (!(System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[A-Z]$")))
            {
                MessageBox.Show("Oops!" + "\n" + "Only capital letter can be accepted.");
                return;
            }

            var request = WebRequest.CreateHttp("https://strikingly-hangman.herokuapp.com/game/on");
            request.Method = "POST";
            request.ContentType = "application/json";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string jsonString = new JavaScriptSerializer().Serialize(new
                    {
                        sessionId = this.sessionId,
                        action = "guessWord",
                        guess = textBox1.Text
                    });

                    streamWriter.Write(jsonString);
                }

                var response = request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    JObject jo = JObject.Parse(result);

                    sessionId = jo["sessionId"].ToString();
                    word = jo["data"]["word"].ToString();
                    totalWordCount = Convert.ToInt32(jo["data"]["totalWordCount"]);
                    wrongGuessCountOfCurrentWord = Convert.ToInt32(jo["data"]["wrongGuessCountOfCurrentWord"]);
                }
            }
            catch(WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
            }


            wordLabel.Text = word;

            usedWordEachWord += textBox1.Text.ToString();
            usedLabel.Text = usedWordEachWord;
            lengthLabel.Text = word.Length.ToString();
            triesLabel.Text = (10 - wrongGuessCountOfCurrentWord).ToString();

            var request2 = WebRequest.CreateHttp("https://strikingly-hangman.herokuapp.com/game/on");
            request2.Method = "POST";
            request2.ContentType = "application/json";

            try
            {
                using (var streamWriter = new StreamWriter(request2.GetRequestStream()))
                {
                    string jsonString = new JavaScriptSerializer().Serialize(new
                    {
                        sessionId = this.sessionId,
                        action = "getResult"
                    });

                    streamWriter.Write(jsonString);
                }

                var response2 = request2.GetResponse();
                using (var sr = new StreamReader(response2.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    JObject jo = JObject.Parse(result);

                    sessionId = jo["sessionId"].ToString();
                    totalWordCount = Convert.ToInt32(jo["data"]["totalWordCount"]);
                    correctWordCount = Convert.ToInt32(jo["data"]["correctWordCount"]);
                    totalWrongGuessCount = Convert.ToInt32(jo["data"]["totalWrongGuessCount"]);
                    score = Convert.ToInt32(jo["data"]["score"]);
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
            }

            scoreLabel.Text = score.ToString();
        }

        private void gmawButton_Click(object sender, EventArgs e)
        {
            var request = WebRequest.CreateHttp("https://strikingly-hangman.herokuapp.com/game/on");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(new
                {
                    sessionId = this.sessionId,
                    action = "nextWord"
                });

                streamWriter.Write(jsonString);
            }

            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
                JObject jo = JObject.Parse(result);

                sessionId = jo["sessionId"].ToString();
                word = jo["data"]["word"].ToString();
                totalWordCount = Convert.ToInt32(jo["data"]["totalWordCount"]);
                wrongGuessCountOfCurrentWord = Convert.ToInt32(jo["data"]["wrongGuessCountOfCurrentWord"]);
            }

            wordLabel.Text = word;

            nowtgLabel.Text = (80 - totalWordCount).ToString();
            lengthLabel.Text = word.Length.ToString();
            triesLabel.Text = (10 - wrongGuessCountOfCurrentWord).ToString();
            usedWordEachWord = string.Empty;
            usedLabel.Text = usedWordEachWord;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            var request = WebRequest.CreateHttp("https://strikingly-hangman.herokuapp.com/game/on");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonString = new JavaScriptSerializer().Serialize(new
                {
                    sessionId = this.sessionId,
                    action = "submitResult"
                });

                streamWriter.Write(jsonString);
            }

            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
                JObject jo = JObject.Parse(result);

                sessionId = jo["sessionId"].ToString();
                playerIDRecord=jo["data"]["playerId"].ToString();
                datetimeRecord=jo["data"]["datetime"].ToString();
                scoreRecord = Convert.ToInt32(jo["data"]["score"]) ;
            }

            playIDrecordLabel.Text = playerIDRecord;
            datetimerecordLabel.Text = datetimeRecord;
            scorerecordLabel.Text = scoreRecord.ToString();

            MessageBox.Show("Submit successed.");
        }

        private void aiguessButton_Click(object sender, EventArgs e)
        {

            string currentLine;

            //letter frequency distribution
            string[] optimalCallingOrder = new string[]
            {
                "AI",
                "AOEIUMBH",
                "AEOIUYHBCK",
                "AEOIUYSBF",
                "SEAOIUYH",
                "EAIOUSY",
                "EIAOUS",
                "EIAOU",
                "EIAOU",
                "EIOAU",
                "EIOAD",
                "EIOAF",
                "IEOA",
                "IEO",
                "IEA",
                "IEH",
                "IER",
                "IEA",
                "IEA",
                "IE"
            };

            using (StreamReader reader=new StreamReader(@"words.txt"))
            {
                while((currentLine=reader.ReadLine())!=null)
                {
                    dictionary.Add(currentLine.ToUpper());
                }
            }

            dictionary.RemoveAll(a => (a.Length != word.Length));

            switch (word.Length)
            {
                case 1:
                    for(int a = 0;a <= 1;a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length-1].ElementAt(a).ToString();
                        GuessButton_Click(sender,e);
                        if (word.Contains(optimalCallingOrder[word.Length-1].ElementAt(a).ToString()))
                        {
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(textBox1.Text));
                        }
                    }
                    break;
                case 2:
                    for(int a=0;a<=7;a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length-1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length-1].ElementAt(a).ToString()))
                        {
                            int index=word.IndexOf(optimalCallingOrder[word.Length-1].ElementAt(a));
                            dictionary.RemoveAll(aString=>(aString[index]!= optimalCallingOrder[word.Length-1].ElementAt(a)));
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length-1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 3:
                    for (int a = 0; a <= 9; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 4:
                    for (int a = 0; a <= 8; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for(int aa=0;aa<word.Length;aa++)
                                {
                                    if(aa==index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for(int aa=0;aa<word.Length;aa++)
                                {
                                    if(indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 5:
                    for (int a = 0; a <= 7; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 6:
                    for (int a = 0; a <= 6; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 7:
                    for (int a = 0; a <= 5; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 8|9:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 10:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 11:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 12:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 13:
                    for (int a = 0; a <= 3; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 14:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 15:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 16:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 17:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 18:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 19:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 20:
                    for (int a = 0; a <= 1; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length - 1].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length - 1].ElementAt(a));
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length - 1].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                    else
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] == optimalCallingOrder[word.Length - 1].ElementAt(a)));
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length - 1].ElementAt(a).ToString()));
                        }
                    }
                    break;
            }

            List<char> filterChar = new List<char>();
            if(textBox1.Text!="")
            {
                filterChar.Add(textBox1.Text.First());
            }

            while (wrongGuessCountOfCurrentWord<10)
            {
                if(word.Contains("*"))
                {
                    while (dictionary.Count!=0)
                    {
                        Dictionary<char, int> dic = new Dictionary<char, int>();
                        for (int a = 0; a < dictionary.Count; a++)
                        {
                            foreach (char c in dictionary[a])
                            {
                                if(!(filterChar.Contains(c)))
                                {
                                    if (dic.Keys.Contains(c))
                                    {
                                        dic[c]++;
                                    }
                                    else
                                    {
                                        dic.Add(c, 1);
                                    }
                                }
                            }
                        }
                        char nextChar = dic.OrderByDescending(x => x.Value).Select(x => x.Key.ToString()).First().First();
                        filterChar.Add(nextChar);
                        textBox1.Text = nextChar.ToString();
                        GuessButton_Click(sender, e);

                        if (word.Contains(nextChar))
                        {
                            if (word.IndexOf(nextChar) == word.LastIndexOf(nextChar))
                            {
                                int index = word.IndexOf(nextChar);
                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (aa == index)
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != nextChar));
                                    }
                                    else
                                    {
                                        if(word[aa].ToString()=="*")
                                        {
                                            dictionary.RemoveAll(aString => (aString[aa] == nextChar));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == nextChar)
                                                    .Select(x => x.inDex)
                                                    .ToArray();

                                for (int aa = 0; aa < word.Length; aa++)
                                {
                                    if (indexes.Contains(aa))
                                    {
                                        dictionary.RemoveAll(aString => (aString[aa] != nextChar));
                                    }
                                    else
                                    {
                                        if (word[aa].ToString() == "*")
                                        {
                                            dictionary.RemoveAll(aString => (aString[aa] == nextChar));
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(nextChar));
                        }
                    }
                    if(dictionary.Count==0)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string currentLine;

            //letter frequency distribution
            string[] optimalCallingOrder = new string[]
            {
                "AI",
                "AOEIUMBH",
                "AEOIUYHBCK",
                "AEOIUYSBF",
                "SEAOIUYH",
                "EAIOUSY",
                "EIAOUS",
                "EIAOU",
                "EIAOU",
                "EIOAU",
                "EIOAD",
                "EIOAF",
                "IEOA",
                "IEO",
                "IEA",
                "IEH",
                "IER",
                "IEA",
                "IEA",
                "IE"
            };

            using (StreamReader reader = new StreamReader(@"words.txt"))
            {
                while ((currentLine = reader.ReadLine()) != null)
                {
                    dictionary.Add(currentLine);
                }
            }

            dictionary.RemoveAll(a => (a.Length != word.Length));

            switch (word.Length)
            {
                case 1:
                    for (int a = 0; a <= 1; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(textBox1.Text));
                        }
                    }
                    break;
                case 2:
                    for (int a = 0; a <= 7; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                            dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 3:
                    for (int a = 0; a <= 9; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 4:
                    for (int a = 0; a <= 8; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 5:
                    for (int a = 0; a <= 7; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 6:
                    for (int a = 0; a <= 6; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 7:
                    for (int a = 0; a <= 5; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 8 | 9:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 10:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 11:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 12:
                    for (int a = 0; a <= 4; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 13:
                    for (int a = 0; a <= 3; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 14:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 15:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 16:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 17:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 18:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 19:
                    for (int a = 0; a <= 2; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
                case 20:
                    for (int a = 0; a <= 1; a++)
                    {
                        textBox1.Text = optimalCallingOrder[word.Length].ElementAt(a).ToString();
                        GuessButton_Click(sender, e);
                        if (word.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()))
                        {
                            if (word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a)) == word.LastIndexOf(optimalCallingOrder[word.Length].ElementAt(a)))
                            {
                                int index = word.IndexOf(optimalCallingOrder[word.Length].ElementAt(a));
                                dictionary.RemoveAll(aString => (aString[index] != optimalCallingOrder[word.Length].ElementAt(a)));
                            }
                            else
                            {
                                int[] indexes = word.Select((item, inDex) => new { item, inDex })
                                                    .Where(x => x.item == optimalCallingOrder[word.Length].ElementAt(a))
                                                    .Select(x => x.inDex)
                                                    .ToArray();
                                for (int b = 0; b < indexes.Length; b++)
                                {
                                    dictionary.RemoveAll(aString => (aString[indexes[b]] != optimalCallingOrder[word.Length].ElementAt(a)));
                                }
                            }
                            break;
                        }
                        else
                        {
                            dictionary.RemoveAll(aString => aString.Contains(optimalCallingOrder[word.Length].ElementAt(a).ToString()));
                        }
                    }
                    break;
            }
        }
    }
}
