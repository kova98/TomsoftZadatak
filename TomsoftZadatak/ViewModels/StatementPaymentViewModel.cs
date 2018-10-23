using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomsoftZadatak.Models;

namespace TomsoftZadatak.ViewModels
{
    class StatementPaymentViewModel : Screen
    {
        private BindableCollection<StatementPayment> statements;

        public BindableCollection<StatementPayment> Statements
        {
            get { return statements; }
            set
            {
                statements = value;
                NotifyOfPropertyChange(() => Statements);
            }
        }

        public StatementPaymentViewModel(BindableCollection<StatementPayment> statementsList)
        {
            Statements = statementsList;
        }
    }
}
