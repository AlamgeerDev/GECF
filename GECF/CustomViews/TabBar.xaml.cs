using System.Windows.Input;

namespace GECF.CustomViews;

public partial class TabBar : Grid
{
    public TabBar()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(TabBar), null);
    public ICommand BackCommand
    {
        get
        {
            return (ICommand)GetValue(BackCommandProperty);
        }
        set
        {
            SetValue(BackCommandProperty, value);
        }
    }
}
