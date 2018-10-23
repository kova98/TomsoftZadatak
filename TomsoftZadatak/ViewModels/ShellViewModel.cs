using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TomsoftZadatak.Models;

namespace TomsoftZadatak.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        private static HttpClientHandler handler = new HttpClientHandler();
        private static HttpClient client = new HttpClient(handler);

        private BindableCollection<Item> items;
        private string naziv;
        private string statusMessage;
        private DateTime dateStart;
        private DateTime dateEnd;

        private string username = "luceed_mb";
        private string password = "7e5y2Uza";

        public ShellViewModel()
        {
            Items = new BindableCollection<Item>();
            DateStart = DateEnd = DateTime.Now;
        }

        #region Public Properties

        public DateTime DateStart
        {
            get { return dateStart; }
            set
            {
                dateStart = value;
                NotifyOfPropertyChange(() => DateStart);
            }
        }

        public DateTime DateEnd
        {
            get { return dateEnd; }
            set
            {
                dateEnd = value;
                NotifyOfPropertyChange(() => DateEnd);
            }
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

        public BindableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        #endregion

        // 1. Zadatak
        public async void GetItemsByName()
        {
            var url = "http://apidemo.luceed.hr/datasnap/rest/artikli/naziv/" + Naziv;

            StatusMessage = "Dohvaćam...";

            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            var resultString = await response.Content.ReadAsStringAsync();

            var resultDeserialized = JsonConvert.DeserializeObject<dynamic>(resultString);

            var resultItemArray = resultDeserialized["result"][0]["artikli"];
            var resultItemArraySerialized = JsonConvert.SerializeObject(resultItemArray);

            var itemList = JsonConvert.DeserializeObject<BindableCollection<Item>>(resultItemArraySerialized);

            ActivateItem(new ItemViewModel((BindableCollection<Item>)itemList));

            StatusMessage = "";
        }

        // 2. Zadatak
        public async void GetStatementByPaymentType()
        {
            string baseUrl = "http://apidemo.luceed.hr/datasnap/rest/mpobracun/placanja/4986-1";

            string start = GetDateStringFromDateTime(DateStart);
            string end = GetDateStringFromDateTime(dateEnd);
            string url = $"{baseUrl}/{start}/{end}";

            StatusMessage = "Dohvaćam...";

            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            var resultString = await response.Content.ReadAsStringAsync();

            var resultDeserialized = JsonConvert.DeserializeObject<dynamic>(resultString);

            var resultStatementArray = resultDeserialized["result"][0]["obracun_placanja"];
            var resultStatementArraySerialized = JsonConvert.SerializeObject(resultStatementArray);

            var resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<BindableCollection<StatementPayment>>(resultStatementArraySerialized);

            base.ActivateItem(new StatementPaymentViewModel((BindableCollection<StatementPayment>)resultList));

            StatusMessage = "";
        }

        //3. Zadatak
        public async void GetStatementByItem()
        {
            string baseUrl = "http://apidemo.luceed.hr/datasnap/rest/mpobracun/artikli/4986-1";

            string start = GetDateStringFromDateTime(DateStart);
            string end = GetDateStringFromDateTime(dateEnd);
            string url = $"{baseUrl}/{start}/{end}";

            StatusMessage = "Dohvaćam...";

            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            var resultString = await response.Content.ReadAsStringAsync();

            var resultDeserialized = JsonConvert.DeserializeObject<dynamic>(resultString);

            var resultStatementArray = resultDeserialized["result"][0]["obracun_artikli"];
            var resultStatementArraySerialized = JsonConvert.SerializeObject(resultStatementArray);

            var resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<BindableCollection<StatementItem>>(resultStatementArraySerialized);

            base.ActivateItem(new StatementItemViewModel((BindableCollection<StatementItem>)resultList));

            StatusMessage = "";
        }

        private string GetDateStringFromDateTime(DateTime time)
        {
            return $"{time.Day}.{time.Month}.{time.Year}";
        }

    }
}
