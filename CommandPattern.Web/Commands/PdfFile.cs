using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Web.Commands
{
    public class PdfFile<T>
    {
        public readonly List<T> _list;
        public readonly HttpContext _context;

        public PdfFile(List<T> list, HttpContext context)
        {
            _context = context;
            _list = list;
        }

        public string FileName => $"{typeof(T).Name}.pdf";
        public string FileType => "application/octet-stream";

        public MemoryStream Create()
        {
            var type = typeof(T);
            var sb = new StringBuilder();
            sb.Append($@"<html>
             <head></head>
             <body>
               <div class='text-center'>
                  <h1>{type.Name} tablo</h1>
               </div>
               <table class='table table-striped' align='center'>");

            sb.Append("<tr>");
            type.GetProperties().ToList().ForEach(v =>
            {
                sb.Append($"<th>{v.Name}</th>");
            });

            sb.Append("</th>");

            _list.ForEach(v =>
            {
                var values = typeof(T).GetProperties().Select(pi => pi.GetValue(v, null)).ToList();
                sb.Append("<tr>");
                values.ForEach(c =>
                {
                    sb.Append($"<td>{c}</td>");
                });
                sb.Append("</tr>");
            });

            sb.Append("</table></body></html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                  ColorMode = ColorMode.Color,
                  Orientation = Orientation.Portrait,
                  PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = sb.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8",UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/lib/bootstrap/dist/css/bootstrap.css") },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            var converter = _context.RequestServices.GetRequiredService<IConverter>();
            byte[] pdf = converter.Convert(doc);
            MemoryStream pdfMemory = new(pdf);

            return pdfMemory;
        }
    }
}
