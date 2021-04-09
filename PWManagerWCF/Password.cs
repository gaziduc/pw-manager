using System.Text;
using System.Text.RegularExpressions;

namespace PWManagerWCF
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }

    public class Password
    {
        // only for the master password
        public static bool IsValid(string password)
        {
            if (!(Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success
                && Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success
                && Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,+,£,(,)]", RegexOptions.ECMAScript).Success
                && password.Length >= 12
                && Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success))
            {
                return false;
            }
            return true;
        }

        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;

            // contains numbers
            if (Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success)
                score++;
            // contains lowercase and uppercase
            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
                score++;
            // contains special character
            if (Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,+,£,(,)]", RegexOptions.ECMAScript).Success)
                score++;

            return (PasswordScore)score;
        }
    }
}