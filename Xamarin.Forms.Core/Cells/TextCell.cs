using System;
using System.Windows.Input;

namespace Xamarin.Forms
{
	public class TextCell : Cell
	{
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command) , typeof(ICommand), typeof(TextCell), default(ICommand),
			propertyChanging: (bindable, oldvalue, newvalue) =>
			{
				var textCell = (TextCell)bindable;
				var oldcommand = (ICommand)oldvalue;
				if (oldcommand != null)
					oldcommand.CanExecuteChanged -= textCell.OnCommandCanExecuteChanged;
			}, propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				var textCell = (TextCell)bindable;
				var newcommand = (ICommand)newvalue;
				if (newcommand != null)
				{
					textCell.IsEnabled = newcommand.CanExecute(textCell.CommandParameter);
					newcommand.CanExecuteChanged += textCell.OnCommandCanExecuteChanged;
				}
			});

		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(TextCell), default(object),
			propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				var textCell = (TextCell)bindable;
				if (textCell.Command != null)
				{
					textCell.IsEnabled = textCell.Command.CanExecute(newvalue);
				}
			});

		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(TextCell), default(string));

		public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(string), typeof(TextCell), default(string));

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TextCell), Color.Default);

		public static readonly BindableProperty DetailColorProperty = BindableProperty.Create(nameof(DetailColor), typeof(Color), typeof(TextCell), Color.Default);

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public string Detail
		{
			get { return (string)GetValue(DetailProperty); }
			set { SetValue(DetailProperty, value); }
		}

		public Color DetailColor
		{
			get { return (Color)GetValue(DetailColorProperty); }
			set { SetValue(DetailColorProperty, value); }
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public Color TextColor
		{
			get { return (Color)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		protected internal override void OnTapped()
		{
			base.OnTapped();

			if (!IsEnabled)
			{
				return;
			}

			Command?.Execute(CommandParameter);
		}

		void OnCommandCanExecuteChanged(object sender, EventArgs eventArgs)
		{
			IsEnabled = Command.CanExecute(CommandParameter);
		}
	}
}