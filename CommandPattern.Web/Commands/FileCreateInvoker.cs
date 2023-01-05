using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPattern.Web.Commands
{
    public class FileCreateInvoker
    {
        private ITableActionCommand _command;
        private List<ITableActionCommand> _commandList = new List<ITableActionCommand>();

        public IActionResult CreateFile()
        {
            return _command.Execute();
        }

        public void SetCommand(ITableActionCommand command)
        {
            _command = command;
        }

        public void AddCommand(ITableActionCommand tableCommand)
        {
            _commandList.Add(tableCommand);
        }

        public List<IActionResult> CreateFiles()
        {
            return _commandList.Select(v => v.Execute()).ToList();
        }
    }
}
