<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rstat.aspx.cs" Inherits="KPIWeb.Rector.Rstat" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div>
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" OnClick="Button222_Click" />
            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
       <div style="margin-top: 42px; position: relative" >
        <h2><%: Title %>Основные показатели КФУ</h2>
        
        <p class=MsoNormal align=center style='margin-right:-21.1pt;text-align:center'><o:p>&nbsp;</o:p></p>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=1795
 style='width:1345.9pt;margin-left:.75pt;border-collapse:collapse;mso-yfti-tbllook:
 1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:15.0pt'>
  <td width=1795 valign=top style='width:1345.9pt;padding:0cm .75pt 0cm .75pt;
  height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:-21.1pt;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:11.25pt;text-autospace:none'><b><span style='font-size:10.0pt;
  font-family:"Arial",sans-serif;color:black'>Сведения о приеме, численности
  студентов и выпускников</span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes;height:15.0pt'>
  <td width=1795 valign=top style='width:1345.9pt;padding:0cm .75pt 0cm .75pt;
  height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:-21.1pt;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:11.25pt;text-autospace:none'><b><span style='font-size:10.0pt;
  font-family:"Arial",sans-serif;color:black'>Распределение приема по уровням
  подготовки</span></b></p>
  </td>
 </tr>
</table>

<p class=MsoNormal align=center style='text-align:center'><o:p>&nbsp;</o:p></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=345 rowspan=3 style='width:258.5pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Наименование <br>
  направления <span class=GramE>подготовки,<br>
  специальности</span></span></p>
  </td>
  <td width=40 rowspan=3 style='width:30.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ строки</span></p>
  </td>
  <td width=72 rowspan=3 style='width:54.2pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано заявлений</span></p>
  </td>
  <td width=80 rowspan=3 style='width:60.2pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято*<br>
  (сумма гр. 6 – 9)</span></p>
  </td>
  <td width=273 colspan=4 style='width:204.65pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято на обучение:</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:12.0pt'>
  <td width=205 colspan=3 style='width:153.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет бюджетных ассигнований</span></p>
  </td>
  <td width=68 rowspan=2 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по договорам об оказании платных <span
  class=GramE>образователь-<br>
  <span class=SpellE>ных</span></span> услуг</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:33.0pt'>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:33.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>федерального бюджета</span></p>
  </td>
  <td width=68 valign=top style='width:51.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:33.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>бюджета субъекта Российской
  Федерации</span></p>
  </td>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:33.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>местного бюджета</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:12.0pt'>
  <td width=345 style='width:258.5pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span>
  – всего</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></b></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>10423</span></b></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3203</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2592</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>611</span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:12.0pt'>
  <td width=345 style='width:258.5pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span>
  – всего</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>05</span></b></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4066</span></b></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1250</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>633</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>617</span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;height:12.0pt'>
  <td width=345 style='width:258.5pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы магистратуры – всего</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>09</span></b></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1675</span></b></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1307</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1285</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>22</span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:6;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=345 style='width:258.5pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего по программам высшего
  образования (сумма строк 01, 05, 09)</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>13</span></b></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>16164</span></b></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5760</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4510</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1250</span></b></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'><span lang=EN-US
style='mso-ansi-language:EN-US'>&nbsp;</span></p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Распределение численности студентов и выпуска по уровням
подготовки</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=981
 style='width:735.6pt;border-collapse:collapse;mso-yfti-tbllook:1184;
 mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=306 rowspan=4 style='width:229.35pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Наименование <br>
  направления <span class=GramE>подготовки,<br>
  специальности</span></span></p>
  </td>
  <td width=36 rowspan=4 style='width:27.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ строки</span></p>
  </td>
  <td width=489 colspan=14 style='width:366.65pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов по курсам</span></p>
  </td>
  <td width=57 rowspan=4 style='width:42.55pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.4pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Числен-<br>
  <span class=SpellE>ность</span><br>
  студентов<br>
  на всех<br>
  <span class=GramE>курсах<br>
  (</span>сумма гр.<br>
  5,7,9,11,<br>
  13,15,17)</span></p>
  </td>
  <td width=93 colspan=2 rowspan=2 valign=top style='width:69.95pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.4pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Из них <span class=GramE>обучаются<br>
  (</span>из гр.19):</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:1;height:6.0pt'>
  <td width=80 colspan=2 rowspan=2 style='width:60.2pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>1 курс</span></p>
  </td>
  <td width=80 colspan=2 rowspan=2 style='width:60.2pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>2 курс</span></p>
  </td>
  <td width=80 colspan=2 rowspan=2 style='width:60.2pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>3 курс</span></p>
  </td>
  <td width=80 colspan=2 rowspan=2 style='width:60.2pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>4 курс</span></p>
  </td>
  <td width=80 colspan=2 rowspan=2 style='width:60.2pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>5 курс</span></p>
  </td>
  <td width=80 colspan=2 rowspan=2 style='width:60.2pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>6 курс</span></p>
  </td>
  <td width=7 colspan=2 rowspan=2 style='width:5.45pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:6.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:2;height:6.0pt'>
  <td width=48 rowspan=2 valign=top style='width:36.1pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  <span class=GramE>бюджет-<br>
  <span class=SpellE>ных</span></span> ас-<br>
  <span class=SpellE>сигнова</span>-<br>
  <span class=SpellE>ний</span> <span class=SpellE>фе</span>-<br>
  <span class=SpellE>дераль</span>-<br>
  <span class=SpellE>ного</span> <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span> (<span class=SpellE>сум</span>-<br>
  <span class=SpellE>ма</span> гр.6, 8,<br>
  10, 12,14, 16,18)</span></p>
  </td>
  <td width=45 rowspan=2 style='width:33.85pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:6.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об<br>
  оказании<br>
  платных<br>
  <span class=SpellE>образо</span>-<br>
  <span class=SpellE>ватель</span>-<br>
  <span class=SpellE>ных</span><br>
  услуг</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:6.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:3;height:66.0pt'>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них<br>
  за счет<br>
  <span class=SpellE><span class=GramE>бюд</span></span><span class=GramE>-<br>
  <span class=SpellE>жетных</span></span><br>
  <span class=SpellE>ассигн</span><br>
  <span class=SpellE>ований</span><br>
  <span class=SpellE>федера</span><br>
  <span class=SpellE>льного</span><br>
  <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span></span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них<br>
  за счет<br>
  <span class=SpellE><span class=GramE>бюд</span></span><span class=GramE>-<br>
  <span class=SpellE>жетных</span></span><br>
  <span class=SpellE>ассигн</span><br>
  <span class=SpellE>ований</span><br>
  <span class=SpellE>федера</span><br>
  <span class=SpellE>льного</span><br>
  <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span></span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них<br>
  за счет<br>
  <span class=SpellE><span class=GramE>бюд</span></span><span class=GramE>-<br>
  <span class=SpellE>жетных</span></span><br>
  <span class=SpellE>ассигно</span><br>
  <span class=SpellE>ваний</span><br>
  <span class=SpellE>федера</span><br>
  <span class=SpellE>льного</span><br>
  <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span></span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них<br>
  за счет<br>
  <span class=SpellE><span class=GramE>бюд</span></span><span class=GramE>-<br>
  <span class=SpellE>жетных</span></span><br>
  <span class=SpellE>ассигн</span><br>
  <span class=SpellE>ований</span><br>
  <span class=SpellE>федера</span><br>
  <span class=SpellE>льного</span><br>
  <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span></span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них<br>
  за счет<br>
  <span class=SpellE><span class=GramE>бюд</span></span><span class=GramE>-<br>
  <span class=SpellE>жетных</span></span><br>
  <span class=SpellE>ассигн</span><br>
  <span class=SpellE>ований</span><br>
  <span class=SpellE>федера</span><br>
  <span class=SpellE>льного</span><br>
  <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span></span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них<br>
  за счет<br>
  <span class=SpellE><span class=GramE>бюд</span></span><span class=GramE>-<br>
  <span class=SpellE>жетных</span></span><br>
  <span class=SpellE>ассигн</span><br>
  <span class=SpellE>ований</span><br>
  <span class=SpellE>федера</span><br>
  <span class=SpellE>льного</span><br>
  <span class=SpellE>бюд</span>-<br>
  <span class=SpellE>жета</span></span></p>
  </td>
  <td width=3 style='width:2.5pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=4 valign=top style='width:2.95pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:66.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:66.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:4;height:12.0pt'>
  <td width=306 style='width:229.35pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span>
  – всего</span></b></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3055</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2483</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4061</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2834</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2827</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1809</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2752</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1822</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>61</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>30</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=3 valign=bottom style='width:2.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=4 valign=bottom style='width:2.95pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=57 valign=bottom style='width:42.55pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>12756</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>8978</span></b></p>
  </td>
  <td width=45 valign=bottom style='width:33.85pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3778</span></b></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:5;height:12.0pt'>
  <td width=306 style='width:229.35pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span>
  – всего</span></b></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>06</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1072</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>636</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1188</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>637</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>751</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>287</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>716</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>289</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>593</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>250</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>416</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>182</span></b></p>
  </td>
  <td width=3 valign=bottom style='width:2.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=4 valign=bottom style='width:2.95pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=57 valign=bottom style='width:42.55pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4736</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2281</span></b></p>
  </td>
  <td width=45 valign=bottom style='width:33.85pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2455</span></b></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:6;height:12.0pt'>
  <td width=306 style='width:229.35pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы магистратуры – всего</span></b></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>11</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1310</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1286</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2099</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1923</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></b></p>
  </td>
  <td width=3 valign=bottom style='width:2.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=4 valign=bottom style='width:2.95pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=57 valign=bottom style='width:42.55pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3409</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3209</span></b></p>
  </td>
  <td width=45 valign=bottom style='width:33.85pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>200</span></b></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:7;mso-yfti-lastrow:yes;height:18.0pt'>
  <td width=306 valign=top style='width:229.35pt;border:solid black 1.0pt;
  border-top:none;padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><b><span style='font-size:
  7.0pt;font-family:"Arial",sans-serif;color:black'>Всего по программам высшего
  <span class=GramE>образования<br>
  (</span>сумма строк 01, 06, 11)</span></b></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>15</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5437</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4405</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>7348</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5394</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3578</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2096</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3468</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2111</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>654</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>280</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>416</span></b></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>182</span></b></p>
  </td>
  <td width=3 valign=bottom style='width:2.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=4 valign=bottom style='width:2.95pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></b></p>
  </td>
  <td width=57 valign=bottom style='width:42.55pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>20901</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>14468</span></b></p>
  </td>
  <td width=45 valign=bottom style='width:33.85pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>6433</span></b></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:18.0pt'></td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<b><span style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
mso-fareast-font-family:"Times New Roman";color:black;mso-ansi-language:RU;
mso-fareast-language:RU;mso-bidi-language:AR-SA'><br clear=all
style='page-break-before:always'>
</span></b>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Распределение приема граждан иностранных государств (в
соответствии с международными договорами<br>
Российской Федерации, с федеральными законами или установленной Правительством
Российской Федерации <span class=GramE>квотой)<br>
по</span> уровням подготовки</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=261 rowspan=2 style='width:195.65pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Наименование <br>
  направления <span class=GramE>подготовки,<br>
  специальности</span></span></p>
  </td>
  <td width=36 rowspan=2 style='width:27.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ строки</span></p>
  </td>
  <td width=72 rowspan=2 style='width:54.15pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span class=GramE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>Принято<br>
  (</span></span><span style='font-size:7.0pt;font-family:"Arial",sans-serif;
  color:black'>сумма<br>
  гр. 5 – 7)</span></p>
  </td>
  <td width=241 colspan=3 style='width:180.6pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято на обучение за счет:</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:45.0pt'>
  <td width=80 style='width:60.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:45.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>бюджетных ассигнований
  федерального бюджета</span></p>
  </td>
  <td width=80 valign=top style='width:60.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:45.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>бюджетных ассигнований бюджета
  субъекта Российской Федерации</span></p>
  </td>
  <td width=80 style='width:60.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:45.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>бюджетных ассигнований местного
  бюджета</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:12.0pt'>
  <td width=261 style='width:195.65pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span></span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>109</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>109</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:12.0pt'>
  <td width=261 style='width:195.65pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span></span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>02</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>55</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>55</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:12.0pt'>
  <td width=261 style='width:195.65pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы магистратуры</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>03</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;mso-yfti-lastrow:yes;height:18.0pt'>
  <td width=261 valign=top style='width:195.65pt;border:solid black 1.0pt;
  border-top:none;padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего по программам высшего <span
  class=GramE>образования<br>
  (</span>сумма строк 01, 02, 03)</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>04</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>164</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>164</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Движение численности студентов</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=253 rowspan=4 style='width:189.6pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'></td>
  <td width=36 rowspan=4 style='width:27.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№<br>
  строки</span></p>
  </td>
  <td width=68 rowspan=4 style='width:51.15pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span class=GramE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>Всего<br>
  (</span></span><span style='font-size:7.0pt;font-family:"Arial",sans-serif;
  color:black'>сумма<br>
  гр. 4, 7, 10)</span></p>
  </td>
  <td width=590 colspan=9 style='width:442.5pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>в том числе по программам</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:12.0pt'>
  <td width=197 colspan=3 style='width:147.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span class=SpellE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>бакалавриата</span></span></p>
  </td>
  <td width=197 colspan=3 style='width:147.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span class=SpellE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>специалитета</span></span></p>
  </td>
  <td width=197 colspan=3 style='width:147.5pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>магистратуры</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:12.0pt'>
  <td width=52 rowspan=2 style='width:39.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=144 colspan=2 style='width:108.35pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
  <td width=52 rowspan=2 style='width:39.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=144 colspan=2 style='width:108.35pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
  <td width=52 rowspan=2 style='width:39.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=144 colspan=2 style='width:108.35pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:36.0pt'>
  <td width=72 valign=top style='width:54.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:36.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет бюджетных ассигнований
  федерального бюджета</span></p>
  </td>
  <td width=72 valign=top style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:36.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по договорам об оказании платных <span
  class=GramE>образователь-<br>
  <span class=SpellE>ных</span></span> услуг</span></p>
  </td>
  <td width=72 valign=top style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:36.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет бюджетных ассигнований
  федерального бюджета</span></p>
  </td>
  <td width=72 valign=top style='width:54.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:36.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по договорам об оказании платных <span
  class=GramE>образователь-<br>
  <span class=SpellE>ных</span></span> услуг</span></p>
  </td>
  <td width=72 valign=top style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:36.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет бюджетных ассигнований
  федерального бюджета</span></p>
  </td>
  <td width=72 valign=top style='width:54.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:36.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по договорам об оказании платных <span
  class=GramE>образователь-<br>
  <span class=SpellE>ных</span></span> услуг</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;mso-yfti-lastrow:yes;height:18.0pt'>
  <td width=253 valign=top style='width:189.6pt;border:solid black 1.0pt;
  border-top:none;padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов на начало
  предыдущего учебного года (на 1 октября)</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>18690</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>11679</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>7143</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4536</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4622</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2077</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2545</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2389</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1998</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>391</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'><o:p>&nbsp;</o:p></p>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Распределение численности студентов, приема и выпуска по
источникам финансирования обучения. <br>
Целевой прием и целевое обучение. Сведения о студентах с ОВЗ и инвалидах</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=1011
 style='width:758.5pt;border-collapse:collapse;mso-yfti-tbllook:1184;
 mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=357 rowspan=2 style='width:267.9pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=40 rowspan=2 style='width:30.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ <br>
  строки</span></p>
  </td>
  <td width=205 colspan=3 style='width:153.5pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span></span></p>
  </td>
  <td width=205 colspan=3 style='width:153.5pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span></span></p>
  </td>
  <td width=205 colspan=3 style='width:153.5pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы магистратуры</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:25.5pt'>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
  <td width=68 style='width:51.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов</span></p>
  </td>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Выпуск</span></p>
  </td>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
  <td width=68 style='width:51.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов</span></p>
  </td>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Выпуск</span></p>
  </td>
  <td width=68 style='width:51.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов</span></p>
  </td>
  <td width=68 style='width:51.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:25.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Выпуск</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=357 style='width:267.9pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего (сумма строк 02 – 05)</span></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3203</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>12756</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1739</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1250</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4736</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>569</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1307</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3409</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>326</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Численность студентов <u>очной</u> формы обучения, получающих стипендии
и другие формы материальной поддержки</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:15.0pt'>
  <td width=317 rowspan=2 style='width:237.8pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=40 rowspan=2 style='width:30.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№<br>
  строки</span></p>
  </td>
  <td width=72 rowspan=2 style='width:54.2pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span class=GramE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>Всего<br>
  (</span></span><span style='font-size:7.0pt;font-family:"Arial",sans-serif;
  color:black'>сумма<br>
  граф 4 – 6)</span></p>
  </td>
  <td width=241 colspan=3 style='width:180.6pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>в том числе по программам</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:15.0pt'>
  <td width=80 style='width:60.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span class=SpellE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>бакалавриата</span></span></p>
  </td>
  <td width=80 style='width:60.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span class=SpellE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>специалитета</span></span></p>
  </td>
  <td width=80 style='width:60.2pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:15.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>магистратуры</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:12.0pt'>
  <td width=317 style='width:237.8pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов, получающих
  стипендию (хотя бы одну)</span></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>11491</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>7272</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1440</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2779</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:21.0pt'>
  <td width=317 style='width:237.8pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:21.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов, получающих
  другие формы материальной поддержки</span></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:21.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>02</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:21.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>437</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:21.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>334</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:21.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>87</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:21.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>16</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=317 style='width:237.8pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:4.5pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них за счет стипендиального
  фонда</span></p>
  </td>
  <td width=40 valign=bottom style='width:30.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>03</span></p>
  </td>
  <td width=72 valign=bottom style='width:54.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>79</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>29</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>45</span></p>
  </td>
  <td width=80 valign=bottom style='width:60.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Численность студентов, прием и выпуск по категориям льготного
обеспечения <u>очной</u> формы обучения</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:24.0pt'>
  <td width=181 rowspan=3 style='width:135.45pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=40 rowspan=3 style='width:30.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№<br>
  строки</span></p>
  </td>
  <td width=120 rowspan=3 style='width:90.3pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Студенты, <br>
  находящиеся на <br>
  полном <br>
  государственном обеспечении</span></p>
  </td>
  <td width=96 rowspan=3 style='width:72.2pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span class=GramE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>Граждане,<br>
  подвергшиеся</span></span><span style='font-size:7.0pt;font-family:"Arial",sans-serif;
  color:black'> воздействию<br>
  радиации</span></p>
  </td>
  <td width=145 colspan=2 style='width:108.4pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Инвалиды 1 и 2 групп, инвалиды с
  детства</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:24.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:1;height:1.5pt'>
  <td width=68 rowspan=2 style='width:51.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:1.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=76 rowspan=2 style='width:57.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:1.5pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>в том числе дети-инвалиды</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:1.5pt'></td>
 </tr>
 <tr style='mso-yfti-irow:2;height:31.5pt'>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:31.5pt'></td>
 </tr>
 <tr style='mso-yfti-irow:3;height:12.0pt'>
  <td width=181 style='width:135.45pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Прием</span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=120 valign=bottom style='width:90.3pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>95</span></p>
  </td>
  <td width=96 valign=bottom style='width:72.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>65</span></p>
  </td>
  <td width=76 valign=bottom style='width:57.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>32</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:4;height:12.0pt'>
  <td width=181 style='width:135.45pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Численность студентов</span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>02</span></p>
  </td>
  <td width=120 valign=bottom style='width:90.3pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>339</span></p>
  </td>
  <td width=96 valign=bottom style='width:72.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>6</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>225</span></p>
  </td>
  <td width=76 valign=bottom style='width:57.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>70</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
 <tr style='mso-yfti-irow:5;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=181 style='width:135.45pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Выпуск</span></p>
  </td>
  <td width=40 style='width:30.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>03</span></p>
  </td>
  <td width=120 valign=bottom style='width:90.3pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>77</span></p>
  </td>
  <td width=96 valign=bottom style='width:72.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>31</span></p>
  </td>
  <td width=76 valign=bottom style='width:57.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></p>
  </td>
  <td width=0 style='width:.3pt;padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Результаты приема по уровню образования абитуриентов</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=995
 style='width:746.5pt;border-collapse:collapse;mso-yfti-tbllook:1184;
 mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=309 rowspan=4 style='width:231.8pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=36 rowspan=4 style='width:27.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ <br>
  строки</span></p>
  </td>
  <td width=217 colspan=4 style='width:162.5pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span></span></p>
  </td>
  <td width=217 colspan=4 style='width:162.55pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span></span></p>
  </td>
  <td width=217 colspan=4 style='width:162.55pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы магистратуры</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:12.0pt'>
  <td width=48 rowspan=3 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано <span class=SpellE><span
  class=GramE>заявле</span></span><span class=GramE>-<br>
  <span class=SpellE>ний</span></span></span></p>
  </td>
  <td width=169 colspan=3 style='width:126.4pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
  <td width=48 rowspan=3 style='width:36.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано <span class=SpellE><span
  class=GramE>заявле</span></span><span class=GramE>-<br>
  <span class=SpellE>ний</span></span></span></p>
  </td>
  <td width=169 colspan=3 style='width:126.4pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
  <td width=48 rowspan=3 style='width:36.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано <span class=SpellE><span
  class=GramE>заявле</span></span><span class=GramE>-<br>
  <span class=SpellE>ний</span></span></span></p>
  </td>
  <td width=169 colspan=3 style='width:126.4pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:12.0pt'>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=120 colspan=2 style='width:90.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=120 colspan=2 style='width:90.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=120 colspan=2 style='width:90.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:46.5pt'>
  <td width=64 valign=top style='width:48.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  бюджетных<br>
  <span class=SpellE><span class=GramE>ассигнова</span></span><span
  class=GramE>-<br>
  <span class=SpellE>ний</span></span> <span class=SpellE>феде</span>-<br>
  <span class=SpellE>рального</span> бюджета</span></p>
  </td>
  <td width=56 valign=top style='width:42.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об ока-<br>
  <span class=SpellE>зании</span> плат-<br>
  <span class=SpellE>ных</span> <span class=SpellE>обра</span>-<br>
  <span class=SpellE>зователь</span>-<br>
  <span class=SpellE>ных</span> услуг</span></p>
  </td>
  <td width=64 valign=top style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  бюджетных<br>
  <span class=SpellE><span class=GramE>ассигнова</span></span><span
  class=GramE>-<br>
  <span class=SpellE>ний</span></span> <span class=SpellE>феде</span>-<br>
  <span class=SpellE>рального</span> бюджета</span></p>
  </td>
  <td width=56 valign=top style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об ока-<br>
  <span class=SpellE>зании</span> плат-<br>
  <span class=SpellE>ных</span> <span class=SpellE>обра</span>-<br>
  <span class=SpellE>зователь</span>-<br>
  <span class=SpellE>ных</span> услуг</span></p>
  </td>
  <td width=64 valign=top style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  бюджетных<br>
  <span class=SpellE><span class=GramE>ассигнова</span></span><span
  class=GramE>-<br>
  <span class=SpellE>ний</span></span> <span class=SpellE>феде</span>-<br>
  <span class=SpellE>рального</span> бюджета</span></p>
  </td>
  <td width=56 valign=top style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об ока-<br>
  <span class=SpellE>зании</span> плат-<br>
  <span class=SpellE>ных</span> <span class=SpellE>обра</span>-<br>
  <span class=SpellE>зователь</span>-<br>
  <span class=SpellE>ных</span> услуг</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=309 style='width:231.8pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего (сумма строк 02, 06, 08,
  10, 12, 14)</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>10423</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3203</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2592</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>611</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4066</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1250</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>633</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>617</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1675</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1307</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1285</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>22</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>&nbsp;</span></b></p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>&nbsp;</span></b></p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Результаты приема на обучение по программам <span class=SpellE>бакалавриата</span>
и программам<br>
<span class=SpellE>специалитета</span> по отдельным категориям абитуриентов и
условиям приема</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=429 rowspan=4 style='width:322.1pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=36 rowspan=4 style='width:27.05pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ <br>
  строки</span></p>
  </td>
  <td width=217 colspan=4 style='width:162.55pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span></span></p>
  </td>
  <td width=217 colspan=4 style='width:162.55pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:12.0pt'>
  <td width=48 rowspan=3 style='width:36.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано <span class=SpellE><span
  class=GramE>заявле</span></span><span class=GramE>-<br>
  <span class=SpellE>ний</span></span></span></p>
  </td>
  <td width=169 colspan=3 style='width:126.4pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
  <td width=48 rowspan=3 style='width:36.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано <span class=SpellE><span
  class=GramE>заявле</span></span><span class=GramE>-<br>
  <span class=SpellE>ний</span></span></span></p>
  </td>
  <td width=169 colspan=3 style='width:126.4pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:12.0pt'>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=120 colspan=2 style='width:90.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=120 colspan=2 style='width:90.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:46.5pt'>
  <td width=64 valign=top style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  бюджетных<br>
  <span class=SpellE><span class=GramE>ассигнова</span></span><span
  class=GramE>-<br>
  <span class=SpellE>ний</span></span> <span class=SpellE>феде</span>-<br>
  <span class=SpellE>рального</span> бюджета</span></p>
  </td>
  <td width=56 valign=top style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об ока-<br>
  <span class=SpellE>зании</span> плат-<br>
  <span class=SpellE>ных</span> <span class=SpellE>обра</span>-<br>
  <span class=SpellE>зователь</span>-<br>
  <span class=SpellE>ных</span> услуг</span></p>
  </td>
  <td width=64 valign=top style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  бюджетных<br>
  <span class=SpellE><span class=GramE>ассигнова</span></span><span
  class=GramE>-<br>
  <span class=SpellE>ний</span></span> <span class=SpellE>феде</span>-<br>
  <span class=SpellE>рального</span> бюджета</span></p>
  </td>
  <td width=56 valign=top style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об ока-<br>
  <span class=SpellE>зании</span> плат-<br>
  <span class=SpellE>ных</span> <span class=SpellE>обра</span>-<br>
  <span class=SpellE>зователь</span>-<br>
  <span class=SpellE>ных</span> услуг</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:12.0pt'>
  <td width=429 style='width:322.1pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего (сумма строк 02, 13)</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>10423</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3203</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2592</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>611</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4066</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1250</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>633</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>617</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;height:24.0pt'>
  <td width=429 valign=top style='width:322.1pt;border:solid black 1.0pt;
  border-top:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:4.5pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>в том <span class=GramE>числе:<br>
  принято</span> на обучение для получения первого высшего образования (сумма
  строк 03 – 05; сумма строк 06, 08, 10 – 12)</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>02</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>10420</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3201</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2592</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>609</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>4061</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1245</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>633</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>612</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:6;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=429 style='width:322.1pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:4.5pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>принято на обучение для получения
  второго высшего образования</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>13</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>3</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>2</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>X</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>5</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Результаты приема на обучение по программам магистратуры</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.0pt'>
  <td width=269 rowspan=3 style='width:201.7pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=36 rowspan=3 style='width:27.05pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ <br>
  строки</span></p>
  </td>
  <td width=48 rowspan=3 style='width:36.15pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Подано <span class=SpellE><span
  class=GramE>заявле</span></span><span class=GramE>-<br>
  <span class=SpellE>ний</span></span></span></p>
  </td>
  <td width=169 colspan=3 style='width:126.4pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято на обучение</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:12.0pt'>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=120 colspan=2 style='width:90.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:46.5pt'>
  <td width=64 valign=top style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>за счет<br>
  бюджетных<br>
  <span class=SpellE><span class=GramE>ассигнова</span></span><span
  class=GramE>-<br>
  <span class=SpellE>ний</span></span> <span class=SpellE>феде</span>-<br>
  <span class=SpellE>рального</span> бюджета</span></p>
  </td>
  <td width=56 valign=top style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:46.5pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>по <span class=SpellE><span
  class=GramE>догово</span></span><span class=GramE>-<br>
  рам</span> об ока-<br>
  <span class=SpellE>зании</span> плат-<br>
  <span class=SpellE>ных</span> <span class=SpellE>обра</span>-<br>
  <span class=SpellE>зователь</span>-<br>
  <span class=SpellE>ных</span> услуг</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;mso-yfti-lastrow:yes;height:18.0pt'>
  <td width=269 valign=top style='width:201.7pt;border:solid black 1.0pt;
  border-top:none;padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Принято на обучение по программам
  <span class=GramE>магистратуры:<br>
  первое</span> высшее образование</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1675</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1307</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1285</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:18.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>22</span></p>
  </td>
 </tr>
</table>

</div>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'>&nbsp;</p>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-size:10.0pt;line-height:115%;font-family:"Arial",sans-serif;
color:black'>Направление на работу выпускников, обучавшихся по очной форме
обучения за счет средств бюджетов всех уровней</span></b></p>

<div align=center>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=1045
 style='width:783.95pt;border-collapse:collapse;mso-yfti-tbllook:1184;
 mso-padding-alt:0cm 0cm 0cm 0cm'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:24.0pt'>
  <td width=215 rowspan=3 style='width:160.9pt;border:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>&nbsp;</span></p>
  </td>
  <td width=36 rowspan=3 style='width:27.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>№ <br>
  строки</span></p>
  </td>
  <td width=36 rowspan=3 style='width:27.05pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Код <br>
  <span class=SpellE>клас</span>-<br>
  <span class=SpellE>сифи</span>-<br>
  <span class=SpellE>катора</span>*</span></p>
  </td>
  <td width=52 rowspan=3 style='width:39.15pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:4.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Код<br>
  НП(С)**</span></p>
  </td>
  <td width=189 colspan=3 valign=top style='width:141.45pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Выпуск по очной форме обучения (кроме
  обучавшихся по договорам об оказании платных образовательных услуг)</span></p>
  </td>
  <td width=132 colspan=2 style='width:99.35pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Получили направление<br>
  на работу</span></p>
  </td>
  <td width=153 colspan=3 style='width:114.4pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Не получили направление<br>
  на работу</span></p>
  </td>
  <td width=116 colspan=2 valign=top style='width:87.25pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Предоставлено право свободного
  трудоустройства<br>
  по желанию выпускника</span></p>
  </td>
  <td width=64 rowspan=3 style='width:48.2pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Продолжают обучение на следующем уровне
  по очной форме обучения</span></p>
  </td>
  <td width=52 rowspan=3 style='width:39.1pt;border:solid black 1.0pt;
  border-left:none;padding:0cm .75pt 0cm .75pt;height:24.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Призваны<br>
  в ряды <span class=GramE>Вооружен-<br>
  <span class=SpellE>ных</span></span> Сил</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:12.0pt'>
  <td width=60 rowspan=2 style='width:45.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span class=GramE><span
  style='font-size:7.0pt;font-family:"Arial",sans-serif;color:black'>Всего<br>
  (</span></span><span style='font-size:7.0pt;font-family:"Arial",sans-serif;
  color:black'>сумма<br>
  граф 8, 10,<br>
  13, 15, 16)</span></p>
  </td>
  <td width=128 colspan=2 style='width:96.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них (из гр.5):</span></p>
  </td>
  <td width=48 rowspan=2 style='width:36.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=84 rowspan=2 style='width:63.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>в соответствии<br>
  с заключенными<br>
  договорами о<br>
  целевом приеме<br>
  и целевом обучении</span></p>
  </td>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего</span></p>
  </td>
  <td width=104 colspan=2 style='width:78.3pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них (из гр. 10):</span></p>
  </td>
  <td width=48 rowspan=2 style='width:36.1pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>всего</span></p>
  </td>
  <td width=68 rowspan=2 valign=top style='width:51.15pt;border-top:none;
  border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из них из-за<br>
  несогласия<br>
  выпускника<br>
  с <span class=GramE>предложен-<br>
  <span class=SpellE>ными</span></span> <span class=SpellE>услови</span>-<br>
  <span class=SpellE>ями</span> контракта работодателя</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:42.0pt'>
  <td width=44 style='width:33.1pt;border-top:none;border-left:none;border-bottom:
  solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:42.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>женщин</span></p>
  </td>
  <td width=84 valign=top style='width:63.2pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:42.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.15pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>в соответствии<br>
  с заключенными<br>
  договорами о<br>
  целевом приеме<br>
  и целевом обучении</span></p>
  </td>
  <td width=48 style='width:36.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:42.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:8.35pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>женщин</span></p>
  </td>
  <td width=56 style='width:42.15pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0cm .75pt 0cm .75pt;
  height:42.0pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:6.85pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>из-за отсутствия заявок</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:12.0pt'>
  <td width=215 style='width:160.9pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><b><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>бакалавриата</span>
  - всего</span></b></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>01</span></b></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></b></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></b></p>
  </td>
  <td width=60 valign=bottom style='width:45.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1253</span></b></p>
  </td>
  <td width=44 valign=bottom style='width:33.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>688</span></b></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>45</span></b></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>645</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>368</span></b></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>638</span></b></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>133</span></b></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></b></p>
  </td>
  <td width=64 valign=bottom style='width:48.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>415</span></b></p>
  </td>
  <td width=52 valign=bottom style='width:39.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><b><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>15</span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:12.0pt'>
  <td width=215 style='width:160.9pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:2.25pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы <span class=SpellE>специалитета</span>
  - всего</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:2.25pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:7.6pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>02</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></p>
  </td>
  <td width=60 valign=bottom style='width:45.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>300</span></p>
  </td>
  <td width=44 valign=bottom style='width:33.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>265</span></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>104</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>91</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>196</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;height:12.0pt'>
  <td width=215 style='width:160.9pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:2.25pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Программы магистратуры - всего</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:2.25pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:7.6pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>03</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></p>
  </td>
  <td width=60 valign=bottom style='width:45.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>218</span></p>
  </td>
  <td width=44 valign=bottom style='width:33.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>111</span></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>58</span></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>135</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>87</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>83</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>25</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>24</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:6;mso-yfti-lastrow:yes;height:12.0pt'>
  <td width=215 style='width:160.9pt;border:solid black 1.0pt;border-top:none;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:2.25pt;margin-bottom:.0001pt;text-align:center;
  line-height:7.6pt;text-autospace:none'><span style='font-size:7.0pt;
  font-family:"Arial",sans-serif;color:black'>Всего по программам высшего
  образования (сумма строк 01-03; 05-07)</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:.75pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:2.25pt;margin-bottom:.0001pt;text-align:center;
  mso-line-height-alt:7.6pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>04</span></p>
  </td>
  <td width=36 valign=bottom style='width:27.05pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>0</span></p>
  </td>
  <td width=60 valign=bottom style='width:45.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1771</span></p>
  </td>
  <td width=44 valign=bottom style='width:33.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>1064</span></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>103</span></p>
  </td>
  <td width=84 valign=bottom style='width:63.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>-</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>884</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>546</span></p>
  </td>
  <td width=56 valign=bottom style='width:42.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>721</span></p>
  </td>
  <td width=48 valign=bottom style='width:36.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>158</span></p>
  </td>
  <td width=68 valign=bottom style='width:51.15pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>24</span></p>
  </td>
  <td width=64 valign=bottom style='width:48.2pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>611</span></p>
  </td>
  <td width=52 valign=bottom style='width:39.1pt;border-top:none;border-left:
  none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;
  padding:0cm .75pt 0cm .75pt;height:12.0pt'>
  <p class=MsoNormal align=center style='margin-top:1.5pt;margin-right:0cm;
  margin-bottom:0cm;margin-left:.75pt;margin-bottom:.0001pt;text-align:center;
  line-height:9.3pt;text-autospace:none'><span style='font-size:8.0pt;
  font-family:"Arial",sans-serif;color:black'>15</span></p>
  </td>
 </tr>
</table>
    </div>
   </div>
           </div>

</asp:Content>
