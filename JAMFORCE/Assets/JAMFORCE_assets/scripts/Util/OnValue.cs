[System.Serializable]
public class OnValue<T> where T : struct
{
    public T value, old;
    public readonly VoidFrom<T> onChange;

    //------------------------------------------------------------------------------------------------------------------------------

    public OnValue(T init = default, VoidFrom<T> onChange = default)
    {
        value = old = init;

        if (onChange == null)
            this.onChange = delegate { };
        else
        {
            this.onChange = onChange;
            onChange(init);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public void OnChange(T value)
    {
        if (!value.Equals(this.value))
            onChange(value);

        old = this.value;
        this.value = value;
    }
}