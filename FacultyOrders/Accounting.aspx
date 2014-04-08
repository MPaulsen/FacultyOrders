<%@ Page Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="true" CodeBehind="Accounting.aspx.cs" Inherits="FacultyOrders.Accounting"  %>




<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="Body">
    <div> <br />
        <asp:GridView runat="server" ID="grdOrders" AllowSorting="true" AutoGenerateColumns="false" OnSorting="grdOrders_Sorting" OnRowCommand="gv_RowCommand">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="ID" SortExpression="orderID" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="OrderRequestDate" HeaderText="Date Requested" SortExpression="Order_Request_Date" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="URGENT" HeaderText="Urgent" SortExpression="Urgent" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="RequestorEmail" HeaderText="Email" SortExpression="Requestor_Email" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="AccountNumber" HeaderText="Acct Number" SortExpression="Account_Number" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="ComputerPurchase" HeaderText="Computer?" SortExpression="Computer_Purchase" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="ItemDesc" HeaderText="Description" SortExpression="Item_Desc" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="PreOrderNotes" HeaderText="PreOrder Notes" SortExpression="Pre_Order_Notes" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="ApprovalDate" HeaderText="Approved" SortExpression="Approval_Date" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="AccountCode" HeaderText="Acct Code" SortExpression="Account_Code" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="PO_Number" HeaderText="PO Number" SortExpression="PO_Number" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="PurchaseDate" HeaderText="Purchased" SortExpression="Purchase_Date" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="PostOrderNotes" HeaderText="PostOrder Notes" SortExpression="Post_Order_Notes" ItemStyle-CssClass="col"/>
                <asp:BoundField DataField="ReceiveDate" HeaderText="Received" SortExpression="Receive_Date" ItemStyle-CssClass="col"/>
                <asp:ButtonField Text="Edit" CommandName="cmdEdit"/>
                <asp:ButtonField Text="Approve" CommandName="cmdApprove"/>
            </Columns>
        </asp:GridView>
    </div>
  </asp:Content>
