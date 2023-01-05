using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPattern.Web.Commands
{
    public class ExcelTableActionCommand<T> : ITableActionCommand
    {
        private readonly ExcelFile<T> _excelFile;
        public ExcelTableActionCommand(ExcelFile<T> excelFile)
        {
            _excelFile = excelFile;
        }
        public IActionResult Execute()
        {
            var ms = _excelFile.Create();
            return new FileContentResult(ms.ToArray(), _excelFile.FileType)
            {
                FileDownloadName = _excelFile.FileName
            };
        }
    }
}
