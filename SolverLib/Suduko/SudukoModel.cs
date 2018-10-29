using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SolverLib.Suduko
{
    public class SudukoModel : ObservableCollection<SudukoRow>, INotifyPropertyChanged
    {
        public SudukoModel()
        {
            Clear();
            Add(new SudukoRow());
            Add(new SudukoRow());
            
           
        }
        public string Name { get; set; }

         public void AddData()
         {
             Add(new SudukoRow());
             NotifyPropertyChanged("new Row");
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

    public class SudukoRow : ObservableCollection<SudukoItem>, INotifyPropertyChanged
    {
        public string Row { get; set; }
        public SudukoRow()
        {
            Add(new SudukoItem(){1,2,3,4,5,6,7,8,9});
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Add(new SudukoItem() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            
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

    public class SudukoItem : List<int>, INotifyPropertyChanged
    {
        public SudukoItem()
        {
            
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
