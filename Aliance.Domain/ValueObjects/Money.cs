using Flunt.Notifications;
using Flunt.Validations;

namespace Aliance.Domain.ValueObjects;

public class Money : Notifiable<Notification>
{
    public decimal Value { get; }

    protected Money() { }

    public Money(decimal amount)
    {
        Value = amount;

        AddNotifications(new Contract<Money>()
                .Requires()
                .IsGreaterOrEqualsThan(Value, 0, nameof(Value), "O valor não pode ser negativo.")
                .IsLowerOrEqualsThan(Value, 999999999, nameof(Value), "Valor muito alto.")
            );

        if (IsValid)
            Value = decimal.Round(amount, 2, MidpointRounding.ToZero);
    }
}
