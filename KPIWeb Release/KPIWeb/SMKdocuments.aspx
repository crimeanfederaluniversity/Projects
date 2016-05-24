<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SMKdocuments.aspx.cs" Inherits="KPIWeb.SMKdocuments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>&nbsp;<span style="font-size: 30px"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </span></h2>
    <div>
        <span style="font-size: 20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ОСНОВОПОЛАГАЮЩИЕ ДОКУМЕНТЫ СИСТЕМЫ МЕНЕДЖМЕНТА КАЧЕСТВА</span><br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >
            <Columns>
               
                <asp:TemplateField HeaderText="                       Наименование документа                     " HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="DocName" runat="server" Text='<%# Bind("DocName") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Код документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="DocNumber" runat="server" Text='<%# Bind("DocNumber") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Дата размещения документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="CreateDate" runat="server" Text='<%# Bind("CreateDate") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Название документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle"  Visible = "False" >
                    <ItemTemplate>
                        <asp:Label ID="DocLink" runat="server" Text='<%# Bind("DocLink") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                  
                <asp:TemplateField Visible="true"   HeaderText="Действующая версия">
                        <ItemTemplate >                          
                            <asp:Image ID="Active" Width="20px" Height="20px" ImageUrl="~/Director/check.jpg" Visible='<%# Eval("Active") %>'  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>  
                <asp:TemplateField HeaderText="Скачать документ">
                    <ItemTemplate>
                        <asp:Button ID="ViewDocumentButton" runat="server" CommandName="Select" Text="Скачать" Width="150px"  CommandArgument='<%# Eval("DocLink") %>' OnClick="ViewDocumentButtonClick"  />
                    </ItemTemplate>
                </asp:TemplateField>            
            </Columns>
        </asp:GridView>
        </br>
        <span style="font-size: 20px">
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ДОКУМЕНТАЦИЯ ПО ПРОЦЕССАМ СИСТЕМЫ МЕНЕДЖМЕНТА КАЧЕСТВА</span>
        <br />
          <asp:GridView ID="GridView2" AutoGenerateColumns="False" runat="server" >
            <Columns>
                 <asp:TemplateField HeaderText="Группа процессов" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="GroupType" runat="server" Text='<%# Bind("GroupType") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="                      Наименование документа                        " HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="DocName" runat="server" Text='<%# Bind("DocName") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Код документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="DocNumber" runat="server" Text='<%# Bind("DocNumber") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Дата размещения документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="CreateDate" runat="server" Text='<%# Bind("CreateDate") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Название документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                    <ItemTemplate>
                        <asp:Label ID="DocLink" runat="server" Text='<%# Bind("DocLink") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                  
                <asp:TemplateField Visible="true"   HeaderText="Действующая версия">
                        <ItemTemplate >                          
                            <asp:Image ID="Active" Width="20px" Height="20px" ImageUrl="~/Director/check.jpg" Visible='<%# Eval("Active") %>'  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>  
                <asp:TemplateField HeaderText="Скачать документ">
                    <ItemTemplate>
                        <asp:Button ID="ViewDocumentButton" runat="server" CommandName="Select" Text="Скачать" Width="150px"  CommandArgument='<%# Eval("DocLink") %>' OnClick="ViewDocumentButtonClick"  />
                    </ItemTemplate>
                </asp:TemplateField>            
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
