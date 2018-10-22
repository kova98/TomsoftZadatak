using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TomsoftZadatak.Models;

namespace TomsoftZadatak.ViewModels
{
    class ShellViewModel : Screen
    {
        private static HttpClient client = new HttpClient();
        private BindableCollection<Artikl> artikli;
        private string naziv;
        private string statusMessage;

        public ShellViewModel()
        {
            Artikli = new BindableCollection<Artikl>();
        }


        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                statusMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);
            }
        }


        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                NotifyOfPropertyChange(() => Naziv);
            }
        }

        public BindableCollection<Artikl> Artikli
        {
            get { return artikli; }
            set
            {
                artikli = value;
                NotifyOfPropertyChange(() => Artikli);
            }
        }

        public async void GetArtikli()
        {
            var url = "http://apidemo.luceed.hr/datasnap/rest/artikli/naziv/" + Naziv;
            var username = "luceed_mb";
            var password = "7e5y2Uza";

            StatusMessage = "Dohvaćam...";

            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);

            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            var resultString = await response.Content.ReadAsStringAsync();

            var resultDeserialized = JsonConvert.DeserializeObject<dynamic>(resultString);

            var resultArtiklArray = resultDeserialized["result"][0]["artikli"];
            var resultArtiklArraySerialized = JsonConvert.SerializeObject(resultArtiklArray);

            var artiklList = JsonConvert.DeserializeObject<BindableCollection<Artikl>>(resultArtiklArraySerialized);

            Artikli = artiklList;

            StatusMessage = "";
        }

    }
}
