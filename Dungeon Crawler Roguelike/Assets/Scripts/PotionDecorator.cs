public abstract class PotionDecorator : Potion
{
    protected Potion potion;

    public PotionDecorator(Potion potion)
    {
        this.potion = potion;
    }

    public virtual void SetPotion(Potion potion)
    {
        this.potion = potion;
    }


    public override abstract float GetValue();
}
