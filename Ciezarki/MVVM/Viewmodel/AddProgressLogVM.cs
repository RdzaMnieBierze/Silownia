using Ciezarki.MVVM.Model;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ciezarki.MVVM.Viewmodel
{
    internal class AddProgressLogVM:BaseVM
    {
        private ProgressLog _progressLog;

        private AppDbContext _dbContext;

        public AddProgressLogVM()
        {
            _dbContext = new AppDbContext();
          
            _progressLog = new ProgressLog();

            LoadData();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }




        public string Weight
        {
            get => _progressLog.Weight.ToString();
            set             {
                if (double.TryParse(value, out double weight))
                {
                    _progressLog.Weight = weight;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        public string Height
        {
            get => _progressLog.Height.ToString();
            set
            {
                if (double.TryParse(value, out double height))
                {
                    _progressLog.Height = height;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public string Biceps     {
            get => _progressLog.Biceps.ToString();
            set
            {
                if (double.TryParse(value, out double biceps))
                {
                    _progressLog.Biceps = biceps;
                    OnPropertyChanged(nameof(Biceps));
                }
            }
        }

        public string Chest
        {
            get => _progressLog.Chest.ToString();

            set
            {
                if (double.TryParse(value, out double chest))
                {
                    _progressLog.Chest = chest;
                    OnPropertyChanged(nameof(Chest));
                }
            }
        }

        public void LoadData()
        {
            _dbContext.Database.EnsureCreated();
            var logs = _dbContext.ProgressLogs
                .Where(p => p.UserId == DbData.UserId)
                .OrderBy(p => p.Date)
                .ToList();

            var weightValues = new ChartValues<double>();
            var heightValues = new ChartValues<double>();
            var chestValues = new ChartValues<double>();
            var bicepsValues = new ChartValues<double>();
            var labels = new List<string>();

            foreach (var log in logs)
            {
                weightValues.Add(log.Weight);
                heightValues.Add(log.Height);
                chestValues.Add(log.Chest);
                bicepsValues.Add(log.Biceps);
                labels.Add(log.Date.ToShortDateString());
            }

            SeriesCollection = new SeriesCollection
    {
        new LineSeries
        {
            Title = "Waga",
            Values = weightValues
        },
        new LineSeries
        {
            Title = "Wzrost",
            Values = heightValues
        },
        new LineSeries
        {
            Title = "Klata",
            Values = chestValues
        },
        new LineSeries
        {
            Title = "Biceps",
            Values = bicepsValues
        }
    };

            Labels = labels.ToArray();
            YFormatter = value => value.ToString("F1");

            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(YFormatter));
        }
        private void SaveProgress()
        {
            if(CheckData() == false)
            {
                MessageBox.Show("Wprowadź poprawne dane!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _progressLog.UserId = DbData.UserId; 
            _dbContext.Database.EnsureCreated();
            _progressLog.Date = DateTime.Now;
            _dbContext.ProgressLogs.Add(_progressLog);
            _dbContext.SaveChanges();
            Clear();
            LoadData(); // Reload data to update the chart

        }

        private bool CheckData()
        {
            if(Weight=="0,0" ||
               Height == "0,0" ||
               Biceps == "0,0" ||
               Chest == "0,0")
            {
               
                return false;
            }
            return true;

        }

        private void Clear()
        {
            _progressLog = new ProgressLog();
            OnPropertyChanged(nameof(Weight));
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Biceps));
            OnPropertyChanged(nameof(Chest));
        }

       public RelayCommand SaveProgressCommand => new RelayCommand(execute =>SaveProgress());




    }
}
