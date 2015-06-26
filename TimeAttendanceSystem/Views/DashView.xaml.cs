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
            var intime = new Telerik.Windows.Controls.ChartView.DoughnutSeries();
            var series = new Telerik.Windows.Controls.ChartView.SplineSeries();
            series.StrokeThickness = 4;
            var tbiTemplate = this.Resources["theTemplate"] as DataTemplate;
            series.TrackBallInfoTemplate = tbiTemplate;
            var seriesEI = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            var seriesEO = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            var seriesLI = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            var seriesLO = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            for (int i = 0; i < data.Count; i++)
            {
                series.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].PresentEmps });
                seriesEI.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].EIEmps });
                seriesEO.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].EOEmps });
                seriesLI.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LIEmps});
                seriesLO.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LOEmps });
            }
            series.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Time" };
            series.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Present" };
           
            this.xCartesianGraphPresence.Series.Add(series);
            var Tei = this.Resources["TemplateEarlyIn"] as DataTemplate;
            seriesEI.TrackBallInfoTemplate = Tei;
            var Teo = this.Resources["TemplateEarlyOut"] as DataTemplate;
            seriesEO.TrackBallInfoTemplate = Teo;
            var Tli = this.Resources["TemplateLateIn"] as DataTemplate;
            seriesLI.TrackBallInfoTemplate = Tli;
            var Tlo = this.Resources["TemplateLateOut"] as DataTemplate;
            seriesLO.TrackBallInfoTemplate = Tlo;
            this.Departments.Series.Add(seriesEI);
            this.Departments.Series.Add(seriesEO);
            this.Departments.Series.Add(seriesLI);
            this.Departments.Series.Add(seriesLO);
            this.intimeanalysis.Series[0].ItemsSource = vmdash.Value;
 
             
           
        }
    }
    }

