using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PasswordManagerMaui.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PasswordManagerMaui
{
    public static class Common
    {
        public static List<Password> GetPasswordData(HttpResponseMessage response)
        {
            List<Password> passwords=new List<Password>();
            if (response.IsSuccessStatusCode)
            {
                var contentStream = response.Content.ReadAsStreamAsync().Result;

                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);

                JsonSerializer serializer = new JsonSerializer();
                try
                {
                    passwords = serializer.Deserialize<List<Password>>(jsonReader);
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }
            return passwords;
        }

        public static JwtSecurityToken GetToken(HttpResponseMessage response)
        {
           JwtSecurityToken token = new JwtSecurityToken();
            if (response.IsSuccessStatusCode)
            {
                var contentStream = response.Content.ReadAsStreamAsync().Result;

                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);

                JsonSerializer serializer = new JsonSerializer();
                try
                {
                    token= serializer.Deserialize<JwtSecurityToken>(jsonReader);
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }
            return token;
        }
    }
}
