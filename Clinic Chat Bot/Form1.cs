using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClinicChatbot
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string openAiApiKey = "YOUR_OPENAI_API_KEY_HERE"; // Replace with your actual API key

        public MainWindow()
        {
            InitializeComponent();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiApiKey);
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                string userInput = InputTextBox.Text;
                ChatListView.Items.Add("You: " + userInput);
                GetAiResponse(userInput);
                InputTextBox.Clear();
            }
        }

        private async void GetAiResponse(string input)
        {
            try
            {
                var response = await RequestAiResponse(input);
                ChatListView.Items.Add("Bot: " + response);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error contacting AI service: " + ex.Message);
            }
        }

        private static async Task<string> RequestAiResponse(string input)
        {
            var requestBody = new { prompt = input, max_tokens = 100 };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(responseBody);
            return jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("text").GetString();
        }
    }
}
