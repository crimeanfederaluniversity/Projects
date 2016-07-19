<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OMRUserRatingPage.aspx.cs" Inherits="Rank.Forms.OMRUserRatingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Рейтинг сотрудников КФУ</h3>
    <span style="font-size: medium"><br />
    По структурному подразделению:<br />
    <asp:Label ID="Label2" runat="server" Text="Академия" Visible="False"></asp:Label>
&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; По виду сотрудника:<br /></span>
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px"  Width="250px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
    <asp:DropDownList ID="DropDownList6" runat="server" Height="17px" Width="249px">
    </asp:DropDownList>
    <br />
    <span style="font-size: medium">
    <asp:Label ID="Label3" runat="server" Text="Факультет" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Выберите должность:</span><br />
    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="25px"  Width="250px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;<asp:DropDownList ID="DropDownList7" runat="server" Height="17px" Width="249px">
    </asp:DropDownList>
    <br />
    <span style="font-size: medium">
    <asp:Label ID="Label4" runat="server" Text="Кафедра" Visible="False"></asp:Label>
    </span>
    <br />
    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Height="25px" Width="250px" Visible="False" >
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Поиск" />
    <br />
    <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" OnRowDataBound ="OnRowDataBound"  >
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                    <ItemTemplate>
                        <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>'  Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Фамилия Имя Отчество" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="fio" runat="server" Text='<%# Bind("fio") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Академия" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="firstlvl" runat="server" Text='<%# Bind("firstlvl") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Институт/факультет" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="secondlvl" runat="server" Text='<%# Bind("secondlvl") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Кафедра" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="thirdlvl" runat="server" Text='<%# Bind("thirdlvl") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Должность" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="position" runat="server" Text='<%# Bind("position") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Рейтинг" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                         <asp:Label ID="point" runat="server" Text='<%# Bind("point") %>'  Visible="True"></asp:Label>                                    
                    </ItemTemplate>
                </asp:TemplateField>
        
            </Columns>
        </asp:GridView>
         </asp:Content>
