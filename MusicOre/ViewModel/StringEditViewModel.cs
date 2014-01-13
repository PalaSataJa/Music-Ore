using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using HW.WpfControls.DAL;

namespace HW.WpfControls.ViewModel
{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class StringEditViewModel : ViewModelBase
	{
		/// <summary>
		/// Initializes a new instance of the StringEditViewModel class.
		/// </summary>
		public StringEditViewModel()
		{
		}

		#region Label
		/// <summary>
		/// The <see cref="Label" /> property's name.
		/// </summary>
		public const string LabelPropertyName = "Label";

		private string label = "Label";

		/// <summary>
		/// Sets and gets the Label property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public string Label
		{
			get
			{
				return label;
			}

			set
			{
				if (label == value)
				{
					return;
				}

				RaisePropertyChanging(LabelPropertyName);
				label = value;
				RaisePropertyChanged(LabelPropertyName);
			}
		}
		#endregion

		#region Text
		/// <summary>
		/// The <see cref="Text" /> property's name.
		/// </summary>
		public const string TextPropertyName = "Text";

		private string text = "Text to change";

		/// <summary>
		/// Sets and gets the Text property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public string Text
		{
			get
			{
				return text;
			}

			set
			{
				if (text == value)
				{
					return;
				}

				RaisePropertyChanging(TextPropertyName);
				text = value;
				RaisePropertyChanged(TextPropertyName);
			}
		}
		#endregion

		#region Title
		/// <summary>
		/// The <see cref="Title" /> property's name.
		/// </summary>
		public const string TitlePropertyName = "Title";

		private string title = "Title";

		/// <summary>
		/// Sets and gets the Title property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}

			set
			{
				if (title == value)
				{
					return;
				}

				RaisePropertyChanging(TitlePropertyName);
				title = value;
				RaisePropertyChanged(TitlePropertyName);
			}
		}
		#endregion

		#region DialogResult
		/// <summary>
		/// The <see cref="DialogResult" /> property's name.
		/// </summary>
		public const string DialogResultPropertyName = "DialogResult";

		private bool? _dialogResult;

		/// <summary>
		/// Sets and gets the DialogResult property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public bool? DialogResult
		{
			get
			{
				return _dialogResult;
			}

			set
			{
				if (_dialogResult == value)
				{
					return;
				}

				RaisePropertyChanging(DialogResultPropertyName);
				_dialogResult = value;
				RaisePropertyChanged(DialogResultPropertyName);
			}
		}
		#endregion

		private RelayCommand saveCommand;

		/// <summary>
		/// Gets the SaveCommand.
		/// </summary>
		public RelayCommand SaveCommand
		{
			get
			{
				return saveCommand ?? (saveCommand = new RelayCommand(
						ExecuteSaveCommand,
						CanExecuteSaveCommand));
			}
		}

		private void ExecuteSaveCommand()
		{
			SimpleIoc.Default.GetInstance<ISettingsProvider>().SavePdsConnectionString(Text);
			DialogResult = true;
		}

		private bool CanExecuteSaveCommand()
		{
			return true;
		}
	}
}