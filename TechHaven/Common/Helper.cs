namespace TechHaven.Common;

public static class Helper
{
    public static Func<decimal, decimal> ToEuro = (priceInLv) => priceInLv / 1.95m;
}
