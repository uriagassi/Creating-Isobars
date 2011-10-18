using System.ComponentModel;
using UriAgassi.Isobars.Algo;
using System.Collections.Generic;

namespace UriAgassi.Isobars
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private double[][] _RawData;
        public double[][] RawData
        {
            get { return _RawData; }
            set
            {
                _RawData = value;
                OnPropertyChanged("RawData");
            }
        }

        private IEnumerable<IsobarPoint>[][] _HGrid, _VGrid;

        public IEnumerable<IsobarPoint>[][] HGrid
        {
            get { return _HGrid; }
            set
            {
                _HGrid = value;
                OnPropertyChanged("HGrid");
            }
        }

        public IEnumerable<IsobarPoint>[][] VGrid
        {
            get { return _VGrid; }
            set
            {
                _VGrid = value;
                OnPropertyChanged("VGrid");
            }
        }

        private Isobar[] _Isobars;

        public Isobar[] Isobars
        {
            get { return _Isobars; }
            set
            {
                _Isobars = value;
                OnPropertyChanged("Isobars");
            }
        }
        protected void OnPropertyChanged(string fieldName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(fieldName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
