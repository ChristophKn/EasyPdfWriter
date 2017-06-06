namespace EasyPdfWriter
{
  using Extensions;
  using Interfaces;

  /// <summary>
  ///   The PdfWriterWrapper class.
  ///   Wrapper for dependency injection.
  /// </summary>
  public class PdfWriterWrapper : IPdfWriterWrapper
  {
    /// <summary>
    ///   Fills the given pdf with the object.
    /// </summary>
    /// <param name="pdfToFillBytes">The pdf to fill.</param>
    /// <param name="obj">The object to use.</param>
    /// <param name="setReadOnly">Sets the pdf in a read only state.</param>
    /// <returns>The filled pdf.</returns>
    public byte[] FillPdf(byte[] pdfToFillBytes, object obj, bool setReadOnly = true)
    {
      return obj.ToPdf(pdfToFillBytes, setReadOnly);
    }
  }
}