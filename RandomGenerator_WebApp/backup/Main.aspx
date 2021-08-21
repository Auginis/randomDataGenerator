<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="RandomGenerator_WebApp.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <title>Random Generator</title>
    <style>
        .input {
            width: 65%;
        }

        .int {
            width: 15%;
        }
        .container {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .checkbox {
            width: 25%;
        }

        .italic {
            font-style: italic;
        }
        .padding {
            padding-left: 85%;
            padding-top: 34%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class ="container text-center">
            <div>
                <h2>Random Information Generator</h2>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="White" CssClass="btn btn-danger" DisplayMode="List" />
                <br />
                <h3>Mold your own data!</h3>
                <p class="italic">Choose what you need and press the button.</></p>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas" Height="30px" Width="150px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="dropdown-toggle dropdown-menu-dark" ForeColor="White">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas" Height="30px" Width="150px" CssClass="dropdown-toggle dropdown-menu-dark" ForeColor="White">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas" Height="30px" Width="150px" CssClass="dropdown-toggle dropdown-menu-dark" ForeColor="White">
        </asp:DropDownList>
        <br />
            <br />
            <div class="input-group">
            <span class="input-group-text input btn-dark">Amount of lines (between 1 and 1000)</span>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="input-group-text int" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </div>
        <br />
            <div class="input-group-text checkbox btn-dark">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Enumerate?" />
            </div>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate" CssClass="btn btn-primary btn-lg" />
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/Meniu.xml" OnTransforming="XmlDataSource1_Transforming"></asp:XmlDataSource>
        </p>
            </div>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TextBox1" Display="None" ErrorMessage="Enter the amount of lines needed (integer)" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="TextBox1" Display="None" ErrorMessage="Too much or too low! Get it fixed." OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
    </div>
        <h5 class="padding" >Made by <a href="http://jukna.tech/">Augustinas Jukna</a></h5>
            </form>
</body>
</html>
