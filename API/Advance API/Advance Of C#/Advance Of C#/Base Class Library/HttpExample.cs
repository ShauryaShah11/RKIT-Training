using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Advance_Of_C_.Base_Class_Library
{
    class HttpExample
    {
        public async Task SendHttpRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://api.github.com");
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("HTTP Response Content: " + content);
            }
        }
    }
}
