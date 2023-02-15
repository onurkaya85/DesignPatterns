using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdapterPattern.Web.Services
{
    public interface IAdvanceImageProcess
    {
        //void AddWatermark(string text, string fileName, Stream stream);

        void AddWatermanrkImage(Stream stream, string text, string filePath, Color color, Color outlineColor);
    }
}
