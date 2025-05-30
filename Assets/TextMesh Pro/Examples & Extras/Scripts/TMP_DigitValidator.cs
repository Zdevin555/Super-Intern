﻿using UnityEngine;
using System;


namespace TMPro
{
       [Serializable]
     public class TMP_DigitValidator : TMP_InputValidator
    {
         public override char Validate(ref string text, ref int pos, char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                text += ch;
                pos += 1;
                return ch;
            }

            return (char)0;
        }
    }
}
