namespace RecipePlanner.Api.Services;

/// <summary>Scales ingredient quantities when the number of servings changes.</summary>
public static class ServingsCalculator
{
    public const int MinServings = 1;
    public const int MaxServings = 100;

    /// <summary>
    /// Returns the quantity for <paramref name="targetServings"/> servings,
    /// given that <paramref name="quantity"/> is written for <paramref name="baseServings"/>.
    /// Result is rounded to 2 decimals.
    /// </summary>
    public static decimal Scale(decimal quantity, int baseServings, int targetServings)
    {
        if (baseServings < MinServings)
            throw new ArgumentOutOfRangeException(nameof(baseServings), "Base servings must be at least 1.");
        if (targetServings is < MinServings or > MaxServings)
            throw new ArgumentOutOfRangeException(nameof(targetServings), $"Servings must be between {MinServings} and {MaxServings}.");

        return Math.Round(quantity * targetServings / baseServings, 2, MidpointRounding.AwayFromZero);
    }
}
