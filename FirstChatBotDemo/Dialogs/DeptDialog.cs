using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace FirstChatBotDemo.Dialogs
{
    [Serializable]
    public class DeptDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Borcunuzu ogrenmek için lutfen TC 353554654 seklinde nonunuzu yazınız?");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var act = await result as Activity;
            var message = act.Text.ToLower();
            
            if (message.Contains("tc "))
            {
                var idText = message.Substring(message.IndexOf("tc "), 14);
                if (string.IsNullOrEmpty(idText))
                {
                    await context.PostAsync("Size yardımcı olabilmem için bilgilerinizi dogru giriniz?");
                }
                context.UserData.SetValue<string>("Id", idText);
                await context.PostAsync(GetDept(context.UserData.GetValue<string>("Id")));
            }

            context.Done(message);
        }

        private static string GetDept(string idNumber)
        {
            return string.Format("{0} Borcunuz 100 TLdir.", idNumber);
        }

    }
}