<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionSchedulePage.aspx.cs" Inherits="ProductionSchedulePage" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="jombotron text-center">
                <img src ="data:image/png;base64,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 "
                    alt="ACQBUILT LOGO" />
            </div>
            <div class="jombotron text-center">
                <h1> PRODUCTION SCHEDULE</h1>
                <p> Select Date and Resource Group to Load Schedule</p>
            </div>
        </div>
<div class="container">

            Select the Resource Group<br />
                <asp:DropDownList ID="DropDownList1" runat="server" Width="249px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            <br />
            <br />

            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" OnSelectionChanged="Calendar1_SelectionChanged" Width="350px" jum>
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>

            <p>&nbsp;Job List</p>
            <asp:ListBox ID="ListBox1" runat="server" Height="151px" Width="340px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
            <br />
            Operation<br />
            <asp:TextBox ID="TextBox2" runat="server" Height="22px" Width="177px"></asp:TextBox>
            <br />
            Instruction<br />
            <asp:TextBox ID="TextBox3" runat="server" Width="177px"></asp:TextBox>
            <br />
            CrewName/Work Area<br />
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <br />
            Allocated Qty<br />

            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>

            <br />
            UOM<br />
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            <br />
            Comment<br />
            <asp:TextBox ID="TextBox9" runat="server" Height="50px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Go Back" Width="98px" />

         </div>
    </form>
</body>
</html>
