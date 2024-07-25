using Assets.Scripts.Pokemon;
using UnityEngine;

public class Swablu : Piece
{
    public Swablu()
    {
        Tier = 1;

        MaxHP = 9;
        HP = MaxHP;

        Atk = 2;
        Speed = 1;
        Range = 1;
        Steps = Speed;
        Sprite = Resources.Load<Sprite>(FilePaths.Swablu);
        Scale = 1.3f;
        Abilities[0] = new Roost(this);

        Events.OnTakeDamageStart += (object? sender, Attack Attack) =>
        {
            Debug.Log($"Taking {Attack.Damage} damage");
            // Attack.Damage = 0;
            // Debug.Log($"But swablu is invulnerable so attack damage was reduced to {Attack.Damage}");
        };

    }

    public override string GetContents()
    {
        return "swablu";
    }
}

public class Roost : ActiveAbility
{
    private Swablu swablu;

    public Roost(Swablu swablu)
    {
        this.swablu = swablu;
    }

    public override void OnUse()
    {
        int healAmount = swablu.MaxHP / 2;
        swablu.HP += healAmount;
        if (swablu.HP > swablu.MaxHP)
        {
            swablu.HP = swablu.MaxHP;
        }
        Debug.Log($"Swablu used Roost and healed for {healAmount} HP. Current HP: {swablu.HP}");
    }
}
