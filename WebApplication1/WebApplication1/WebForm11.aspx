<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm11.aspx.cs" Inherits="WebApplication1.WebForm11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Transfer amount from A to B" OnClick="Button1_Click" Width="277px" />
    </div>
    </form>
</body>
</html>
