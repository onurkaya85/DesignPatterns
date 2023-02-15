using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdapterPattern.Web.Services
{
    public interface IImageProcess
    {
        void AddWatermark(string text, string fileName, Stream stream);
    }
}
