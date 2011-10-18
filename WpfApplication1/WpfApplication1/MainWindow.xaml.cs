using System;
using System.Linq;
using UriAgassi.Isobars;
using UriAgassi.Isobars.Algo;
using System.Collections.Generic;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var mvm = new MainWindowViewModel
            {
                RawData = UriAgassi.Isobars.Properties.Resources.Data.Split('\n').Select(x => x.Trim().Split(',').Select(Convert.ToDouble).ToArray()).ToArray()
            };
            IEnumerable<IsobarPoint>[][] hgrid, vgrid;
            mvm.Isobars = Isobar.CreateIsobars(mvm.RawData, out hgrid, out vgrid).ToArray();
            mvm.VGrid = vgrid;
            mvm.HGrid = hgrid;
            DataContext = mvm;
        }
    }
}
