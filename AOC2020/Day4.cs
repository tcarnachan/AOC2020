using System.Linq;

namespace AOC2020
{
    public class Day4
    {
        string[] fields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        string[] passports;

        public Day4()
        {
            InputReader ir = new InputReader(4);
            passports = ir.GetInput("\n\n");
        }

        public int RunSilver()
        {
            return passports.Count(p => fields.All(f => p.Split().Any(pfield => pfield.StartsWith(f))));
        }

        public int RunGold()
        {
            return passports.Count(p => fields.All(f => VerifyField(f, p.Split())));
        }

        private static bool VerifyField(string field, string[] data)
        {
            string fieldData = data.SingleOrDefault(s => s.StartsWith(field));
            if (fieldData == null)
                return false;
            fieldData = fieldData.Split(':')[1];
            int t;
            switch (field)
            {
                case "byr":
                    t = int.Parse(fieldData);
                    return 1920 <= t && t <= 2002;
                case "iyr":
                    t = int.Parse(fieldData);
                    return 2010 <= t && t <= 2020;
                case "eyr":
                    t = int.Parse(fieldData);
                    return 2020 <= t && t <= 2030;
                case "hgt":
                    if (fieldData.Length <= 2)
                        return false;
                    t = int.Parse(fieldData.Substring(0, fieldData.Length - 2));
                    if (fieldData.EndsWith("cm"))
                        return 150 <= t && t <= 193;
                    if (fieldData.EndsWith("in"))
                        return 59 <= t && t <= 76;
                    return false;
                case "hcl":
                    if (fieldData[0] != '#')
                        return false;
                    fieldData = fieldData.Substring(1);
                    return fieldData.Length == 6 && fieldData.All(c => "0123456789abcdef".Contains(c));
                case "ecl":
                    return "amb blu brn gry grn hzl oth".Split().Contains(fieldData);
                case "pid":
                    if (int.TryParse(fieldData, out t))
                        return fieldData.Length == 9;
                    return false;
                default:
                    return false;
            }
            /*return field switch
            {
                "byr" => InRange(1920, int.Parse(fieldData), 2002),
                "iyr" => InRange(2010, int.Parse(fieldData), 2020),
                "eyr" => InRange(2020, int.Parse(fieldData), 2030),
                "hgt" => ((Func<bool>)(() => {
                    if (fieldData.Length <= 2)
                        return false;
                    t = int.Parse(fieldData.Substring(0, fieldData.Length - 2));
                    if (fieldData.EndsWith("cm"))
                        return 150 <= t && t <= 193;
                    if (fieldData.EndsWith("in"))
                        return 59 <= t && t <= 76;
                    return false;
                }))(),
                "hcl" => ((Func<bool>)(() => {
                    if (fieldData[0] != '#')
                        return false;
                    fieldData = fieldData.Substring(1);
                    return fieldData.Length == 6 && fieldData.All(c => "0123456789abcdef".Contains(c));
                }))(),
                "ecl" => "amb blu brn gry grn hzl oth".Split().Contains(fieldData),
                "pid" => int.TryParse(fieldData, out t) && fieldData.Length == 9,
                _ => false
            };*/
        }

        private static bool InRange(int val, int min, int max) => min <= val && val <= max;
    }
}
