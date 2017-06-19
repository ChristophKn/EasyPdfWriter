namespace EasyPdfWriter.Extensions
{
  using System.IO;
  using System.Reflection;
  using System.Xml;
  using iTextSharp.text.pdf;
  using Security;

  /// <summary>
  ///   The ObjectExtensions class.
  /// </summary>
  public static class ObjectExtensions
  {
    /// <summary>
    ///   Fills the given pdf bytes with the given object.
    /// </summary>
    /// <param name="obj">The object to use.</param>
    /// <param name="pdfToFillBytes">The pdf to fill bytes.</param>
    /// <param name="setReadOnly">A value indicating whether to set the pdf read only or not.</param>
    /// <returns>The filled pdf.</returns>
    public static byte[] ToPdf(this object obj, byte[] pdfToFillBytes, bool setReadOnly = true)
    {
      Guard.AgainstNull(obj, "The object cannot be null.");
      Guard.AgainstEmpty(pdfToFillBytes, "Pdf to fill bytes cannot be null or empty.");

      using (var sourcePdf = new PdfReader(pdfToFillBytes))
      using (var destinationStream = new MemoryStream())
      using (var destinationPdf = new PdfStamper(sourcePdf, destinationStream))
      {
        destinationPdf.SetFields(obj);

        if (setReadOnly)
        {
          destinationPdf.SetReadOnly();
        }

        return destinationStream.ToArray();
      }
    }

    /// <summary>
    ///   Converts the object to xfa compliant xml.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>The converted xml node.</returns>
    public static XmlNode ToXfaCompliantXml(this object obj)
    {
      Guard.AgainstNull(obj, "The given object cannot be null");

      var doc = new XmlDocument();

      var root = doc.AppendChild(doc.CreateElement("root"));
      root.AppendChild(obj);

      return root;
    }

    /// <summary>
    ///   Gets the property value with the given name.
    /// </summary>
    /// <typeparam name="T">The type of the value to get.</typeparam>
    /// <param name="obj">The object to use.</param>
    /// <param name="propertyName">The property name to use.</param>
    /// <returns>The value of the property or the default value of the given type.</returns>
    public static T GetPropertyValue<T>(this object obj, string propertyName)
    {
      Guard.AgainstNull(obj, "The given object cannot be null");

      var property = obj.GetType()
        .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

      return property.IsNotNull() ? (T)property.GetValue(obj) : default(T);
    }

    /// <summary>
    ///   Returns an empty string if the object is null or the object as a string if not null.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>An empty string or the object as a string.</returns>
    public static string ToStringSafe(this object obj)
    {
      return obj?.ToString() ?? string.Empty;
    }

    /// <summary>
    ///   Checks if the given obj is not null.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>A value indicating whether the object is not null or is null.</returns>
    public static bool IsNotNull(this object obj)
    {
      return obj != null;
    }
  }
}