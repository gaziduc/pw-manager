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

        public PasswordStrenght()
        {
            isTooShort = false;
            isMissingUppercaseLetter = false;
            isMissingLowercaseLetter = false;
            isMissingNumber = false;
            isMissingSpecialCharacter = false;
        }

        public static PasswordStrenght IsValidMasterPassword(string password)
        {
            PasswordStrenght strenght = new PasswordStrenght();

            strenght.isTooShort = password.Length < 12;
            strenght.isMissingUppercaseLetter = !Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success;
            strenght.isMissingLowercaseLetter = !Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success;
            strenght.isMissingSpecialCharacter = !Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,+,£,(,)]", RegexOptions.ECMAScript).Success;
            strenght.isMissingNumber = !Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success;

            return strenght;
        }
    }
}