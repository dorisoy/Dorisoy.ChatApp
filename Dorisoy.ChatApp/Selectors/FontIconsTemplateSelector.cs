namespace Dorisoy.ChatApp.Selectors;

public class FontIconsTemplateSelector : DataTemplateSelector
{
    public DataTemplate Dorisoy { get; set; }
    public DataTemplate FaBrands { get; set; }
    public DataTemplate FaPro { get; set; }
    public DataTemplate FaRegular { get; set; }
    public DataTemplate LineAwesome { get; set; }
    public DataTemplate IonIcons { get; set; }
    public DataTemplate MaterialDesign { get; set; }


    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (((FontIconModel)item).Type.Contains("Dorisoy"))
        {
            return Dorisoy;
        }

        if (((FontIconModel)item).Type.Contains("Brands"))
        {
            return FaBrands;
        }

        if (((FontIconModel)item).Type.Contains("Pro"))
        {
            return FaPro;
        }

        if (((FontIconModel)item).Type.Contains("Regular"))
        {
            return FaRegular;
        }

        if (((FontIconModel)item).Type.Contains("Line Awesome"))
        {
            return LineAwesome;
        }

        if (((FontIconModel)item).Type.Contains("IonIcons"))
        {
            return IonIcons;
        }

        if (((FontIconModel)item).Type.Contains("Material Designs"))
        {
            return MaterialDesign;
        }

        //return ((FontIconModel)item).Type.Contains("Brands") ? FaBrands : FaPro;
        return null;
    }
}
