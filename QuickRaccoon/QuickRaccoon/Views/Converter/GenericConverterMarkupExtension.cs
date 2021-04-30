using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace QuickRaccoon.Views.Converter
{
 /// <summary>
 /// Basisklasse für Converter, um dieses generisch als MarkupExtension deklarieren zu können. Abgeleitete Converter müssen nur noch die Convert Methode, und ggf. die ConvertBack Methode, überschreiben.
 /// Zugriff in Xaml über "Converter={namespace:MyConverter}"
 /// </summary>
 /// <typeparam name="T">Typ des eigen erstellten Interpreters</typeparam>
 [MarkupExtensionReturnType(typeof(IValueConverter))]
 public abstract class GenericConverterMarkupExtension<T> : MarkupExtension where T : class, IValueConverter, new()
 {
  public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

  public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
   throw new NotImplementedException();
  }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
   return this;
  }
 }
}
