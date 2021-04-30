using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;

namespace QuickRaccoon.Views.Converter
{
 public class ViewModelToDataTemplateConverter : GenericConverterMarkupExtension<ViewModelToDataTemplateConverter>, IValueConverter
 {
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
   if (!(value is NotifyableBase vm))
    return Binding.DoNothing;

   Type vmType = vm.GetType();
   string ucPath = vmType.Name;
   if (ucPath.EndsWith("VM"))
    ucPath = ucPath.Replace("VM", "_UserControl");
   else
    ucPath += "_UserControl";

   string assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

   StringReader stringReader = new StringReader(
        @"<DataTemplate 
        xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
        xmlns:view='clr-namespace:QuickRaccoon.UserControls;assembly=" + assembly + @"'> 
            <view:" + ucPath + @"/> 
        </DataTemplate>");
   XmlReader xmlReader = XmlReader.Create(stringReader);
   return XamlReader.Load(xmlReader) as DataTemplate;
  }
 }

}
