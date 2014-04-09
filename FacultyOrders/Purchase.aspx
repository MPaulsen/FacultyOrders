<%@ Page Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="True" CodeBehind="Purchase.aspx.cs" Inherits="FacultyOrders.Purchase" %>




<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="Body">
    <div>
        <br />
        <div class="row">
            <div class="columns notabs">
                <div class="titles">
                    <h3>Purchases</h3>
                </div>
                <div class="innercontent">
                    <asp:Label ID="lblError" runat="server" Text="Testing" />

                    <asp:RadioButtonList ID="rdoDateView" runat="server" RepeatDirection="Horizontal" RepeatLayout="table" OnSelectedIndexChanged="IndexChanged" AutoPostBack="true">

                        <asp:ListItem>All Items</asp:ListItem>
                        <asp:ListItem>Specific Date Range:</asp:ListItem>
                        <asp:ListItem>Items not ordered</asp:ListItem>
                        <asp:ListItem>Ordered, not recieved</asp:ListItem>


                    </asp:RadioButtonList>
                    <asp:Table runat="server" HorizontalAlign="Center" Visible="false" ID="tblDate">
                        <asp:TableRow HorizontalAlign="Center">
                            <asp:TableCell HorizontalAlign="Center">
                                From:<asp:TextBox ID="txtFrom" runat="server" Width="20"></asp:TextBox>
                                <asp:Calendar ID="FromCalendar" runat="server" Visible="false" Width="20" OnSelectionChanged="CalenderChange"></asp:Calendar>
                            </asp:TableCell>

                            <asp:TableCell HorizontalAlign="Center">
                                To:<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                                <asp:Calendar ID="ToCalendar" runat="server" Visible="false" Width="20"></asp:Calendar>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" runat="server" />
                            </asp:TableCell>

                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Button ID="FromCal" OnClick="FromCal_Click" runat="server" Text="Open Calender" />
                            </asp:TableCell>
                        </asp:TableRow>

                    </asp:Table>
                </div>
                <asp:GridView runat="server" ID="grdOrders" AllowSorting="true" AutoGenerateColumns="false" OnSorting="grdOrders_Sorting" OnRowCommand="gv_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="OrderID" HeaderText="ID" SortExpression="orderID" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="OrderRequestDate" HeaderText="Date Requested" SortExpression="Order_Request_Date" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="URGENT" HeaderText="Urgent" SortExpression="Urgent" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="AccountNumber" HeaderText="Acct Number" SortExpression="Account_Number" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="ComputerPurchase" HeaderText="Computer?" SortExpression="Computer_Purchase" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="ItemDesc" HeaderText="Description" SortExpression="Item_Desc" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="PreOrderNotes" HeaderText="PreOrder Notes" SortExpression="Pre_Order_Notes" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="AccountCode" HeaderText="Acct Code" SortExpression="Account_Code" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="PO_Number" HeaderText="PO Number" SortExpression="PO_Number" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="PurchaseDate" HeaderText="Purchased" SortExpression="Purchase_Date" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="PostOrderNotes" HeaderText="PostOrder Notes" SortExpression="Post_Order_Notes" ItemStyle-CssClass="col" />
                        <asp:BoundField DataField="ReceiveDate" HeaderText="Received" SortExpression="Receive_Date" ItemStyle-CssClass="col" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
