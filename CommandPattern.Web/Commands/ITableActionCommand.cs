using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPattern.Web.Commands
{
    public interface ITableActionCommand
    {
        IActionResult Execute();
    }
}
