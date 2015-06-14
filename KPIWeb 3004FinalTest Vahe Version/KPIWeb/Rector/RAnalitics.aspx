<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" MasterPageFile="~/Site.Master" CodeBehind="RAnalitics.aspx.cs" Inherits="KPIWeb.Rector.RAnalitics" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
   
    <div>    <h2>Выберите целевые показатели по нужным критериям для анализа</h2>
        <br />
        <div id="but4cent">
        <asp:Button ID="Button4" runat="server" Text="По всем показателям" Width="100%" Height="70px" OnClick="Button4_Click" />
        </div>
 
&nbsp;&nbsp;&nbsp;   
        <br />
        <style>
            .NoSkin
            { 
                display:inline;
                top:0px;
                border-style:none;
                border-width:0px;
             
                
            }
            .NoSkin tr th
            {
                visibility:hidden;
            }
                        .NoSkin th
            {
               visibility:hidden;
                            border-width: 0px;
            }

         
           .table {

              
               border-width:15px;
               background-color: #ffffff;
               opacity: 0.9;
               }
              .table th {
                text-align: center;
            }
            </style>
         
        <table width="100%" border="1" class="table">
          <tr>
            <th>    
                По группе показателей:   
            </th>
            <th>  
                По проректорам:
            </th>
            <th>       
                По произвольному набору показателей:
            </th>
          </tr>

          <tr >
            <td >    
                 
                <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="NoSkin">
                    <Columns> 
                    <asp:TemplateField HeaderText="1">
                      <ItemTemplate>
                          <asp:Button ID="IndicatorClass" runat="server"  Text='<%# Eval("IndicatorClassName") %>' CommandArgument='<%# Eval("IndicatorClassID") %>' Width="200px"  
                          OnClick="ButtonClassClick" />           
                          <br />                                         
                       </ItemTemplate>                 
                 </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
                       
            </td>
            <td >        
                <asp:GridView ID="GridView2" AutoGenerateColumns="false" runat="server" CssClass="NoSkin">
                    <Columns> 
                    <asp:TemplateField HeaderText="2">
                      <ItemTemplate>
                          <asp:Button ID="Prorector" runat="server" Text='<%# Eval("ProrectorName") %>' CommandArgument='<%# Eval("ProrectorID") %>' Width="200px"  
                          OnClick="ButtonProrectorClick" />   
                          <br />                                                  
                       </ItemTemplate>                     
                 </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
                  
            </td>
            <td>       
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="Button5" runat="server" Text="Показать отмеченное" OnClick="Button5_Click" />
            </td>
          </tr>
        </table>
        <br />
        <br />    
    </div>

</asp:Content>