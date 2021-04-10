using System.Text;
using System.Text.RegularExpressions;

namespace PWManagerWCF
{
    public class PasswordStrenght
    {
        public bool isTooShort;
        public bool isMissingUppercaseLetter;
        public bool isMissingLowercaseLetter;
        public bool isMissingNumber;
        public bool isMissingSpecialCharacter;

        public PasswordStrenght(string password)
        {
            isTooShort = password.Length < 12;
            isMissingUppercaseLetter = !Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success;
            isMissingLowercaseLetter = !Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success;
            isMissingSpecialCharacter = !Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,+,£,(,)]", RegexOptions.ECMAScript).Success;
            isMissingNumber = !Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success;
        }
    }
}