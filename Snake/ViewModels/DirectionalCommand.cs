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

    /// <summary>
    ///  A comand that defines the operation of moving in a direction.
    /// </summary>
    /// <param name="dir">the direction that should be checked and applied by this command</param>
    /// <param name="exec">the action that should be executed when the command can be applied</param>
    /// <param name="can">the predicate that checks if the command can be executed</param>
    public class DirectionalCommand(Direction dir, Action<Direction> exec, Predicate<Direction>? can = null) : ICommand
    {
        
        public event EventHandler? CanExecuteChanged;

        private readonly Direction direction = dir;
        private readonly Predicate<Direction>? canExecute = can;
        private readonly Action<Direction> execute = exec;
        

        public bool CanExecute(object? parameter) => canExecute?.Invoke(direction) ?? false;

        public void Execute(object? parameter) => execute(direction);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
