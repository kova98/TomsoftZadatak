using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomsoftZadatak.Models;

namespace TomsoftZadatak.ViewModels
{
    class ItemViewModel : Screen
    {
        private BindableCollection<Item> items;

        public ItemViewModel(BindableCollection<Item> itemsList)
        {
            Items = itemsList;
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
    }
}
