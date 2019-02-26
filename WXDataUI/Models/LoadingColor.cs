using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXDataUI.Models
{
    public static class LoadingColor
    {
        public static string[] Colors = new string[]{
            "rgba(255, 108, 96, 1)",
            "rgba(255, 153, 78, 1)",
            "rgba(245, 217, 74, 1)"
        };
        public static int Index = 0;

        public static bool ChangeColor = true;

        public static string GetColor()
        {
            if (ChangeColor)
            {
                Index = new Random().Next(0, Colors.Length);
                ChangeColor = false;
            }else
            {
                ChangeColor = true;
            }
            return Colors[Index];
        }
    }
}