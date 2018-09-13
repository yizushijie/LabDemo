using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenFuncLib
{
    public partial class GenFunc
    {
        public static bool GenFuncEqual(byte[] buffer, byte val = 0xFF)
        {
            if ((buffer == null))
            {
                return false;
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == val)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool GenFuncEqual(byte[] b1, byte[] b2)
        {
            if ((b1!=null)&&(b2!=null))
            {
                if (b1.Length!=b2.Length)
                {
                    return false;
                }
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i]!=b2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

    }
}
