using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using DocumentFormat.OpenXml.Drawing;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class OpenAIService : IOpenAIService
    {

        OpenAIClient _client;
        public OpenAIService(OpenAIClient client)
        {
            _client = client;
        }
        public async Task<DefaultReturn<string>> SendPromtAsync(AIPostRequest promt)
        {
            DefaultReturn<string> defaultReturn = new DefaultReturn<string>();
            var chat = _client.GetChatClient("gpt-5-mini");
            var messages = new List<ChatMessage>
                {
                    ChatMessage.CreateSystemMessage("You are a helpful assistant."),
                    ChatMessage.CreateUserMessage(promt.Promt)
                };
            var response = await chat.CompleteChatAsync(messages);

            defaultReturn.Data = response.Value.Content[0].Text;


            return defaultReturn;
        }
    }
}
