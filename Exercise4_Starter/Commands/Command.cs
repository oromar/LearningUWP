﻿using System;
using System.Windows.Input;

namespace Exercise4.Commands
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action command = null;
        private readonly Func<bool> canExecute;

        public Command(Action command)
        {
            this.command = command;
        }

        public Command(Action command, Func<bool> canExecute) : this(command)
        {
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(null))
                command?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
