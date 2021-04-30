using QuickRaccoon.Models;
using System;
using System.Windows;
using System.Windows.Markup;

namespace QuickRaccoon.Views.Utilities
{
 [MarkupExtensionReturnType(typeof(string))]
 public class Localizer : MarkupExtension
 {
  [ConstructorArgument("ResourceKey")]
  public string ResourceKey { get; set; }

  public Localizer()
  {

  }

  public Localizer(string resourceKey) : this()
  {
   ResourceKey = resourceKey;
  }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
   string key = ResourceKey; //Manuellen Schlüssel nehmen
   if (string.IsNullOrEmpty(key))//Prüden ob manueller Key angegeben
   {//nein => dynamischer Key
    IProvideValueTarget targetProvider = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
    FrameworkElement targetObject = targetProvider.TargetObject as FrameworkElement;
    string target = targetObject?.Name;
    if (string.IsNullOrEmpty(target))
     throw new Exception(nameof(targetProvider.TargetObject) + "must not be empty");
    string property = (targetProvider.TargetProperty as DependencyProperty)?.Name;

    if (string.IsNullOrEmpty(property))
     key = target;
    else
     key = target + "_" + property;
   }
   return ProvideResource(key);
  }

  public object ProvideResource(string key)
  {
   ResourceHandler resources = GetResources();
   if (resources == null)
    return key;
   string res = resources.GetString(key);
   if (string.IsNullOrEmpty(res))
    throw new Exception(string.Format("No resource for key '{0}' found", key));
   return res;
  }

  public ResourceHandler GetResources() => null;
 }
}
