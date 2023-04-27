using Domain.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChatGPT.Helper
{
    public static class ChatGPTHelper
    {
        /// <summary>
        /// Pega url base da API
        /// </summary>
        /// <returns></returns>
        public static string GetBaseUrl()
        {
            return ConfigHelper.Get<string>("ChatGPT:BaseUrl");
        }

        /// <summary>
        /// Monta o cabeçalho com token de autenticação
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeader()
        {
            var header = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {GetApiKey()}" }
            };
            return header;
        }

        /// <summary>
        /// Obtém o token de autenticação
        /// </summary>
        /// <returns></returns>
        public static string GetApiKey()
        {
            var accessToken = ConfigHelper.Get<string>("ChatGPT:ApiKey");
            return accessToken;
        }

        /// <summary>
        /// Pega a configuração de serialização da requisição
        /// </summary>
        /// <returns></returns>
        public static JsonMediaTypeFormatter GetJsonMediaTypeFormatter()
        {
            var formatter = new JsonMediaTypeFormatter()
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    //Formatting = Formatting.None,
                    //NullValueHandling = NullValueHandling.Ignore,
                    //ObjectCreationHandling = ObjectCreationHandling.Replace
                }
            };
            formatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return formatter;
        }
    }
}
