<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlPage.aspx.cs" Inherits="Chancelerry.kanz.ControlPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="menu1">
  <br id="tab2"/><br id="tab3"/>
  <a href="#tab1">Шаблон 1</a><a href="#tab2">Шаблон 2</a><a href="#tab3">Шаблон 3</a>
  <div>
      

  </div>
  <div>
      

  </div>
  <div>
      

  </div>
</div>
    
<style>
#tab2, #tab3 {position: fixed; }

.menu1 > a,
.menu1 #tab2:target ~ a:nth-of-type(1),
.menu1 #tab3:target ~ a:nth-of-type(1),
.menu1 > div { padding: 5px; border: 1px solid #aaa; }

.menu1 > a { line-height: 28px; background: #fff; text-decoration: none; }

#tab2,
#tab3,
.menu1 > div,
.menu1 #tab2:target ~ div:nth-of-type(1),
.menu1 #tab3:target ~ div:nth-of-type(1) {display: none; }

.menu1 > div:nth-of-type(1),
.menu1 #tab2:target ~ div:nth-of-type(2),
.menu1 #tab3:target ~ div:nth-of-type(3) { display: block; }

.menu1 > a:nth-of-type(1),
.menu1 #tab2:target ~ a:nth-of-type(2),
.menu1 #tab3:target ~ a:nth-of-type(3) { border-bottom: 2px solid #fff; }
</style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TableContent" runat="server">
</asp:Content>
