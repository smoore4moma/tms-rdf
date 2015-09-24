<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RdfViewer.aspx.cs" Inherits="tms_rdf.RdfViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        <strong>RDF Data Viewer</strong>
        <br />
        <br />
        Change the predicate: 
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="margin-r-10">
            <asp:ListItem>bornIn</asp:ListItem>
            <asp:ListItem>diedIn</asp:ListItem>
            <asp:ListItem>activeIn</asp:ListItem>
            <asp:ListItem Selected="True">livedIn</asp:ListItem>
            <asp:ListItem>educatedIn</asp:ListItem>
        </asp:DropDownList>
        Add a filter:&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" CssClass="margin-r-10"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="GO" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

    </div>
    </form>
</body>
</html>
