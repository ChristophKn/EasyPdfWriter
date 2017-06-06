namespace EasyPdfWriter
{
  using System;
  using System.Xml;

  /// <summary>
  ///   The XmlNodeExtensions class.
  /// </summary>
  public static class XmlNodeExtensions
  {
    /// <summary>
    ///   Appends the given object with all its properties to the xml node.
    /// </summary>
    /// <param name="root">The xml node to append too.</param>
    /// <param name="obj">The object to use.</param>
    public static void AppendChild(this XmlNode root, object obj)
    {
      if (obj.IsNull())
      {
        return;
      }

      foreach (var property in obj.GetType().GetProperties())
      {
        if (property.PropertyType.BaseType == typeof(Array) && property.PropertyType != typeof(byte[]))
        {
          foreach (var item in property.GetValue(obj) as Array)
          {
            var element = root.AppendChild(root.OwnerDocument.CreateElement(property.Name));
            element.AppendChild(item);
          }
        }
        else
        {
          var element = root.OwnerDocument.CreateElement(property.Name);
          var valToSet = property.GetValue(obj);
          if (valToSet.IsNotNull() && valToSet.GetType() == typeof(byte[]))
          {
            element.InnerText = Convert.ToBase64String((byte[])valToSet);
          }
          else
          {
            element.InnerText = valToSet.ToString();
          }

          root.AppendChild(element);
        }
      }
    }
  }
}