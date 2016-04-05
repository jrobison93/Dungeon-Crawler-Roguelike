public class MediumPotion : PotionDecorator
{
    public MediumPotion(Potion potion) : base(potion)
    {
        this.value = 5.0f;
    }

    public override void SetPotion(Potion potion) 
    {
        base.SetPotion(potion);
        value = 2.5f;
    }

    public override float GetValue()
    {
        return value * potion.GetValue();
    }
}
