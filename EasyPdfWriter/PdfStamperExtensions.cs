namespace EasyPdfWriter
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Xml;
  using iTextSharp.text;
  using iTextSharp.text.pdf;

  /// <summary>
  ///   The PdfStamperExtensions class.
  /// </summary>
  public static class PdfStamperExtensions
  {
    /// <summary>
    ///   Sets the fields in the pdf stamper with the given object data.
    ///   Based on the framework, it's easier to use an anonymous-object to fill the pdf.
    /// </summary>
    /// <param name="pdfStamper">The pdf stamper to use.</param>
    /// <param name="data">The data to set.</param>
    public static void SetFields(this PdfStamper pdfStamper, object data)
    {
      if (pdfStamper.IsXfaCompliant())
      {
        pdfStamper.AcroFields.Xfa.FillXfaForm(data.ToXfaCompliantXml());
      }
      else
      {
        foreach (var fieldName in pdfStamper.GetFieldNames())
        {
          var valToSet = data.GetPropertyValue<object>(fieldName);

          var isImageData = valToSet.IsNotNull() && valToSet.GetType() == typeof(byte[]);
          if (isImageData && pdfStamper.GetFieldPosition(fieldName, out var fieldPos))
          {
            var image = Image.GetInstance((byte[])valToSet);
            pdfStamper.GetOverContent(fieldPos.page).AddImage(image.SetImageMetrics(fieldPos));
          }
          else
          {
            pdfStamper.AcroFields.SetField(fieldName, valToSet.ToStringSafe());
          }
        }
      }
    }

    /// <summary>
    ///   Sets each field in the pdfStamper to read only.
    /// </summary>
    /// <param name="pdfStamper">The pdf stamper to use.</param>
    public static void SetReadOnly(this PdfStamper pdfStamper)
    {
      if (pdfStamper.IsXfaCompliant())
      {
        pdfStamper.AcroFields.Xfa.DomDocument.GetElementsByTagName("field")
          .Cast<XmlElement>()
          .ToList()
          .ForEach(element => element.SetAttribute("access", "readOnly"));
      }
      else
      {
        pdfStamper.FormFlattening = true;
      }
    }

    /// <summary>
    ///   Checks if the given pdf stamper is xfa complaint.
    /// </summary>
    /// <param name="pdfStamper">The pdf stamper to use.</param>
    /// <returns>A value indicating whether xfa complaint or not.</returns>
    public static bool IsXfaCompliant(this PdfStamper pdfStamper)
    {
      return pdfStamper.AcroFields.Xfa.XfaPresent;
    }

    /// <summary>
    ///   Checks if the pdf stamper has any field positions.
    ///   If the stamper has any field positions it'll set the out parameter with the first field position.
    /// </summary>
    /// <param name="pdfStamper">The pdf stamper to use.</param>
    /// <param name="name">The field name to use.</param>
    /// <param name="fieldPosition">The field position, which is set to the first field position or to null.</param>
    /// <returns>True or false, whether the field has any field positions or not.</returns>
    public static bool GetFieldPosition(this PdfStamper pdfStamper, string name, out AcroFields.FieldPosition fieldPosition)
    {
      var fieldPositions = pdfStamper.AcroFields.GetFieldPositions(name);

      fieldPosition = fieldPositions.Any() ? fieldPositions[0] : null;

      return fieldPositions.Any();
    }

    /// <summary>
    ///   Gets the field names from the pdf stamper.
    /// </summary>
    /// <param name="pdfStamper">The pdf stamper to use.</param>
    /// <returns>A collection of the field names.</returns>
    public static IEnumerable<string> GetFieldNames(this PdfStamper pdfStamper)
    {
      return pdfStamper.AcroFields.Fields.Select(field => field.Key);
    }
  }
}