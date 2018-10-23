using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomsoftZadatak.Models;

namespace TomsoftZadatak.ViewModels
{
    class StatementItemViewModel : Screen
    {
        private BindableCollection<StatementItem> statements;

        public BindableCollection<StatementItem> Statements
        {
            get { return statements; }
            set
            {
                statements = value;
                NotifyOfPropertyChange(() => Statements);
            }
        }

        public StatementItemViewModel(BindableCollection<StatementItem> statementsList)
        {
            Statements = statementsList;
        }
    }
}
