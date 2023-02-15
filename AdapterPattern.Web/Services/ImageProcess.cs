using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdapterPattern.Web.Services
{
    public class ImageProcess : IImageProcess
    {
        public void AddWatermark(string text, string fileName, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
