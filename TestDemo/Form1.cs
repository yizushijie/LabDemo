
using COMMPortLib;
using System;
using System.Drawing;
using System.Management;
using System.Threading;
using System.Windows.Forms;
namespace TestDemo
{
    public partial class Form1 : Form
    {
	    private COMMPort _usedPort = null;

		private bool exitTest = false;

		private long count = 0;

        public Form1()
        {
            InitializeComponent();
        }

		protected override void WndProc(ref Message m)
		{
			if (this._usedPort!=null)
			{
				this._usedPort.DevicesChangedEvent(ref m,this.comboBox1,this.richTextBox1);
			}
			base.WndProc(ref m);
		}

		private void timer_Tick_Tick(object sender, EventArgs e)
        {
			/*
            int  _return= this._usedPort.Init(this.comboBox1);
            if (_return>0)
            {
                RichTextBoxPlus.AppendTextInfoTopWithoutDateTime(this.richTextBox1, this._usedPort.m_ErrMsg,Color.Black,false);
            }
			*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this._usedPort = new SerialCOMMPort(this);
	        this._usedPort.Init(this.comboBox1, this.richTextBox1);
			this.Focus();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			
		}
		
		private void button_Open_Click(object sender, EventArgs e)
		{
			if ((this.comboBox1.Text != string.Empty) && (this.comboBox1.Text != null) && (this.comboBox1.Text != "") && (this._usedPort != null))
			{
				this._usedPort.OpenDevice(this.comboBox1.Text, this.richTextBox1);
			}
		}

		private void button_Close_Click(object sender, EventArgs e)
		{
			//if ((this.comboBox1.Text != string.Empty) && (this.comboBox1.Text != null) && (this.comboBox1.Text != "") && (this._usedPort != null))
			//{
			//	this._usedPort.CloseDevice(this.comboBox1.Text, this.richTextBox1);
			//}
		}

		private void button_Clear_Click(object sender, EventArgs e)
		{
			this.richTextBox1.Clear();
		}

		private void button_T1_Click(object sender, EventArgs e)
		{
			if ((this._usedPort != null) && (this._usedPort.IsAttached()))
			{
				byte[] temp = { 0x5A, 0x02, 0x01, 0x02 };
				byte[] res = null;
				this._usedPort.SendCmdAndReadResponse(temp, ref res, 200);
				//this._usedPort.SendCmd(ref temp);
			}
		}

		private void button_T2_Click(object sender, EventArgs e)
		{

		}

		
		private void button_Log_Click(object sender, EventArgs e)
		{
			/*
			while (true)
			{
				if (this.exitTest)
				{
					this.exitTest = false;
					break;
				}
				byte[] temp = { 0x5A, 0x00, 0x0D,0x08,0x00,0xF0,0x00,0x48,0x57,0x48,0x53,0x57,0x2E,0x30,0x30,0x01 };
				if ((this._usedPort.m_comportreceeventisfinished)&&(this._usedPort.m_comportrecebyte!=null)&&(this._usedPort.m_comportrecebyte.Count>3))
				{
					if (this._usedPort.m_comportrecebyte[2]==0x08)
					{
						temp[4] = this._usedPort.m_comportrecebyte[3];
						this._usedPort.SendCmd(ref temp);
					}
					else if(this._usedPort.m_comportrecebyte[2]==0x0B)
					{
						//---生成随机数
						byte[] rndData = GenFun.GetRandom(530, 524);
						//---重置缓存区的大小
						Array.Resize<byte>(ref temp, 530);
						//---拷贝数据
						Array.Copy(rndData,0, temp, 6, 524);
						//---命令号
						temp[3] = this._usedPort.m_comportrecebyte[2];
						//---ARM序号
						temp[4] = this._usedPort.m_comportrecebyte[3];
						//---数据序号
						temp[5] = this._usedPort.m_comportrecebyte[4];
						//---字节长度
						temp[1] = 0x02;
						temp[2] = 0x0F;
						this._usedPort.SendCmd(ref temp);
						count += 1;
						this.numericUpDown1.Value = (decimal)count;
						//---每计数1000次，清除消息控件
						if ((count%1000)==0)
						{
							this.richTextBox1.Clear();
						}
					}
					this._usedPort.m_comportreceeventisfinished = false;
					this._usedPort.m_comportrecebyte = null;
				}
				Application.DoEvents();
			}
			*/
		}
		

		private void button_Exit_Click(object sender, EventArgs e)
		{
			this.exitTest = true;
		}
		private void button_Log2_Click(object sender, EventArgs e)
		{
			//while (true)
			//{
			//	if (this.exitTest)
			//	{
			//		this.exitTest = false;
			//		break;
			//	}
			//	byte[] temp = { 0x5A, 0x00, 0x0D, 0x08, 0x00, 0xF0, 0x00, 0x48, 0x57, 0x48, 0x53, 0x57, 0x2E, 0x30, 0x30, 0x01 };
			//	if ((this._usedPort.m_comportreceeventisfinished) && (this._usedPort.m_comportrecebyte != null) && (this._usedPort.m_comportrecebyte.Count > 3))
			//	{
			//		if (this._usedPort.m_comportrecebyte[2] == 0x08)
			//		{
			//			Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
			//			//---获取0到15之间的随机数
			//			temp[15] = (byte)rnd.Next(1, 26);
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			this._usedPort.SendCmd(ref temp);
			//		}
			//		else if (this._usedPort.m_comportrecebyte[2] == 0x0B)
			//		{
			//			//---生成随机数
			//			byte[] rndData = GenFun.GetRandom(530, 524);
			//			//---重置缓存区的大小
			//			Array.Resize<byte>(ref temp, 530);
			//			//---拷贝数据
			//			Array.Copy(rndData, 0, temp, 6, 524);
			//			//---命令号
			//			temp[3] = this._usedPort.m_comportrecebyte[2];
			//			//---ARM序号
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			//---数据序号
			//			temp[5] = this._usedPort.m_comportrecebyte[4];
			//			//---字节长度
			//			temp[1] = 0x02;
			//			temp[2] = 0x0F;
			//			this._usedPort.SendCmd(ref temp);
			//			count += 1;
			//			this.numericUpDown1.Value = (decimal)count;
			//			//---每计数1000次，清除消息控件
			//			if ((count % 1000) == 0)
			//			{
			//				this.richTextBox1.Clear();
			//			}
			//		}
			//		this._usedPort.m_comportreceeventisfinished = false;
			//		this._usedPort.m_comportrecebyte = null;
			//	}
			//	Application.DoEvents();
			//}
		}
		private void button_Log3_Click(object sender, EventArgs e)
		{
			//while (true)
			//{
			//	if (this.exitTest)
			//	{
			//		this.exitTest = false;
			//		break;
			//	}
			//	byte[] temp = { 0x5A, 0x00, 0x0D, 0x08, 0x00, 0xF0, 0x00, 0x48, 0x57, 0x48, 0x53, 0x57, 0x2E, 0x30, 0x30, 0x01 };
			//	if ((this._usedPort.m_comportreceeventisfinished) && (this._usedPort.m_comportrecebyte != null) && (this._usedPort.m_comportrecebyte.Count > 3))
			//	{
			//		if (this._usedPort.m_comportrecebyte[2] == 0x08)
			//		{
			//			//Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
			//			////---获取0到15之间的随机数
			//			//temp[15] = (byte)rnd.Next(0, 15);
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			this._usedPort.SendCmd(ref temp);
			//		}
			//		else if (this._usedPort.m_comportrecebyte[2] == 0x0B)
			//		{
			//			//---生成随机数
			//			byte[] rndData = GenFun.GetRandom(530, 524);
			//			//---重置缓存区的大小
			//			Array.Resize<byte>(ref temp, 530);
			//			//---拷贝数据
			//			Array.Copy(rndData, 0, temp, 6, 524);
			//			//---命令号
			//			temp[3] = this._usedPort.m_comportrecebyte[2];
			//			//---ARM序号
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			//---数据序号
			//			temp[5] = this._usedPort.m_comportrecebyte[4];
			//			//---字节长度
			//			temp[1] = 0x02;
			//			temp[2] = 0x0F;
			//			this._usedPort.SendCmd(ref temp);
			//			count += 1;
			//			this.numericUpDown1.Value = (decimal)count;
			//			//---每计数1000次，清除消息控件
			//			if ((count % 1000) == 0)
			//			{
			//				this.richTextBox1.Clear();
			//			}
			//		}
			//		this._usedPort.m_comportreceeventisfinished = false;
			//		this._usedPort.m_comportrecebyte = null;
			//	}
			//	Application.DoEvents();
			//}
		}

		private void button_Log4_Click(object sender, EventArgs e)
		{
			//while (true)
			//{
			//	if (this.exitTest)
			//	{
			//		this.exitTest = false;
			//		break;
			//	}
			//	byte[] temp = { 0x5A, 0x00, 0x0D, 0x08, 0x00, 0xF0, 0x00, 0x48, 0x57, 0x48, 0x53, 0x57, 0x2E, 0x30, 0x30, 0x01 };
			//	if ((this._usedPort.m_comportreceeventisfinished) && (this._usedPort.m_comportrecebyte != null) && (this._usedPort.m_comportrecebyte.Count > 3))
			//	{
			//		if (this._usedPort.m_comportrecebyte[2] == 0x08)
			//		{
			//			Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
			//			//---获取0到15之间的随机数
			//			temp[15] = (byte)rnd.Next(1, 26);
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			this._usedPort.SendCmd(ref temp);
			//		}
			//		else if (this._usedPort.m_comportrecebyte[2] == 0x0B)
			//		{
			//			//---生成随机数
			//			byte[] rndData = GenFun.GetRandom(530, 524);
			//			//---重置缓存区的大小
			//			Array.Resize<byte>(ref temp, 530);
			//			//---拷贝数据
			//			Array.Copy(rndData, 0, temp, 6, 524);
			//			//---命令号
			//			temp[3] = this._usedPort.m_comportrecebyte[2];
			//			//---ARM序号
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			//---数据序号
			//			temp[5] = this._usedPort.m_comportrecebyte[4];
			//			//---字节长度
			//			temp[1] = 0x02;
			//			temp[2] = 0x0F;
			//			this._usedPort.SendCmd(ref temp);
			//			count += 1;
			//			this.numericUpDown1.Value = (decimal)count;
			//			//---每计数1000次，清除消息控件
			//			if ((count % 1000) == 0)
			//			{
			//				this.richTextBox1.Clear();
			//			}
			//		}
			//		this._usedPort.m_comportreceeventisfinished = false;
			//		this._usedPort.m_comportrecebyte = null;
			//	}
			//	Application.DoEvents();
			//}
		}

		private void button_Log5_Click(object sender, EventArgs e)
		{
			//while (true)
			//{
			//	if (this.exitTest)
			//	{
			//		this.exitTest = false;
			//		break;
			//	}
			//	byte[] temp = { 0x5A, 0x00, 0x0D, 0x08, 0x00, 0xF0, 0x00, 0x48, 0x57, 0x48, 0x53, 0x57, 0x2E, 0x30, 0x30, 0x01 };
			//	if ((this._usedPort.m_comportreceeventisfinished) && (this._usedPort.m_comportrecebyte != null) && (this._usedPort.m_comportrecebyte.Count > 3))
			//	{
			//		if (this._usedPort.m_comportrecebyte[2] == 0x08)
			//		{
			//			Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
			//			//---获取A到Z之间的随机数
			//			temp[7] = (byte)rnd.Next(65, 90);
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			this._usedPort.SendCmd(ref temp);
			//		}
			//		else if (this._usedPort.m_comportrecebyte[2] == 0x0B)
			//		{
			//			//---生成随机数
			//			byte[] rndData = GenFun.GetRandom(530, 524);
			//			//---重置缓存区的大小
			//			Array.Resize<byte>(ref temp, 530);
			//			//---拷贝数据
			//			Array.Copy(rndData, 0, temp, 6, 524);
			//			//---命令号
			//			temp[3] = this._usedPort.m_comportrecebyte[2];
			//			//---ARM序号
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			//---数据序号
			//			temp[5] = this._usedPort.m_comportrecebyte[4];
			//			//---字节长度
			//			temp[1] = 0x02;
			//			temp[2] = 0x0F;
			//			this._usedPort.SendCmd(ref temp);
			//			count += 1;
			//			this.numericUpDown1.Value = (decimal)count;
			//			//---每计数1000次，清除消息控件
			//			if ((count % 1000) == 0)
			//			{
			//				this.richTextBox1.Clear();
			//			}
			//		}
			//		this._usedPort.m_comportreceeventisfinished = false;
			//		this._usedPort.m_comportrecebyte = null;
			//	}
			//	Application.DoEvents();
			//}
		}

		private void button_Log6_Click(object sender, EventArgs e)
		{
			//while (true)
			//{
			//	if (this.exitTest)
			//	{
			//		this.exitTest = false;
			//		break;
			//	}
			//	byte[] temp = { 0x5A, 0x00, 0x0D, 0x08, 0x00, 0xF0, 0x00, 0x48, 0x57, 0x48, 0x53, 0x57, 0x2E, 0x30, 0x30, 0x01 };
			//	if ((this._usedPort.m_comportreceeventisfinished) && (this._usedPort.m_comportrecebyte != null) && (this._usedPort.m_comportrecebyte.Count > 3))
			//	{
			//		if (this._usedPort.m_comportrecebyte[2] == 0x08)
			//		{
			//			Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
			//			//---获取A到Z之间的随机数
			//			temp[7] = (byte)rnd.Next(65, 91);
			//			//---获取0到15之间的随机数
			//			temp[15] = (byte)rnd.Next(1, 26);
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			this._usedPort.SendCmd(ref temp);
			//		}
			//		else if (this._usedPort.m_comportrecebyte[2] == 0x0B)
			//		{
			//			//---生成随机数
			//			byte[] rndData = GenFun.GetRandom(530, 524);
			//			//---重置缓存区的大小
			//			Array.Resize<byte>(ref temp, 530);
			//			//---拷贝数据
			//			Array.Copy(rndData, 0, temp, 6, 524);
			//			//---命令号
			//			temp[3] = this._usedPort.m_comportrecebyte[2];
			//			//---ARM序号
			//			temp[4] = this._usedPort.m_comportrecebyte[3];
			//			//---数据序号
			//			temp[5] = this._usedPort.m_comportrecebyte[4];
			//			//---字节长度
			//			temp[1] = 0x02;
			//			temp[2] = 0x0F;
			//			this._usedPort.SendCmd(ref temp);
			//			count += 1;
			//			this.numericUpDown1.Value = (decimal)count;
			//			//---每计数1000次，清除消息控件
			//			if ((count % 1000) == 0)
			//			{
			//				this.richTextBox1.Clear();
			//			}
			//		}
			//		this._usedPort.m_comportreceeventisfinished = false;
			//		this._usedPort.m_comportrecebyte = null;
			//	}
			//	Application.DoEvents();
			//}
		}

		private void button_CheckSum_Click(object sender, EventArgs e)
		{
			//while (true)
			//{
			//	if (this.exitTest)
			//	{
			//		this.exitTest = false;
			//		break;
			//	}
			//	if ((this._usedPort != null) && (this._usedPort.IsAttached()))
			//	{
			//		byte[] temp = { 0x55, 0x02, 0x01, 0x02 };
			//		//---生成随机数
			//		byte[] rndData = GenFun.GetRandom(530, 524);
			//		//---重置缓存区的大小
			//		Array.Resize<byte>(ref temp, 530);
			//		//---拷贝数据
			//		Array.Copy(rndData, 0, temp, 6, 524);
			//		//byte[] res = null;
			//		//this._usedPort.SendCmdAndReadResponse(ref temp, ref res, 400);

			//		//---延时10ms
			//		//GenFun.Delay_ms(300);
			//		if (this._usedPort.m_comportreceeventisfinished == true)
			//		{
			//			this._usedPort.m_comportreceeventisfinished = false;
			//		}
			//		this._usedPort.SendCmd(ref temp);
			//		//---等待数据接收完成
			//		while (this._usedPort.m_comportreceeventisfinished != true)
			//		{
			//			Application.DoEvents();
			//		}
			//		int length = 0;
			//		int index = 1;
			//		//---自动调整缓存区空间的大小
			//		if (((this._usedPort.m_comportisenabledrececrc + this._usedPort.m_comportisenabledsendcrc) == 4) || ((this._usedPort.m_comportisenabledrececrc + this._usedPort.m_comportisenabledsendcrc) == 3) || (((this._usedPort.m_comportisenabledrececrc + this._usedPort.m_comportisenabledsendcrc) == 2) && (this._usedPort.m_comportisenabledsendcrc != 1) && (this._usedPort.m_comportisenabledrececrc != 1)))
			//		{
			//			length = this._usedPort.m_comportsendbyte.Count - 4;
			//			index = 3;
			//		}
			//		else if (((this._usedPort.m_comportisenabledrececrc + this._usedPort.m_comportisenabledsendcrc) == 1) || (((this._usedPort.m_comportisenabledrececrc + this._usedPort.m_comportisenabledsendcrc) == 2) && (this._usedPort.m_comportisenabledsendcrc == 1) && (this._usedPort.m_comportisenabledrececrc == 1)))
			//		{
			//			length = this._usedPort.m_comportsendbyte.Count - 1;
			//			index = 2;
			//		}
			//		//---数据的比对校验
			//		if(GenFun.memcmp(this._usedPort.m_comportrecebyte.ToArray(), this._usedPort.m_comportsendbyte.ToArray(), length, index)!=true)
			//		{
			//			this.richTextBox1.Text += "数据通信错误,测试退出!\r\n";
			//			//---下次数据的接收
			//			this._usedPort.m_comportreceeventisfinished = false;
			//			return;

			//		}
			//		count += 1;
			//		this.numericUpDown1.Value = (decimal)count;
			//		//---每计数1000次，清除消息控件
			//		if ((count % 100) == 0)
			//		{
			//			this.richTextBox1.Clear();
			//		}
			//		//---下次数据的接收
			//		this._usedPort.m_comportreceeventisfinished = false;
			//	}
			//}
			
		}

		
	}
}
