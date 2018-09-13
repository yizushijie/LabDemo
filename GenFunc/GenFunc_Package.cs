using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenFuncLib
{
    public partial class GenFunc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="packageByteCount"></param>
        /// <param name="fillByte"></param>
        /// <returns></returns>
        public static byte[][] GenFuncPackageDateToPackage(byte[] dataArray, int packageByteCount, byte fillByte = 0xFF)
        {
            byte[][] _return = null;

            //判断数据包是否为偶数
            if (((packageByteCount & 0x01) != 0))
            {
                packageByteCount -= 1;
            }
            //数据分包处理
            if ((dataArray != null) && (dataArray.Length > 0))
            {
                //计算分包总数
                int packageCount = dataArray.Length / packageByteCount;
                if ((dataArray.Length % packageByteCount) != 0)
                {
                    packageCount += 1;
                }
                _return = new byte[packageCount][];
                //分包处理
                for (int i = 0; i < packageCount; i++)
                {
                    //计算当前数据包的字节字数
                    int byteCount = ((dataArray.Length - i * packageByteCount) > packageByteCount) ? packageByteCount : (dataArray.Length - i * packageByteCount);
                    if ((byteCount & 0x01) != 0)
                    {
                        byteCount += 1;
                    }
                    //构建分包数据,并处理奇数字节的数据包
                    _return[i] = new byte[byteCount];
                    _return[i][byteCount - 1] = fillByte;
                    //拷贝数据到返回数组
                    Array.Copy(dataArray, i * packageByteCount, _return[i], 0, byteCount);
                }
            }
            return _return;
        }

    }
}
