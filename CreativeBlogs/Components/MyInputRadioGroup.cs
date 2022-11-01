using Microsoft.AspNetCore.Components.Forms;

namespace CreativeBlogs.Components;
/// <summary>
/// this component avoids the need to double click on radio button because of EditForm rerender issues
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class MyInputRadioGroup<TValue> : InputRadioGroup<TValue>
{
    private string _name;
    private string _fieldClass;

    protected override void OnParametersSet()
    {
        var fieldClass = EditContext?.FieldCssClass(FieldIdentifier) ?? string.Empty;
        if (fieldClass != _fieldClass || Name != _name)
        {
            _fieldClass = fieldClass;
            _name = Name;
            base.OnParametersSet();
        }
    }
}
