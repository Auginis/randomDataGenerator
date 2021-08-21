using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandomGenerator_WebApp
{
    public class WebUtils : System.Web.UI.Page
    {
        public static bool CheckCheckbox(CheckBox checkBox)
        {
            if (checkBox.Checked == true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static TableCell ReturnCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            return cell;
        }

        public static void FillDropDownLists(DropDownList dropDownList)
        {
            dropDownList.Items.Add("None");
            dropDownList.Items.Add("Letter");
            dropDownList.Items.Add("Digit");
            dropDownList.Items.Add("Constant");
        }

        public static void FillRowWithCells(TableRow row, int cellsAmount)
        {
            for (int i = 0; i < cellsAmount; i++)
            {
                row.Cells.Add(new TableCell());
            }
        }

        public static bool CheckSelectedItem(DropDownList dropDownList, string text)
        {
            if (dropDownList.SelectedValue == text)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static bool CheckSelectedItems(DropDownList[] dropDownLists, string text)
        {
            int count = 0;
            foreach (DropDownList list in dropDownLists)
            {
                if (list.SelectedValue != text)
                {
                    count++;
                }
            }

            if (count == dropDownLists.Count())
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static int IndexOfSelectedValue(DropDownList[] dropDownLists, string selectedValue)
        {
            for (int i = 0; i < dropDownLists.Count(); i++)
            {
                if (dropDownLists[i].SelectedValue == selectedValue)
                {
                    return i;
                }
            }

            return -1;
        }

    }
}