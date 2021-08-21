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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Table"] != null)
            {
                TableRow[] rows = (TableRow[]) Session["Table"];
                Table1.Rows.AddRange(rows);
                DropDownList[] dropDownList = (DropDownList[])Session["DropDownList"];

                dropDownList[0].SelectedIndexChanged += new EventHandler(DropDownList4_SelectedIndexChanged);
                dropDownList[1].SelectedIndexChanged += new EventHandler(DropDownList5_SelectedIndexChanged);
                dropDownList[2].SelectedIndexChanged += new EventHandler(DropDownList6_SelectedIndexChanged);
                dropDownList[3].SelectedIndexChanged += new EventHandler(DropDownList7_SelectedIndexChanged);
                dropDownList[4].SelectedIndexChanged += new EventHandler(DropDownList8_SelectedIndexChanged);
            }

            if (!Page.IsPostBack)
            {
                Session["Button2"] = false;
                Table1.Rows.Clear();
                Table1.Visible = false;

                TableRow firstRow = new TableRow();
                firstRow.Cells.Add(new TableCell());
                firstRow.Cells[0].ColumnSpan = 5;
                firstRow.Cells[0].Text = "String Creation Tool";
                TableRow hRow = new TableRow();
                hRow.Cells.Add(WebUtils.ReturnCell("Character 1"));
                hRow.Cells.Add(WebUtils.ReturnCell("Character 2"));
                hRow.Cells.Add(WebUtils.ReturnCell("Character 3"));
                hRow.Cells.Add(WebUtils.ReturnCell("Character 4"));
                hRow.Cells.Add(WebUtils.ReturnCell("Character 5"));

                TableRow row1 = new TableRow();
                WebUtils.FillRowWithCells(row1, 5);

                DropDownList DropDownList4 = new DropDownList() { CssClass = "dropdown-toggle dropdown-menu-dark" };
                DropDownList DropDownList5 = new DropDownList() { CssClass = "dropdown-toggle dropdown-menu-dark" };
                DropDownList DropDownList6 = new DropDownList() { CssClass = "dropdown-toggle dropdown-menu-dark" };
                DropDownList DropDownList7 = new DropDownList() { CssClass = "dropdown-toggle dropdown-menu-dark" };
                DropDownList DropDownList8 = new DropDownList() { CssClass = "dropdown-toggle dropdown-menu-dark" };

                TextBox TextBox4 = new TextBox() { Visible = false, Width = 20, MaxLength = 1};
                TextBox TextBox5 = new TextBox() { Visible = false, Width = 20, MaxLength = 1};
                TextBox TextBox6 = new TextBox() { Visible = false, Width = 20, MaxLength = 1};
                TextBox TextBox7 = new TextBox() { Visible = false, Width = 20, MaxLength = 1};
                TextBox TextBox8 = new TextBox() { Visible = false, Width = 20, MaxLength = 1};

                DropDownList4.AutoPostBack = true;
                DropDownList5.AutoPostBack = true;
                DropDownList6.AutoPostBack = true;
                DropDownList7.AutoPostBack = true;
                DropDownList8.AutoPostBack = true;

                WebUtils.FillDropDownLists(DropDownList4);
                WebUtils.FillDropDownLists(DropDownList5);
                WebUtils.FillDropDownLists(DropDownList6);
                WebUtils.FillDropDownLists(DropDownList7);
                WebUtils.FillDropDownLists(DropDownList8);

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

                row1.Cells[0].Controls.Add(dropDownLists[0]);
                row1.Cells[1].Controls.Add(dropDownLists[1]);
                row1.Cells[2].Controls.Add(dropDownLists[2]);
                row1.Cells[3].Controls.Add(dropDownLists[3]);
                row1.Cells[4].Controls.Add(dropDownLists[4]);

                Table1.Rows.Add(firstRow);
                Table1.Rows.Add(hRow);
                Table1.Rows.Add(row1);

                TableRow row3 = new TableRow();
                WebUtils.FillRowWithCells(row3, 5);

                row3.Cells[0].Controls.Add(TextBox4);
                row3.Cells[1].Controls.Add(TextBox5);
                row3.Cells[2].Controls.Add(TextBox6);
                row3.Cells[3].Controls.Add(TextBox7);
                row3.Cells[4].Controls.Add(TextBox8);

                TextBox[] textboxes = new TextBox[5];
                textboxes[0] = TextBox4;
                textboxes[1] = TextBox5;
                textboxes[2] = TextBox6;
                textboxes[3] = TextBox7;
                textboxes[4] = TextBox8;

                Session["TextBoxList"] = textboxes;
                row3.Visible = false;
                Table1.Rows.Add(row3);

                Session.Add("DropDownList", dropDownLists);

                TableRow[] rows = new TableRow[Table1.Rows.Count];
                Table1.Rows.CopyTo(rows, 0);
                Session.Remove("Table");
                Session.Add("Table", rows);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    const string CFd = "App_Data/Names.txt";
                    const string CFdTwo = "App_Data/Surnames.txt";
                    const string CFr = "App_Data/Results.txt";

                    Random random = new Random();

                    DropDownList[] dropDownLists = (DropDownList[])Session["DropDownList"];
                    TextBox[] textBoxes = (TextBox[])Session["TextBoxList"];

                    string[] allNames = File.ReadAllLines(Server.MapPath(CFd));
                    string[] allSurnames = File.ReadAllLines(Server.MapPath(CFdTwo));

                    List<string> Names = InOutUtils.ReadFile(allNames);
                    List<string> Surnames = InOutUtils.ReadFile(allSurnames);


                    bool integer = int.TryParse(TextBox1.Text, out int n);

                    char punctuation = ' ';

                    List<string> Lines = TaskUtils.CollectLines(random, DropDownList1, DropDownList2, DropDownList3, Names, Surnames, punctuation, n, dropDownLists, (bool)Session["Button2"], Table1.Visible, textBoxes, TextBox2);
                    string[] WrittenLines = InOutUtils.FormLinesToWrite(WebUtils.CheckCheckbox(CheckBox1), Lines);
                    File.WriteAllLines(Server.MapPath(CFr), WrittenLines);

                    FileStream fs = null;
                    fs = File.OpenRead(Server.MapPath("App_Data/" + CFr.Remove(0, 9)));
                    byte[] temp = new byte[fs.Length];
                    fs.Read(temp, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    Response.AddHeader("Content-disposition", "attachment; filename=" + CFr.Remove(0, 9));
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(temp);
                    Response.End();


                }
            }

            catch (Exception ex)
            {
                File.WriteAllText(Server.MapPath("App_Data/" + "log.txt"), ex.Message + " " + ex.StackTrace);
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
            Session["Button2"] = true;
            if (Table1.Visible && punctuationInput.Visible)
            {
                Table1.Visible = false;
                punctuationInput.Visible = false;
                DropDownList1.Items.Remove("Custom String");
                DropDownList2.Items.Remove("Custom String");
                DropDownList3.Items.Remove("Custom String");
            }

            else
            {
                Table1.Visible = true;
                punctuationInput.Visible = true;
                DropDownList1.Items.Add("Custom String");
                DropDownList2.Items.Add("Custom String");
                DropDownList3.Items.Add("Custom String");
            }

        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList[] dropDownList = (DropDownList[])Session["DropDownList"];
            TextBox[] textBoxes = (TextBox[])Session["TextBoxList"];
            if (dropDownList[0].SelectedValue == "Constant")
            {
                Table1.Rows[3].Visible = true;
                textBoxes[0].Visible = true;
            }

            if (!(dropDownList[0].SelectedValue == "Constant") && Table1.Rows[3].Cells[0].Visible)
            {
                textBoxes[0].Visible = false;
            }

            if (WebUtils.CheckSelectedItems(dropDownList, "Constant"))
            {
                Table1.Rows[3].Visible = false;
            }


        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList[] dropDownList = (DropDownList[])Session["DropDownList"];
            TextBox[] textBoxes = (TextBox[])Session["TextBoxList"];
            if (dropDownList[1].SelectedValue == "Constant")
            {
                Table1.Rows[3].Visible = true;
                textBoxes[1].Visible = true;
            }

            if (!(dropDownList[1].SelectedValue == "Constant") && Table1.Rows[3].Cells[1].Visible)
            {
                textBoxes[1].Visible = false;
            }

            if (WebUtils.CheckSelectedItems(dropDownList, "Constant"))
            {
                Table1.Rows[3].Visible = false;
            }
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList[] dropDownList = (DropDownList[])Session["DropDownList"];
            TextBox[] textBoxes = (TextBox[])Session["TextBoxList"];
            if (dropDownList[2].SelectedValue == "Constant")
            {
                Table1.Rows[3].Visible = true;
                textBoxes[2].Visible = true;
            }

            if (!(dropDownList[2].SelectedValue == "Constant") && Table1.Rows[3].Cells[2].Visible)
            {
                textBoxes[2].Visible = false;
            }

            if (WebUtils.CheckSelectedItems(dropDownList, "Constant"))
            {
                Table1.Rows[3].Visible = false;
            }
        }

        protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList[] dropDownList = (DropDownList[])Session["DropDownList"];
            TextBox[] textBoxes = (TextBox[])Session["TextBoxList"];
            if (dropDownList[3].SelectedValue == "Constant")
            {
                Table1.Rows[3].Visible = true;
                textBoxes[3].Visible = true;
            }

            if (!(dropDownList[3].SelectedValue == "Constant") && Table1.Rows[3].Cells[3].Visible)
            {
                textBoxes[3].Visible = false;
            }

            if (WebUtils.CheckSelectedItems(dropDownList, "Constant"))
            {
                Table1.Rows[3].Visible = false;
            }
        }

        protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList[] dropDownList = (DropDownList[])Session["DropDownList"];
            TextBox[] textBoxes = (TextBox[])Session["TextBoxList"];
            if (dropDownList[4].SelectedValue == "Constant")
            {
                Table1.Rows[3].Visible = true;
                textBoxes[4].Visible = true;
            }

            if (!(dropDownList[4].SelectedValue == "Constant") && Table1.Rows[3].Cells[4].Visible)
            {
                textBoxes[4].Visible = false;
            }

            if (WebUtils.CheckSelectedItems(dropDownList, "Constant"))
            {
                Table1.Rows[3].Visible = false;
            }
        }


    }
}