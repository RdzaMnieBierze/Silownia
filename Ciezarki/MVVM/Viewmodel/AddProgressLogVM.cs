using Ciezarki.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        }

        public string Weight
        {
            get => _progressLog.Weight.ToString("F1");
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
            get => _progressLog.Height.ToString("F1");
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
            get => _progressLog.Biceps.ToString("F1");
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
            get => _progressLog.Chest.ToString("F1");

            set
            {
                if (double.TryParse(value, out double chest))
                {
                    _progressLog.Chest = chest;
                    OnPropertyChanged(nameof(Chest));
                }
            }
        }

      

        private void SaveProgress()
        {
            _progressLog.UserId = 1; // Assuming user ID is 1 for demonstration purposes
            _dbContext.Database.EnsureCreated();
            _progressLog.Date = DateTime.Now;
            _dbContext.ProgressLogs.Add(_progressLog);
            _dbContext.SaveChanges();
            Clear();

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
