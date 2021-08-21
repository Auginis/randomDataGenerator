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
        private const string KEY = "FF#335&/?";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Table"] != null && Table1.Visible)
            {
                TableRow[] rows = (TableRow[]) Session["Table"];
                Table1.Rows.Clear();
                Table1.Rows.AddRange(rows);

                TableRow row1 = Table1.Rows[2];

                DropDownList[] dropDownLists = (DropDownList[])Session["DropDownList"];
                if (dropDownLists[0].SelectedValue == "Constant")
                {
                    Table1.Rows[3].Visible = true;
                    TaskUtils.FillRowWithCells(Table1.Rows[3], 5);
                    Table1.Rows[3].Cells[0].Controls.Add(new TextBox());
                }
            }

            if (!Page.IsPostBack)
            {
                DropDownList DropDownList4 = new DropDownList();
                DropDownList DropDownList5 = new DropDownList();
                DropDownList DropDownList6 = new DropDownList();
                DropDownList DropDownList7 = new DropDownList();
                DropDownList DropDownList8 = new DropDownList();

                DropDownList4.AutoPostBack = true;
                DropDownList5.AutoPostBack = true;
                DropDownList6.AutoPostBack = true;
                DropDownList7.AutoPostBack = true;
                DropDownList8.AutoPostBack = true;

                TaskUtils.FillDropDownLists(DropDownList4);
                TaskUtils.FillDropDownLists(DropDownList5);
                TaskUtils.FillDropDownLists(DropDownList6);
                TaskUtils.FillDropDownLists(DropDownList7);
                TaskUtils.FillDropDownLists(DropDownList8);

                DropDownList4.Visible = true;
                DropDownList5.Visible = true;
                DropDownList6.Visible = true;
                DropDownList7.Visible = true;
                DropDownList8.Visible = true;

                DropDownList[] dropDownLists = new DropDownList[5];
                dropDownLists[0] = DropDownList4;
                dropDownLists[1] = DropDownList5;
                dropDownLists[2] = DropDownList6;
                dropDownLists[3] = DropDownList7;
                dropDownLists[4] = DropDownList8;

                Session.Add("DropDownList", dropDownLists);
            }
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

        protected void Button2_Click(object sender, EventArgs e)
        {
                Table1.Rows.Clear();
                TableRow firstRow = new TableRow();
                firstRow.Cells.Add(new TableCell());
                firstRow.Cells[0].ColumnSpan = 5;
                firstRow.Cells[0].Text = "String Creation Tool";
                TableRow hRow = new TableRow();
                hRow.Cells.Add(TaskUtils.ReturnCell("Character 1"));
                hRow.Cells.Add(TaskUtils.ReturnCell("Character 2"));
                hRow.Cells.Add(TaskUtils.ReturnCell("Character 3"));
                hRow.Cells.Add(TaskUtils.ReturnCell("Character 4"));
                hRow.Cells.Add(TaskUtils.ReturnCell("Character 5"));

                TableRow row1 = new TableRow();
                TaskUtils.FillRowWithCells(row1, 5);

            DropDownList[] dropDownLists = (DropDownList[])Session["DropDownList"];

                row1.Cells[0].Controls.Add(dropDownLists[0]);
                row1.Cells[1].Controls.Add(dropDownLists[1]);
                row1.Cells[2].Controls.Add(dropDownLists[2]);
                row1.Cells[3].Controls.Add(dropDownLists[3]);
                row1.Cells[4].Controls.Add(dropDownLists[4]);

                Table1.Rows.Add(firstRow);
                Table1.Rows.Add(hRow);
                Table1.Rows.Add(row1);
                Table1.Rows.Add(new TableRow());
                Table1.Rows[3].Visible = false;


            if (Table1.Visible && punctuationInput.Visible)
            {
                Table1.Visible = false;
                punctuationInput.Visible = false;
            }

            else
            {
                Table1.Visible = true;
                punctuationInput.Visible = true;

                TableRow[] rows = new TableRow[Table1.Rows.Count];
                Table1.Rows.CopyTo(rows, 0);
                Session.Remove("Table");
                Session.Add("Table", rows);
            }
        }

    }
}