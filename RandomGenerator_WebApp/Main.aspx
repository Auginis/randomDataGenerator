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
            bottom: 1%;
            right: 1%;
            position: absolute;
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
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas" Height="30px" Width="150px" CssClass="dropdown-toggle dropdown-menu-dark" ForeColor="White">
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
                <div class="row">
                    <div class="col-11" style="position: relative">
                        <div class="input-group-text checkbox btn-dark">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Enumerate?" />
            </div>
                    </div>
                    <div class="col" id ="punctuationInput" runat="server" visible ="false" style="margin-left: -360px">
                        <div class="input-group">
               <span class="input-group-text btn-dark">Punctuation (default: " ")</span>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="input-group-text int" OnTextChanged="TextBox1_TextChanged" Width="45px" MaxLength = "1"></asp:TextBox>
            </div>
                    </div>
                </div>
            <asp:Table ID="Table1" runat="server" CssClass="table table-dark table-hover" GridLines="Both" Visible="False" style="margin-top:30px">
            </asp:Table>
            <br/>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate" CssClass="btn btn-outline-dark" />
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/Meniu.xml" OnTransforming="XmlDataSource1_Transforming"></asp:XmlDataSource>
            <asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-danger" OnClick="Button2_Click" Text="Advanced" CausesValidation="False"/>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TextBox1" Display="None" ErrorMessage="Enter the amount of lines needed (integer)" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="TextBox1" Display="None" ErrorMessage="Too much or too low! Get it fixed." OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
            </div>
    </div>
        <h5 class="padding" >Made by <a href="https://www.linkedin.com/in/augustinas-jukna-94a0101b5/" target="_blank">Augustinas Jukna</a></h5>
            </form>
</body>
</html>
