using COMMPortLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RFASKFreqCurrentScanLib
{
    public partial class RFASKFreqCurrentScan
    {
        #region 变量定义
        /// <summary>
		/// 使用的通信端口
		/// </summary>
		private COMMPort usedPort = null;

        /// <summary>
        /// 使用的窗体
        /// </summary>
        private Form usedForm = null;

        #endregion

        #region 属性定义

        /// <summary>
        /// 属性只读
        /// </summary>
        public COMMPort m_COMMPort
        {
            get
            {
                return this.usedPort;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public RFASKFreqCurrentScan()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="useForm"></param>
        /// <param name="usePort"></param>
        /// <param name="cbb"></param>
        /// <param name="msg"></param>
        public RFASKFreqCurrentScan(Form useForm, COMMPort usePort=null, ComboBox cbb = null, RichTextBox msg = null)
        {
            if (this.usedForm==null)
            {
                this.usedForm = new Form();
            }
            this.usedForm = useForm;

            if (usePort!=null)
            {
                if (this.usedPort==null)
                {
                    this.usedPort = new COMMPort();
                }
                this.usedPort = usePort;
            }
        }

        #endregion

        #region 析构函数

        /// <summary>
        /// 
        /// </summary>
        ~RFASKFreqCurrentScan()
        {
            
        }

        #endregion

        #region 函数定义

        #endregion

        #region 事件定义
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="cbb"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual int DevicesChangedEvent(ref Message m, ComboBox cbb = null, RichTextBox msg = null)
        {
            if (this.usedPort != null)
            {
                return this.usedPort.DevicesChangedEvent(ref m, cbb, msg);
            }
            return 0;
        }
        #endregion

    }
}
