using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleApp1
{
    public class APIRequest
    {

        public static HttpResponseMessage PostData(string uri, string json, Dictionary<string, string> headers)
        {
            HttpResponseMessage response = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    foreach (var s in headers)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(s.Key, s.Value);
                    }
                    
                    response = client.PostAsJsonAsync(uri, json).Result;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

    }
}
