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
using Telerik.Windows.Controls;

using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.Charting;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDashboard;
using System.Collections.ObjectModel;
using WPFPieChart;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for DashView.xaml
    /// </summary>
    public partial class DashView : Page
    {
         VMDashboard vmdash;
         private ObservableCollection<AssetClass> classes;
         TAS2013Entities context = new TAS2013Entities();
        

        /// <summary>
        /// Handle clicks on the listview column heading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //A very bad implementation without the DataContext as it was eaten by the AssetClass
        private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.GridViewColumn column = ((GridViewColumnHeader)e.OriginalSource).Column;
            piePlotter.PlottedProperty = column.Header.ToString();
        }
        public DashView()
        {
            InitializeComponent();
            vmdash = new VMDashboard();
            this.SetUpChart(vmdash.presence.ToList());
            classes = new ObservableCollection<AssetClass>(AssetClass.ConstructTestData(context));
            this.DataContext = vmdash;
          
        }
         private void SetUpChart(List<DailySummary> data)
        {   
            //Presence Employee
            var series = new Telerik.Windows.Controls.ChartView.SplineSeries();
            series.StrokeThickness = 4;
            var tbiTemplate = this.Resources["theTemplate"] as DataTemplate;
            series.TrackBallInfoTemplate = tbiTemplate;
            //EI,EO,LI,LO series
            var seriesEI = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            var seriesEO = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            var seriesLI = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
            var seriesLO = new Telerik.Windows.Controls.ChartView.BarSeries() { CombineMode = ChartSeriesCombineMode.Stack };
             //end series
             //ACTUAL, LOSS and targeted hours worked
            var seriesActual = new Telerik.Windows.Controls.ChartView.SplineSeries();
            var seriesLoss = new Telerik.Windows.Controls.ChartView.SplineSeries();
            var seriesTargeted = new Telerik.Windows.Controls.ChartView.SplineSeries();
            for (int i = 0; i < data.Count; i++)
            {
                series.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].PresentEmps });
                seriesEI.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].EIEmps });
                seriesEO.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].EOEmps });
                seriesLI.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LIEmps});
                seriesLO.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LOEmps });
                seriesActual.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].ActualWorkMins });
                seriesLoss.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].LossWorkMins });
                seriesTargeted.DataPoints.Add(new CategoricalDataPoint { Category = data[i].Date, Value = data[i].ExpectedWorkMins});
            }
            
            this.xCartesianGraphPresence.Series.Add(series);
            var Tei = this.Resources["TemplateEarlyIn"] as DataTemplate;
            seriesEI.TrackBallInfoTemplate = Tei;
            var Teo = this.Resources["TemplateEarlyOut"] as DataTemplate;
            seriesEO.TrackBallInfoTemplate = Teo;
            var Tli = this.Resources["TemplateLateIn"] as DataTemplate;
            seriesLI.TrackBallInfoTemplate = Tli;
            var Tlo = this.Resources["TemplateLateOut"] as DataTemplate;
            seriesLO.TrackBallInfoTemplate = Tlo;
            var sA = this.Resources["TemplateActual"] as DataTemplate;
            var sL = this.Resources["TemplateLoss"] as DataTemplate;
            var sT = this.Resources["TemplateTargeted"] as DataTemplate;
            seriesActual.TrackBallInfoTemplate = sA;
            seriesLoss.TrackBallInfoTemplate = sL;
            seriesTargeted.TrackBallInfoTemplate = sT;
            this.Departments.Series.Add(seriesEI);
            this.Departments.Series.Add(seriesEO);
            this.Departments.Series.Add(seriesLI);
            this.Departments.Series.Add(seriesLO);
            this.ActualvsTargeted.Series.Add(seriesActual);
            this.ActualvsTargeted.Series.Add(seriesLoss);
            this.ActualvsTargeted.Series.Add(seriesTargeted);
             
           
        }
         
    }
    }

