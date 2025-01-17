﻿namespace EasyPdfWriter.Extensions
{
  using iTextSharp.text;
  using iTextSharp.text.pdf;
  using Security;

  /// <summary>
  ///   The ITextSharpImageExtensions class.
  /// </summary>
  public static class ITextSharpImageExtensions
  {
    /// <summary>
    ///   Scales and sets the absolute position for the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="fieldPosition">The field position to use.</param>
    /// <returns>The image which was used.</returns>
    public static Image SetImageMetrics(this Image image, AcroFields.FieldPosition fieldPosition)
    {
      Guard.AgainstNull(fieldPosition, "The field position cannot be empty.");

      image.ScaleAbsolute(fieldPosition.position.Width, fieldPosition.position.Height);
      image.SetAbsolutePosition(fieldPosition.position.Left, fieldPosition.position.Bottom);

      return image;
    }
  }
}