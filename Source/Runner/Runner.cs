using System;
using System.ComponentModel;
using System.Windows.Input;
using SumOfMultiple;
using SequenceAnalysis;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;


//==========================================================================
//Localization is considered but not implemented.
//Unit tests are considered but not written. 
//==========================================================================
namespace Runner
{
    /// <summary>
    /// Enum for Sequence analysis methods. Used to find method selection by User.
    /// </summary>
    public enum SeqMethod
    {
        LINQ = 1,
        ARRAY = 2
    }
    /// <summary>
    /// Enum for Sum of multiples methods.Used to find method selection by User.
    /// </summary>
    public enum SOMMethod
    {
        MATH_FORMULA = 1,
        FORLOOP = 2,
        LINQ = 3,
        HASHSET = 4
    }
    /// <summary>
    /// RunnerViewModel class to represent properties of Runner view UI
    /// </summary>
    public class RunnerViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _inputLimText;
        public string InputLimitText
        {
            get
            {
                return _inputLimText;
            }
            set
            {
                _inputLimText = value;
                OnPropertyChanged("InputLimitText");
            }
        }
        public string InputSeqText { get; set; }

        public bool IsSumOfMultSelected { get; set; }
        public bool IsSeqSelected { get; set; }

        public SeqMethod SelectedSeqMethod { get; set; }

        public SOMMethod SelectedSOMMethod { get; set; }

        private string _outPutText;
        public string OutPutText
        {
            get { return _outPutText; }
            set
            {
                _outPutText = value;
                OnPropertyChanged("OutputText");
            }
        }
        #endregion
        #region LocalVariables
        SequenceAnalyzer sequenceAnalyzer;
        SumOfMultipleFinder somFinder;
        #endregion
        public RunnerViewModel()
        {
            somFinder = new SumOfMultipleFinder();
            sequenceAnalyzer = new SequenceAnalyzer();
            RunCommand = new InvokeMethodCommand(InvokeMethod);
        }

        #region private methods
        /// <summary>
        /// Invoke method based on problems selected by User
        /// </summary>
        private void InvokeMethod()
        {
            int limit = int.MinValue;
            if (IsSumOfMultSelected)
            {
                if (int.TryParse(InputLimitText, out limit))
                {
                    long sum = 0;
                    if (limit > 0 && (int)SelectedSOMMethod > 0)
                    {
                        sum = somFinder.CalculateSOM(limit, (int)SelectedSOMMethod);
                        OutPutText = (sum < 0) ? "ERROR" : "Sum of multiples of 3 and 5 below " + limit + " is : " + sum;
                    }
                    else
                        OutPutText = "Please select or fill valid options";
                }
                else
                {
                    OutPutText = "Please select or fill valid options";
                }
            }
            else if (IsSeqSelected)
            {
                if (!String.IsNullOrEmpty(InputSeqText) && !String.IsNullOrWhiteSpace(InputSeqText) && (int)SelectedSeqMethod > 0)
                    OutPutText = sequenceAnalyzer.FindUpperCaseWords(InputSeqText, (int)SelectedSeqMethod);
                else
                    OutPutText = "Please select or fill valid options";
            }
            else
            {
                OutPutText = "Please select or fill valid options";
            }
        }

        #endregion
        /// <summary>
        /// Event to raise events to Runner view
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Commands
        /// <summary>
        /// Command to bind to view control
        /// </summary>
        public ICommand RunCommand { get; set; }
        /// <summary>
        /// InvokeMethodCommand class to create command binding for the Run button
        /// </summary>
        private class InvokeMethodCommand : ICommand
        {
            Action _execute;
            public InvokeMethodCommand(Action execute)
            {
                _execute = execute;
            }
            public event EventHandler CanExecuteChanged
            {
                add { }
                remove { }
            }
            #region ICommand Members
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public void Execute(object parameter)
            {
                _execute.Invoke();
            }
            #endregion
        }
        #endregion
    }

    /// <summary>
    /// Converter to convert checked state of Radio button to an Enum value
    /// </summary>
    public class EnumToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }

        #endregion
    }
}
