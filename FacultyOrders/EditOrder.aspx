﻿<%@ Page Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="FacultyOrders.EditOrder" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="Body">

    <div class="row">
        <div class="twelve columns notabs">
            <div class="titles">
                <h3>Edit Order</h3>
            </div>
            <div class="innercontent">
                <div>
                    <asp:Label ID="lblStatus" ForeColor="Red" runat="server" />
                </div>
                <div>
                    Order ID:
                    <asp:TextBox ID="txtOrderID" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div>
                    Name:
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>

                <div>
                    Email:
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegExEmailValid"
                        ControlToValidate="txtEmail"
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Invalid Email Format"
                        runat="server"></asp:RegularExpressionValidator>
                    <br />
                </div>

                <div>
                    Account Number:
                    <asp:TextBox ID="txtAccountNumber" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegExAccountValid"
                        ControlToValidate="txtAccountNumber"
                        Display="Dynamic"
                        ValidationExpression="\d+"
                        ForeColor="Red"
                        ErrorMessage="Please enter a valid Account Number"
                        runat="server"></asp:RegularExpressionValidator>
                </div>

                <div>
                    Urgent Order:
                    <asp:CheckBox ID="chkUrgent" runat="server" />
                </div>
                <div>
                    Order contains Laptops/Computers?
                    <asp:CheckBox ID="chkComp" runat="server" />
                </div>
                <div>
                    Vendor:
                    <asp:TextBox ID="txtVendor" runat="server"></asp:TextBox>
                </div>
                <div>
                    Item Description:
                <asp:TextBox ID="txtItemDesc" TextMode="MultiLine" Rows="10" Width="400px" runat="server"></asp:TextBox>
                </div>
                <div>
                    Post Order Notes:
                <asp:TextBox ID="txtPONotes" TextMode="MultiLine" Rows="5" Width="400px" runat="server"></asp:TextBox>
                </div>
                <div>
                    PO Number:
                    <asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox>
                </div>
                <div>
                    Total Order Amount: $<asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revNumber"
                        ControlToValidate="txtAmount"
                        ValidationExpression="^\d+(\.\d\d)?$"
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Please enter only numbers like 100 or 100.00"
                        runat="server"></asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:FileUpload runat="server" />
                </div>
                <div><br />
                    <asp:Button ID="btnSubmit" Text="Save Changes" OnClick="btnSubmit_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
