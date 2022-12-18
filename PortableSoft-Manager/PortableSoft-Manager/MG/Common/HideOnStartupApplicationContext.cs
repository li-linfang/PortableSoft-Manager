using System;
using System.Windows.Forms;

namespace PortableSoftManager.MG.Common
{
    public class HideOnStartupApplicationContext : ApplicationContext
    {
        private Form mainFormInternal;


        // 图标
        // <a href = "https://www.flaticon.com/free-icons/platform" title="platform icons">Platform icons created by dDara - Flaticon</a>

        // 构造函数，主窗体被存储在mainFormInternal
        public HideOnStartupApplicationContext(Form mainForm)
        {
            this.mainFormInternal = mainForm;
            this.mainFormInternal.Closed += new EventHandler(mainFormInternal_Closed);
        }

        // 当主窗体被关闭时，退出应用程序
        void mainFormInternal_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
