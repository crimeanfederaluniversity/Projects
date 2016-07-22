<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseCntrlTempl.aspx.cs" Inherits="Chancelerry.kanz.ChooseCntrlTempl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:LinkButton ID="ControlLink1" runat="server" OnClick="ControlLink1_Click">Вх. Статистика по контрольным документам. Выборка по дате контроля.</asp:LinkButton>
    <br />
    <asp:LinkButton ID="ControlLink2" runat="server" OnClick="ControlLink2_Click">Вх. Статистика по контрольным документам. Выборка по дате поступления.</asp:LinkButton>
    <br />
    <asp:LinkButton ID="ControlLink3" runat="server" OnClick="ControlLink3_Click">Вх. Статистика по поступившим документам(с учетом движения докуменитов). Выборка по дате поступления.</asp:LinkButton>
    <br />
     <asp:LinkButton ID="ControlLink4" runat="server" OnClick="ControlLink4_Click" >Вх. Статистика по резолюциям (с учетом движения докуменитов). Выборка по дате передачи.</asp:LinkButton>
    <br />
     <asp:LinkButton ID="ControlLink5" runat="server" OnClick="ControlLink5_Click" >Приказы. Статистика по контрольным приказам.</asp:LinkButton>
    <br />
     <asp:LinkButton ID="ControlLink6" runat="server" Visible="False" ></asp:LinkButton>
    <br />
    </asp:Content>
