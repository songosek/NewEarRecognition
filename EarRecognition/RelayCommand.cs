﻿using System;
using System.Windows.Input;
namespace EarRecognition
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        public RelayCommand(Action<object> execute)
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
        public bool CanExecute(object parameters)
        {
            return _canExecute == null ? true : _canExecute(parameters);
        public event EventHandler CanExecuteChanged
        public void Execute(object parameters)