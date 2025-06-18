using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake.ViewModels
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public class DirectionalCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Direction _direction;
        private readonly Predicate<Direction>? _canExecute;
        private readonly Action<Direction> _execute;

        public DirectionalCommand(Direction direction, Action<Direction> execute)
            : this(direction, execute, null)
        {
        }

        public DirectionalCommand(Direction direction, Action<Direction> execute, Predicate<Direction>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
            _direction = direction;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(_direction);
        }

        public void Execute(object? parameter)
        {
            _execute(_direction);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
