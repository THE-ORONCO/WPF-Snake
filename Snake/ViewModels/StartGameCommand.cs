using Snake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake.ViewModels
{
    class StartGameCommand(PlayField playField) : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly PlayField PlayField = playField;

        public bool CanExecute(object? parameter) => !PlayField.Running;

        public void Execute(object? parameter) => PlayField.Running = true;
    }
}
