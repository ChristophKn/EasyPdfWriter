# EasyPdfWriter
A wrapper around the ITextSharp library

## Usage
```
var pdfValues = new 
                  {
                    Date = DateTime.Now.ToString(),
                    Name = "Karl",
                    Information = "Information-Text"
                    
                    //// Supports nested fields eg. Tables in a pdf document.
                    TableName = new[] 
                                    {
                                      new
                                      {
                                        RowName = new[]
                                        {
                                          new
                                          {
                                            FirstCell = "FirstCell",
                                            SecondCell = "SecondCell"
                                          }
                                        }
                                      }
                                    }
                  };
```

Creating an anonymous object with the pdf-fieldnames as the property names.

### Usage with wrapper
```
var pdfToFillBytes = File.ReadAllBytes("PathToYourPdf");

PdfWriterWrapper pdfWriter = new PdfWriterWrapper();
var filledPdfBytes = pdfWriter.FillPdf(pdfToFillBytes, pdfValues);
```

### Usage with extensions
```
var pdfToFillBytes = File.ReadAllBytes("PathToYourPdf");
var filledPdfBytes = pdfValues.ToPdf(pdfToFillBytes);
```
