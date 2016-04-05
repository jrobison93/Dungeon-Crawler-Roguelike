public class LargePotion : PotionDecorator
{
    public LargePotion(Potion potion) : base(potion)
    {
        this.value = 5.0f;
    }

    public override void SetPotion(Potion potion)
    {
        base.SetPotion(potion);
        value = 5.0f;
    }

    public override float GetValue()
    {
        return value * potion.GetValue();
    }
}
