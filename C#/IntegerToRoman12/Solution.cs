public class Solution
{
    private static readonly (int intValue, string romanValue)[] IntToRomanValues = new (int intValue, string romanValue)[]
    {
        (intValue: 1000, romanValue: "M"),
        (intValue: 900, romanValue: "CM"),
        (intValue: 500, romanValue: "D"),
        (intValue: 400, romanValue: "CD"),
        (intValue: 100, romanValue: "C"),
        (intValue: 90, romanValue: "XC"),
        (intValue: 50, romanValue: "L"),
        (intValue: 40, romanValue: "XL"),
        (intValue: 10, romanValue: "X"),
        (intValue: 9, romanValue: "IX"),
        (intValue: 5, romanValue: "V"),
        (intValue: 4, romanValue: "IV"),
        (intValue: 1, romanValue: "I")
    };

    public string IntToRoman(int num)
    {
        return string.Join("", IntToRomanValues.SelectMany(tuple => {
            int count = num / tuple.intValue;
            num -= count * tuple.intValue;
            return Enumerable.Repeat(tuple.romanValue, count);
        }));
    }
}
