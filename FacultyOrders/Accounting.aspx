<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accounting.aspx.cs" Inherits="FacultyOrders.Accounting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView runat="server" ID="grdOrders" AllowSorting="true" AutoGenerateColumns="false" OnSorting="grdOrders_Sorting">
            <Columns>
                <asp:BoundField DataField="orderID" HeaderText="ID" SortExpression="orderID"/>
                <asp:BoundField DataField="Order_Request_Date" HeaderText="Date Requested" SortExpression="Order_Request_Date"/>
                <asp:BoundField DataField="Urgent" HeaderText="Urgent" SortExpression="Urgent"/>
                <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor"/>
                <asp:BoundField DataField="Requestor_Email" HeaderText="Email" SortExpression="Requestor_Email"/>
                <asp:BoundField DataField="Account_Number" HeaderText="Acct Number" SortExpression="Account_Number"/>
                <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor"/>
                <asp:BoundField DataField="Computer_Purchase" HeaderText="Computer?" SortExpression="Computer_Purchase"/>
                <asp:BoundField DataField="Item_Desc" HeaderText="Description" SortExpression="Item_Desc"/>
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount"/>
                <asp:BoundField DataField="Pre_Order_Notes" HeaderText="PreOrder Notes" SortExpression="Pre_Order_Notes"/>
                <asp:BoundField DataField="Approval_Date" HeaderText="Approved" SortExpression="Approval_Date"/>
                <asp:BoundField DataField="Account_Code" HeaderText="Acct Code" SortExpression="Account_Code"/>
                <asp:BoundField DataField="PO_Number" HeaderText="PO Number" SortExpression="PO_Number"/>
                <asp:BoundField DataField="Purchase_Date" HeaderText="Purchased" SortExpression="Purchase_Date"/>
                <asp:BoundField DataField="Post_Order_Notes" HeaderText="PostOrder Notes" SortExpression="Post_Order_Notes"/>
                <asp:BoundField DataField="Receive_Date" HeaderText="Received" SortExpression="Receive_Date"/>
                <asp:ButtonField Text="Edit"/>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
