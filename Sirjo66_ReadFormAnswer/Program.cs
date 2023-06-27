using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sirjo66_ReadFormAnswer
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            Uri uri = new Uri("https://www.ilportaledellautomobilista.it");
            string formUrl = "/web/portale-automobilista/verifica-foto-patente-professionista";

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions());
            var page = await browser.NewPageAsync();

            // Navigate to url with the form
            await page.GoToAsync(uri.AbsoluteUri + formUrl);

            // Upload the image
            var fileInput = await page.WaitForSelectorAsync("#VerificaImmagineIcao_upload");
            await fileInput.UploadFileAsync("path_to_your_photo\\foto.jpg");

            // this will clicks for you, it pretends to be a human
            await page.ClickAsync("#VerificaImmagineIcao_button_value_verificaImmagine");

            // Wait for the message
            // edit the id of div where you expect the info message
            var message = await page.WaitForSelectorAsync("#uploadSuccess");

            // print it to console or wheherever you want
            Console.WriteLine(await page.EvaluateFunctionAsync<string>("el => el.innerText", message));

            // dont forget to close the page 
            await browser.CloseAsync();
        }
    }
}
