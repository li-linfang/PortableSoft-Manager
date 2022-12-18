using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using PortableSoftManager.MG.Common;
using PortableSoftManager.MG.Data;
using PortableSoftManager.MG.Entity;

namespace PortableSoftManager
{
    public partial class FrmStart : Form
    {
        List<BoxFile> boxFiles;

        public FrmStart()
        {
            InitializeComponent();

            //清空
            this.txtInput.Clear();
            this.lbInfo.Text = string.Empty;

            //获取所有快捷方式
            boxFiles = MG.Data.BoxFileData.getShortcuts();

            //设置自动完成菜单每一列的宽度
            var columnWidth = new int[] { 40, 150, 400 };

            //设置自动完成菜单尺寸
            this.AutoCompleteMenu.MaximumSize = new System.Drawing.Size(590, 200);

            if (boxFiles != null && boxFiles.Count > 0)
            {
                MulticolumnAutocompleteItem menuItem;
                for (int i = 0; i < boxFiles.Count; i++)
                {
                    //加入图标
                    this.IconImageList.Images.Add(boxFiles[i].SmallIcon);

                    //设置字段完成菜单的每一项
                    menuItem = new MulticolumnAutocompleteItem(new string[] { (i + 1).ToString().PadLeft(3, '0'), boxFiles[i].Name, boxFiles[i].AbsolutePath }, boxFiles[i].Name, true, true);
                    menuItem.ColumnWidth = columnWidth;
                    menuItem.ImageIndex = i;

                    //添加
                    this.AutoCompleteMenu.AddItem(menuItem);
                }
                menuItem = null;
            }

            //设置统一变量
            BoxConfig.STATIC_StartForm = this;
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            //回车提交
            if (e.KeyCode == Keys.Enter)
            {
                string input = this.txtInput.Text.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    BoxFile selectBoxFile = this.boxFiles.Where(o => o.Name == input).FirstOrDefault();
                    if (selectBoxFile != null)
                    {
                        bool canStart = Utils.StartFile(selectBoxFile.AbsolutePath);
                        if (canStart)
                        {
                            this.Close();
                        }
                        else
                        {
                            this.lbInfo.Text = input + "=>[无法启动]";
                        }
                        selectBoxFile = null;
                    }
                    else
                    {
                        this.lbInfo.Text = input + "=>[未找到]";
                    }
                }
                else
                {
                    this.lbInfo.Text = "=>[请输入]";
                }
                input = null;
            } else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStart_Activated(object sender, EventArgs e)
        {
            this.txtInput.Focus();
        }

        private void FrmStart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.ClearMemory();    //释放内存
        }

        private void FrmStart_Load(object sender, EventArgs e)
        {

        }
    }
}
