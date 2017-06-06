namespace EasyPdfWriter.Interfaces
{
  /// <summary>
  /// The PdfWriterWrapper Interface.
  /// </summary>
  public interface IPdfWriterWrapper
  {
    /// <summary>
    ///   Fills the given pdf with the object.
    /// </summary>
    /// <param name="pdfToFillBytes">The pdf to fill.</param>
    /// <param name="obj">The object to use.</param>
    /// <param name="setReadOnly">Sets the pdf in a read only state.</param>
    /// <returns>The filled pdf.</returns>
    byte[] FillPdf(byte[] pdfToFillBytes, object obj, bool setReadOnly = true);
  }
}