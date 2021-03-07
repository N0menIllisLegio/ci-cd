using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace ci_cd.Utils
{
  class ClearFocusOnClickBehavior : Behavior<FrameworkElement>
  {
    protected override void OnAttached()
    {
      AssociatedObject.MouseDown += AssociatedObject_MouseDown;
      base.OnAttached();
    }

    private static void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
      Keyboard.ClearFocus();
    }

    protected override void OnDetaching()
    {
      AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
    }
  }
}
