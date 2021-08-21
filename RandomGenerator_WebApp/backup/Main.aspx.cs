using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace RandomGenerator_WebApp
{
    public partial class Main : System.Web.UI.Page
    {
        private string key = "LLFF34#333";
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                const string CFd = "App_Data/Names.txt";
                const string CFdTwo = "App_Data/Surnames.txt";
                const string CFr = "App_Data/Results.txt";

                Random random = new Random();

                string[] allNames = File.ReadAllLines(Server.MapPath(CFd));
                string[] allSurnames = File.ReadAllLines(Server.MapPath(CFdTwo));

                List<string> Names = InOutUtils.ReadFile(allNames);
                List<string> Surnames = InOutUtils.ReadFile(allSurnames);
                bool integer = int.TryParse(TextBox1.Text, out int n);

                List<string> Lines = TaskUtils.CollectLines(random, DropDownList1, DropDownList2, DropDownList3, Names, Surnames, n);
                string[] WrittenLines = InOutUtils.FormLinesToWrite(TaskUtils.CheckCheckbox(CheckBox1), Lines);
                File.WriteAllLines(Server.MapPath(CFr), WrittenLines);
                         
                FileStream fs = null;
                fs = File.OpenRead(Server.MapPath("App_Data/" + CFr.Remove(0, 9)));
                byte[] temp = new byte[fs.Length];
                fs.Read(temp, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + CFr.Remove(0,9));
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(temp);
                Response.End();
                
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool integer = int.TryParse(TextBox1.Text, out int n);
            if (integer)
            {
                args.IsValid = true;
            }

            else
            {
                args.IsValid = false;
            }
        }

        protected void XmlDataSource1_Transforming(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool integer = int.TryParse(TextBox1.Text, out int n);
            if (integer && (n > 1000 || n < 1))
            {
                args.IsValid = false;
            }

            else
            {
                args.IsValid = true;
            }
        }
    }
}