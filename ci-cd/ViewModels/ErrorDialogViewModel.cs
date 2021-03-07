using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ci_cd.ViewModels
{
  public class ErrorDialogViewModel : BindableBase, IDialogAware
  {
    private DelegateCommand<string> _closeDialogCommand;
    private string _message;

    public event Action<IDialogResult> RequestClose;

    public DelegateCommand<string> CloseDialogCommand =>
        _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

    public string Message
    {
      get { return _message; }
      set { SetProperty(ref _message, value); }
    }

    public string Title { get; set; } = "Error!";

    protected void CloseDialog(string parameter)
    {
      RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
    }

    public bool CanCloseDialog() => true;

    public void OnDialogOpened(IDialogParameters parameters)
    {
      Message = parameters.GetValue<string>("message");
    }

    public void OnDialogClosed() { }
  }
}
