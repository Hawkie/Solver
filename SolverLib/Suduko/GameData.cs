using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Suduko
{
    public class DataClass : DataTable, INotifyPropertyChanged
    {


        private string name = "column";

        public DataClass()
        {
     
            Columns.Add(new DataColumn("Strike1", typeof(string)));
            Columns.Add(new DataColumn("Strike2", typeof(string)));
            var row = NewRow();
            Rows.Add(row);
            row[0] = "Data1";
            row[1] = "Data2";

        }

        public void AddData()
        {
            name += "s";
            Columns.Add(new DataColumn(name, typeof(string)));

            var row = NewRow();
            Rows.Add(row);
            row[0] = "More Data1";
            AcceptChanges();

          
            NotifyPropertyChanged("AddColumn");
        }




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }

  
}
