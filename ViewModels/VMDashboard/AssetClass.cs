using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TimeAttendanceSystem.Model;
namespace WPFPieChart
{
    public class AssetClass : INotifyPropertyChanged
    {
        private String myClass;
        
        public String Class
        {
            get { return myClass; }
            set {
                myClass = value;
                RaisePropertyChangeEvent("Class");
            }
        }

        private double fund;

        public double Fund
        {
            get { return fund; }
            set {
                fund = value;
                RaisePropertyChangeEvent("Fund");
            }
        }

        private double total;

        public double Total
        {
            get { return total; }
            set {
                total = value;
                RaisePropertyChangeEvent("Total");
            }
        }

        private double benchmark;

        public double Benchmark
        {
            get { return benchmark; }
            set {
                benchmark = value;
                RaisePropertyChangeEvent("Benchmark");
            }
        }



        public static List<AssetClass> ConstructTestData(TAS2013Entities context)
        {
            List<AssetClass> assetClasses = new List<AssetClass>();
            List<DailyTimeMonitor> d = context.DailyTimeMonitors.ToList();
            Console.WriteLine((Double)d[d.Count - 1].InLabelOneValue);

            Console.WriteLine((Double)d[d.Count - 1].InLabelTwoValue);

            Console.WriteLine((Double)d[d.Count - 1].InLabelThreeValue);

            Console.WriteLine((Double)d[d.Count - 1].InLabelOneValue);
            assetClasses.Add(new AssetClass() { Class = d[d.Count - 1].InLabelOne, Fund = (Double)d[d.Count - 1].InLabelOneValue, Total = (Double)d[d.Count - 1].InLabelOneValue, Benchmark = 445 });
            assetClasses.Add(new AssetClass() { Class = d[d.Count - 1].InLabelTwo, Fund = (Double)d[d.Count - 1].InLabelTwoValue, Total = (Double)d[d.Count - 1].InLabelTwoValue, Benchmark = 4.82 });
            assetClasses.Add(new AssetClass() { Class = d[d.Count - 1].InLabelThree, Fund = (Double)d[d.Count - 1].InLabelThreeValue, Total = (Double)d[d.Count - 1].InLabelThreeValue, Benchmark = 4.82 });
            assetClasses.Add(new AssetClass() { Class = d[d.Count - 1].InLabelFour, Fund = (Double)d[d.Count - 1].InLabeFourValue, Total = (Double)d[d.Count - 1].InLabeFourValue, Benchmark = 4.82 });
         
           
            return assetClasses;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangeEvent(String propertyName)
        {
            if (PropertyChanged!=null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            
        }

        #endregion
    }
}
