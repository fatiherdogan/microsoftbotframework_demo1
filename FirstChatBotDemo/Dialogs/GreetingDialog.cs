
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FirstChatBotDemo.Dialogs
{
    #region Step 2

    [Serializable]
    public class GreetingDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var act = await result as Activity;
            var message = act.Text.ToLower();

            if (message.Contains("merhaba") || message.Contains("mrb") || message.Contains("selam") || message.Contains("slm"))
            {
                await context.PostAsync("Ben KocBot. Nasıl yardımcı olabilirim?");
            }
            else if (message.Contains("borc") || message.Contains("borç") || message.Contains("vade"))
            {
                context.Call<object>(new DeptDialog(), DialogDone);
            }
            else if (message.Contains("rezervasyon"))
            {
                context.Call<object>(new ReservationDialog(), DialogDone);
            }
        }

        private async Task DialogDone(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }
    }
    #endregion
}