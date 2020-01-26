using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CAZ.Common.UI.WPF
{
    /// <summary>
    /// Interaction logic for DateSelectorBox.xaml
    /// </summary>
    public partial class DateSelectorBox : UserControl, INotifyPropertyChanged
    {

        #region "Events"


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region "Constructor"

        public DateSelectorBox()
        {
            this.DataContext = this;

            InitializeComponent();

            OnPropertyChanged(nameof(SelectedMonth));
            OnPropertyChanged(nameof(SelectedDay));
            OnPropertyChanged(nameof(SelectedYear));
        }

        #endregion

        #region "Properties"

        public static readonly DependencyProperty DateSelectorHeaderProperty = DependencyProperty.Register("DateSelector",
                                                                                                        typeof(string),
                                                                                                        typeof(DateSelectorBox),
                                                                                                        new UIPropertyMetadata("Date", new PropertyChangedCallback(DateSelectorBox.OnDateSelectorHeaderChanged)));

        public static readonly DependencyProperty MinYearProperty = DependencyProperty.Register("MinYear",
                                                                                                        typeof(int),
                                                                                                        typeof(DateSelectorBox),
                                                                                                        new UIPropertyMetadata(DateTime.Now.Year, new PropertyChangedCallback(DateSelectorBox.OnMinYearChanged)));

        public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage",
                                                                                                        typeof(string),
                                                                                                        typeof(DateSelectorBox),
                                                                                                        new UIPropertyMetadata("", new PropertyChangedCallback(DateSelectorBox.OnErrorMessageChanged)));

        /// <summary>
        /// the header of the date selector
        /// </summary>
        public string DateSelectorHeader
        {
            get
            {
                return (string)GetValue(DateSelectorHeaderProperty);
            }
            set
            {
                SetValue(DateSelectorHeaderProperty, value);
            }
        }

        /// <summary>
        /// The minimum year to include in the years combobox
        /// </summary>
        public int MinYear
        {
            get
            {
                return (int)GetValue(MinYearProperty);
            }
            set
            {
                SetValue(MinYearProperty, value);
            }
        }

        /// <summary>
        /// Error message to show when set
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return (string)GetValue(ErrorMessageProperty);
            }
            set
            {
                SetValue(ErrorMessageProperty, value);
            }
        }

        /// <summary>
        /// boolean to denote whether the error message should be displayed
        /// </summary>
        public bool ShowErrorMessage
        {
            get
            {
                return _showErrorMessage;
            }
            set
            {
                _showErrorMessage = value;
                OnPropertyChanged();
            }
        }
        private bool _showErrorMessage;

        /// <summary>
        /// Dictionary with months and days per that month
        /// </summary>
        public List<(int, string, int)> MonthDays
        {
            get
            {
                if (_monthDays == null)
                {
                    _monthDays = new List<(int, string, int)>();
                    string[] lines = System.IO.File.ReadAllLines(Global.Constants.MonthDaysFilePath);
                    int count = 0;
                    foreach (string line in lines)
                    {
                        _monthDays.Add((count, line.Split(':').First(), Convert.ToInt32(line.Split(':').Last())));
                        count++;
                    }
                }
                return _monthDays;
            }
        }
        private List<(int, string, int)> _monthDays;

        /// <summary>
        /// List of months in the year for the user to select from
        /// </summary>
        public List<string> Months
        {
            get
            {
                _months = new List<string>();
                _months.Add("Month");
                _months.AddRange(MonthDays.Select(md => md.Item2).ToList());
                return _months;
            }
        }
        private List<string> _months;

        /// <summary>
        /// List of days in the current month for the user to select from
        /// </summary>
        public List<string> Days
        {
            get
            {
                _days = new List<string>();
                _days.Add("Day");
                if (SelectedMonth != "Month")
                {
                    int currentYear = System.DateTime.Now.Year;
                    for (int day = 1; day <= MonthDays.Where(md => md.Item2 == SelectedMonth).FirstOrDefault().Item3; day++)
                        _days.Add(day.ToString());
                    if ((currentYear % 4 == 0) && SelectedMonth.Equals("February"))
                    {
                        _days.Add("29");
                    }
                    if (SelectedYear != "Year" && Convert.ToInt32(SelectedYear) % 4 == 0 && SelectedMonth.Equals("February"))
                    {
                        _days.Add("29");
                    }
                }

                return _days;
            }
        }
        private List<string> _days;

        /// <summary>
        /// list of years for the user to select from
        /// </summary>
        public List<string> Years
        {
            get
            {
                if (_years == null)
                {
                    int currentYear = System.DateTime.Now.Year;
                    _years = new List<string>();
                    _years.Add("Year");
                    for (int year = MinYear; year <= currentYear + 25; year++)
                        _years.Add(year.ToString());
                }
                return _years;
            }
        }
        List<string> _years;

        /// <summary>
        /// the selected month by the user
        /// </summary>
        public string SelectedMonth
        {
            get
            {
                if (_selectedMonth == null)
                {
                    _selectedMonth = "Month";
                }
                return _selectedMonth;
            }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Days));
            }
        }
        private string _selectedMonth;

        /// <summary>
        /// the selected day by the user
        /// </summary>
        public string SelectedDay
        {
            get
            {
                if (_selectedDay == null)
                {
                    _selectedDay = "Day";
                }
                return _selectedDay;
            }
            set
            {
                _selectedDay = value;
                OnPropertyChanged();
            }
        }
        private string _selectedDay;

        /// <summary>
        /// the selected year by the user
        /// </summary>
        public string SelectedYear
        {
            get
            {
                if (_selectedYear == null)
                {
                    _selectedYear = "Year";
                }
                return _selectedYear;
            }
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
            }
        }
        private string _selectedYear;

        /// <summary>
        /// The selected date 
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                if (SelectedMonth != "Month" && SelectedDay != "Day" && SelectedYear != "Year")
                {
                    return new DateTime(Convert.ToInt32(SelectedYear), MonthDays.Where(md => md.Item2 == SelectedMonth).FirstOrDefault().Item1, Convert.ToInt32(SelectedDay));
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// updates header property when the binding is changed 
        /// </summary>
        private void UpdateDateSelectorHeader()
        {
            OnPropertyChanged(nameof(DateSelectorHeader));
        }

        /// <summary>
        /// updates the years property when min year is set by the binding 
        /// </summary>
        private void UpdateMinYear()
        {
            OnPropertyChanged(nameof(Years));
        }

        /// <summary>
        /// Updates the text and visibility of the error message
        /// </summary>
        private void UpdateErrorMessage()
        {
            if (ErrorMessage != "")
                ShowErrorMessage = true;
            else
                ShowErrorMessage = false;
            OnPropertyChanged(nameof(ShowErrorMessage));
            OnPropertyChanged(nameof(ErrorMessage));
        }

        /// <summary>
        /// Raises OnPropertyChanged Event
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null, object newValue = null, object oldValue = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName: propertyName));
        }

        /// <summary>
        /// Property changed event for the ErrorMessage Dependency property 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateSelectorBox control = (DateSelectorBox)d;
            control.UpdateErrorMessage();
        }

        /// <summary>
        /// Property changed event for the MinYear dependency property 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMinYearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateSelectorBox control = (DateSelectorBox)d;
            control.UpdateMinYear();
        }

        /// <summary>
        /// Property changed event for the DateSelectorHeader dependency property 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDateSelectorHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateSelectorBox control = (DateSelectorBox)d;
            control.UpdateDateSelectorHeader();
        }

        #endregion

    }
}
