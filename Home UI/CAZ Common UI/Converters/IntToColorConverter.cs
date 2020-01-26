using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CAZ.Common.UI.Converters
{
    class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BrushConverter converter = new BrushConverter();
            return (Brush)converter.ConvertFromString(System.IO.File.ReadAllLines(Global.Constants.ColorCategoryMappingFilePath)
                .Where(l => l.Split(':').First().Equals((int)value))
                .FirstOrDefault()
                .Split(':')
                .Last());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
