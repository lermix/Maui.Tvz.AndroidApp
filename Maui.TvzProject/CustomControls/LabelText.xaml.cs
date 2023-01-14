namespace Maui.TvzProject.CustomControls;

public partial class LabelText : ContentView
{

   public static readonly BindableProperty PropProperty = BindableProperty.Create(nameof(Prop), typeof(string), typeof(LabelText), string.Empty);

   public string Prop
   {
	  get => (string) GetValue(PropProperty);
	  set => SetValue(PropProperty, value);
   }

   public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(LabelText), string.Empty);

   public string Value
   {
	  get => (string) GetValue(ValueProperty);
	  set => SetValue(ValueProperty, value);
   }
   public LabelText()
	{
		InitializeComponent();
	}
}