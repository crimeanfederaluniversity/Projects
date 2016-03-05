﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Chancelerry.kanz.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="GridViewRegisters" AutoGenerateColumns="False" runat="server" OnRowCommand="GridViewRegisters_RowCommand"/>
    

    <br />
    <asp:Button ID="DictionaryEdit" runat="server" Text="Редактирование справочников" class="centered-button" OnClick="DictionaryEdit_Click"/>
    

    <br />
        
    <script>
        window.onbeforeunload = showLoadingScreen;
    </script>

    <style>
/* Move down content because we have a fixed navbar that is 50px tall */
body {
    padding-top: 50px;
    padding-bottom: 20px;
   
}

/* Wrapping element */
/* Set some basic padding to keep content from hitting the edges */
.body-content {
    padding-left: 15px;
    padding-right: 15px;
}

/* Override the default bootstrap behavior where horizontal description lists 
   will truncate terms that are too long to fit in the left column 
*/
.dl-horizontal dt {
    white-space: normal;
}

/* Set widths on the form inputs since otherwise they're 100% wide */
input[type="text"],
input[type="password"],
input[type="email"],
input[type="tel"],
input[type="select"] {
    max-width: 280px;
}

/* Responsive: Portrait tablets and up */
@media screen and (min-width: 768px) {
    .jumbotron {
        margin-top: 20px;
    }
    .body-content {
        padding: 0;
    }
}


/*
    GLOBAL
 */

html, body {
    background: #E7E4DB;
}

a {
    color: black;
}

a:hover {
    color: #441C11;
}

hr {
    border-top: 1px solid #70463A;
}

.center {
    margin: 0 25%;
    width: 50%;
}

.float-left {
    float: left;
}

.float-right {
    float: right;
}

.navbar {
    background: #70463A;
}

.navbar-inverse .navbar-brand,
.navbar-inverse .navbar-nav > li > a {
    color: white;
}

.navbar-inverse .navbar-brand:hover,
.navbar-inverse .navbar-nav > li > a:hover {
    color: #441C11;
}

.custom-th {
    text-align: center;
    color: white;
    font-family: sans-serif;
    text-transform: uppercase;
    background: #70463A;
    border: 1px dashed;
}

.custom-th td {
    padding: 5px;
}

.centered-button {
    margin: 0 25%;
    width: 50%;
}

/* trees */

.custom-tree table {
    margin-bottom: 0px;
    width:auto;
    background: none;
}


/* loading-screen */

.overlay {
    position:absolute;
    top:0;
    left:0;
    right:0;
    bottom:0;
    background-color:rgba(0, 0, 0, 0.85);
    background: url(data:;base64,iVBORw0KGgoAAAANSUhEUgAAAAIAAAACCAYAAABytg0kAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAABl0RVh0U29mdHdhcmUAUGFpbnQuTkVUIHYzLjUuNUmK/OAAAAATSURBVBhXY2RgYNgHxGAAYuwDAA78AjwwRoQYAAAAAElFTkSuQmCC) repeat scroll transparent\9; /* ie fallback png background image */
    z-index:9999;
    color:white;
}

.loader {
  font-size: 10px;
  margin-top: 20%;
  margin-left: 45%;
  text-indent: -9999em;
  width: 11em;
  height: 11em;
  border-radius: 50%;
  background: #ffffff;
  background: -moz-linear-gradient(left, #ffffff 10%, rgba(255, 255, 255, 0) 42%);
  background: -webkit-linear-gradient(left, #ffffff 10%, rgba(255, 255, 255, 0) 42%);
  background: -o-linear-gradient(left, #ffffff 10%, rgba(255, 255, 255, 0) 42%);
  background: -ms-linear-gradient(left, #ffffff 10%, rgba(255, 255, 255, 0) 42%);
  background: linear-gradient(to right, #ffffff 10%, rgba(255, 255, 255, 0) 42%);
  position: fixed;
  -webkit-animation: load3 1.4s infinite linear;
  animation: load3 1.4s infinite linear;
  -webkit-transform: translateZ(0);
  -ms-transform: translateZ(0);
  transform: translateZ(0);
}
.loader:before {
  width: 50%;
  height: 50%;
  background: #ffffff;
  border-radius: 100% 0 0 0;
  position: absolute;
  top: 0;
  left: 0;
  content: '';
}
.loader:after {
  background: #EBAD0E;
  width: 75%;
  height: 75%;
  border-radius: 50%;
  content: '';
  margin: auto;
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
}
@-webkit-keyframes load3 {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
@keyframes load3 {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}


/*
    LOGIN
 */

.col-md-8 {
    width: 100%;
}


/*
    DASHBOARD
 */

#MainContent_GridViewRegisters {
    background: none;
    margin: 30px 25% 0px 25%;
    border-collapse: collapse;
    width: 50%;
}

.header,
#MainContent_RegisterNameLabel,
#MainContent_GridViewRegisters th {
    color: #EBAD0E;
    text-align: center;
    font-size: 20px;
    border:none;
    border-bottom: 2px solid #70463A;
    text-transform: uppercase;
}

/*
    REGISTER VIEW
 */

.header,
#MainContent_RegisterNameLabel {
    display: block;
    font: bold 20px sans-serif;
    margin: 10px 25%;
    border: none;
}

#MainContent_dataTable {
    background: white;
}

#MainContent_GridViewRegisters {
    background: white;
}

#MainContent_dataTable tr {
    border-width: 1px;
}

#MainContent_dataTable td,
#MainContent_GridViewRegisters td {
    border-size: 5px;
    padding: 5px 10px 5px 10px;
    border: 1px solid;
}

#MainContent_dataTable td > input[type="text"] {
    width: 100%;
    max-width: 100%;
}

#MainContent_dataTable td {
    border-color: black;
    border-width: 1px;
}

.edit-panel {
    text-align: left;
}

/*search*/

#MainContent_SearchPanel {
    background: #E7E4DB;
    border: 1px solid black;
    border-radius:2px;
    padding: 10px;
    position: absolute;
}

/*pagination*/

.pagination {
    text-align: center;
    margin-top: 10px;
    margin-bottom: 10px;
}

/*
    CardEdit
 */

#MainContent_cardMainDiv {
    background: white;
}

#MainContent_cardMainDiv legend {
    text-align: center;
    width: 100%;
    border-bottom: 1px dashed;
    margin: 0;
}

fieldset table {
    padding-top: 20px;
    margin-bottom: 30px;
    width: 100%;
    background: #DEDEDE;
}

/*
    TableSettings
 */

.settings-table {
    width: 50%;
    margin: 0 25%;
    background: white;
}

.settings-table th {
    text-align: center;
    background: #70463A;
    color: white;
}

.settings-table input[type="text"] {
    width: 100%;
}

.settings-table input[type="checkbox"] {
    margin-left: 30px;
}
    </style>
</asp:Content>
