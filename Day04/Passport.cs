using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    public class Passport
    {
        public string PassportID { get; set; }
        public string CountryID { get; set; }
        public int? BirthYear { get; set; }
        public int? IssueYear { get; set; }
        public int? ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColour { get; set; }
        public string EyeColour { get; set; }

        // Return false if any of the properties are null, except CountryID which is considered optional
        public bool HasRequiredFields()
        {
            return (!(PassportID is null || BirthYear is null || IssueYear is null || ExpirationYear is null || Height is null || HairColour is null || EyeColour is null));
        }

        // Check each property is valid, return false if any criteria fails
        public bool IsValid()
        {
            // Passport ID, 9 digit number, including leading 0's
            if (PassportID is null) return false;
            if (PassportID.Length != 9) return false;
            if (!int.TryParse(PassportID, out _)) return false;

            // Birth Year, not null and between 1920 and 2002
            if (BirthYear is null) return false;
            if (BirthYear < 1920 || BirthYear > 2002) return false;

            // Issue Year, not null and between 2010 and 2020
            if (IssueYear is null) return false;
            if (IssueYear < 2010 || IssueYear > 2020) return false;

            // Expiration Year, not null and between 2020 and 2030
            if (ExpirationYear is null) return false;
            if (ExpirationYear < 2020 || ExpirationYear > 2030) return false;

            // Height, not null, within range: 150cm to 193cm or 59in to 76in
            if (Height is null) return false;
            int.TryParse(Height.Substring(0, Height.Length - 2), out int length);
            switch (Height.Substring(Height.Length - 2))
            {
                case "cm":
                    if (length < 150 || length > 193) return false;
                    break;
                case "in":
                    if (length < 59 || length > 76) return false;
                    break;
                default: return false;
            }

            // Hair Colour, not null, is pattern of a # followed by 6 characters 0-9 or a-f
            if (HairColour is null) return false;
            if (!Regex.IsMatch(HairColour, "#[0-9a-f]{6}")) return false;

            // Eye Colour, not null, is in list (amb, blu, brn, gry, grn, hzl, oth)
            string[] eyeColours = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (EyeColour is null) return false;
            if (!eyeColours.Any(EyeColour.Contains)) return false;

            return true;
        }

        // Using the key and value provided parse the input to appropriate properties
        public void TryParse(string key, string val)
        {
            if (key is null || val is null) return;

            // Remove any return characters and trim leading/trailing whitespace
            val = val.Replace('\r', ' ');
            val = val.Trim();

            switch (key)
            {
                case "pid":
                    this.PassportID = val;
                    break;
                case "cid":
                    this.CountryID = val;
                    break;
                case "byr":
                    this.BirthYear = int.Parse(val);
                    break;
                case "iyr":
                    this.IssueYear = int.Parse(val);
                    break;
                case "eyr":
                    this.ExpirationYear = int.Parse(val);
                    break;
                case "hgt":
                    this.Height = val;
                    break;
                case "hcl":
                    this.HairColour = val;
                    break;
                case "ecl":
                    this.EyeColour = val;
                    break;
                default:
                    break;
            }
        }
    }
}
