using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DomainModel.Validation
{
    public class BaseValidation
    {
        public string Persian_English_Numbers_WhiteSpaceRegex = @"^[a-zA-Z\u0600-\u06FF\s\d]+$";

        public string Persian_English_WhiteSpaceRegex = @"^[a-zA-Z\u0600-\u06FF\s]+$";

        public string LowerCaseEnglish_NumbersRegex = @"^[a-z\d]+$";

        public string PersianDateRegex = @"^[1][1-4][0-9]{2}\/((0[1-6]\/(0[1-9]|[1-2][0-9]|3[0-1]))|(0[7-9]\/(0[1-9]|[1-2][0-9]|30))|(1[0-1]\/(0[1-9]|[1-2][0-9]|30))|(12\/(0[1-9]|[1-2][0-9])))";

        public string EmailRegex = "^[a-z0-9+_.-]+@[a-zA-Z0-9.-]+$";

        public static bool CheckNationalCode(string nationalCode)
        {
            try
            {
                char[] chArray = nationalCode.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                switch (nationalCode)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":
                        return false;
                }
                int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                int num4 = num3 - ((num3 / 11) * 11);
                if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckDate(DateTime date)
        {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return false;

            return true;
        }

        public static bool CheckPassword(string password)
        {
            if (password.Length < 8)
                return false;
            if (!password.Any(i => char.IsUpper(i)))
                return false;
            //string specialCharacters = @"!@#$%^&*()_+=|\?.<>:'{}[];`~";

            //if (!specialCharacters.Any(i => password.Contains(i)))
            //    return false;
            return true;
        }
    }
}
