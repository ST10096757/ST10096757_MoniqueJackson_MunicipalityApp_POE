using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2
{
	// This class implements the ICommand interface, allowing commands to be used in MVVM pattern.
	public class RelayCommand : ICommand
	{
		// Delegate to hold the method to execute when the command is triggered
		private readonly Action _execute;

		// Delegate to determine whether the command can execute 
		private readonly Func<bool> _canExecute;

		// Constructor that takes only the execute action (canExecute defaults to null)
		public RelayCommand(Action execute) : this(execute, null) { }

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Constructor that takes both the execute action and canExecute function
		/// </summary>
		/// <param name="execute"></param>
		/// <param name="canExecute"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			// Ensure that the execute action is not null
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));

			// Assign the canExecute function 
			_canExecute = canExecute;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//

		// Determines whether the command can execute (returns true if canExecute is null or its condition is met)
		public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

		// Executes the action that was passed to the constructor
		public void Execute(object parameter) => _execute();

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
		/// <summary>
		/// Event to notify when the "CanExecute" state changes, allowing UI to refresh
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			// Add the event handler to CommandManager's RequerySuggested event
			add => CommandManager.RequerySuggested += value;

			// Remove the event handler from CommandManager's RequerySuggested event
			remove => CommandManager.RequerySuggested -= value;
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	}
}
