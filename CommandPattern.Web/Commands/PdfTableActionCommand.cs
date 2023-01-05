using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPattern.Web.Commands
{
    public class PdfTableActionCommand<T> : ITableActionCommand
    {
        //Strategy Design Pattern ile bir interface geçebiliriz. ExcelFile ve PdfFile bu interface den türer. Dependency Inversion . Üst sınıflar alt sınıflara bağımlı olmamalıdır
        private readonly PdfFile<T> _pdfFile;
        public PdfTableActionCommand(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }
        public IActionResult Execute()
        {
            var ms = _pdfFile.Create();
            return new FileContentResult(ms.ToArray(), _pdfFile.FileType)
            {
                FileDownloadName = _pdfFile.FileName
            };
        }
    }
}
