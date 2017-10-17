using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;


namespace FirstChatBotDemo.Dialogs
{
    [Serializable]
    public class ReservationDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Rezervasyon kaydınız için Isminizi Soyisminizi yazınız?");
            context.Wait(MessageReceivedAsync);
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var act = await result as Activity;
            var message = act.Text.ToLower();           

            context.UserData.SetValue<string>("Name", message);
            var rezervationMessage = string.Format("Sayın {0}, rezervasyon nedeniniz nedir?", message);
            await context.PostAsync(rezervationMessage);
            context.Wait(ReservationReceivedAsync);
        }

        private async Task ReservationReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var act = await result as Activity;
            var message = act.Text.ToLower();
            await context.PostAsync("Rezervasyonunuz alınmıstır. Tesekkurler.");
            context.Done<object>(message);
        }
    }
}
