using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RamGecTools;

namespace ADUK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // http://keyboardmousehooks.codeplex.com/
            RamGecTools.KeyboardHook keyboardHook = new RamGecTools.KeyboardHook();
            keyboardHook.KeyDown += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            keyboardHook.Install();
        }

        private void keyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            Console.WriteLine(key.GetHashCode());
            switch (key.GetHashCode())
            {
                case 127: // F16
                    System.Diagnostics.Process.Start("calc");
                    break;
                case 128: // F17
                    break;
                case 129: // F18
                    break;
                case 130: // F19
                    sleep();
                    break;
            }
        }

        private void sleep()
        {
            //Processオブジェクトを作成
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.UseShellExecute = false;
            //ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = @"/c rundll32.exe PowrProf.dll,SetSuspendState";

            //起動
            p.Start();

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();
            p.Close();
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADUK.Visible = false;
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true; // フォームが閉じるのをキャンセル
                this.Visible = false; // フォームの非表示
            }
        }

        private void ADUK_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true; // フォームの表示
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal; // 最小化をやめる
            }
            this.Activate(); // フォームをアクティブにする
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
