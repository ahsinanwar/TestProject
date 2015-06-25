using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.Charting;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDashboard;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for DashView.xaml
    /// </summary>
    public partial class DashView : UserControl
    {
        VMDashboard vmdash;
        public DashView()
        {
            InitializeComponent();
            vmdash = new VMDashboard();
            this.SetUpChart(vmdash.presence.ToList());
            this.DataContext=vmdash;
          
           
        }
         private void SetUpChart(List<DailySummary> data)
        {
            var series = new Telerik.Windows.Controls.ChartView.LineSeries();
            var bar = new Telerik.Windows.Controls.ChartView.BarSeries();
            var seriesEI = new Telerik.Windows.Controls.ChartView.LineSeries();
            var seriesEO = new Telerik.Windows.Controls.ChartView.LineSeries();
            var seriesLI = new Telerik.Windows.Controls.ChartView.LineSeries();
            var seriesLO = new Telerik.Windows.Controls.ChartView.LineSeries();
            for (int i = 0; i < data.Count; i++)
            {
                series.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].PresentEmps });
                bar.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].PresentEmps });
                seriesEI.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].EIEmps });
                seriesEO.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].EOEmps });
                seriesLI.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LIEmps});
                seriesLO.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LOEmps });
            }
           
            this.xCartesianGraphPresence.Series.Add(series);
            this.xCartesianGraphPresence.Series.Add(bar);
            this.Departments.Series.Add(seriesEI);
            this.Departments.Series.Add(seriesEO);
            this.Departments.Series.Add(seriesLI);
            this.Departments.Series.Add(seriesLO);
 
             
           
        }
    }
    }

