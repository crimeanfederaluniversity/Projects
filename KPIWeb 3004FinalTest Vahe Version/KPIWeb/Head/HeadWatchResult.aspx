<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadWatchResult.aspx.cs" Inherits="KPIWeb.Head.HeadWatchResult" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
 <style>   
    .gridView2
{
  table-layout:fixed;
}
     </style>
    <br />
    <H2> Сводная таблица значений целевых показателей. </H2>
    <br />
    <br />
    <asp:GridView ID="GridviewCollected"  BorderStyle="Solid" runat="server"  CssClass="Grid_view_V_style Grid_view_style GridHeader gridView2" ShowFooter="true" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0">
            
                <Columns>                  
                     <asp:BoundField DataField="ParamID" HeaderText="Код показателя" Visible="false" />                              
                     <asp:BoundField DataField="Name"  HeaderText="Название показателя" Visible="true" />
                     <asp:BoundField DataField="Response"  HeaderText="Ответственный" Visible="true" /> 
                     <asp:BoundField DataField="Measure"  HeaderText="Ед. Изм." Visible="true" />                    
                     <asp:BoundField DataField="Planned"  HeaderText="Плановое" Visible="true" />

                                
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value0"  Width="90%" style="text-align:center;" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"  HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value1" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value2" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value3" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value4" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value5" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value6" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value7" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value8" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value9" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="Value10" Width="90%" style="text-align:center"  ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value11" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value11") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  

                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value12" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value12") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value13" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value13") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value14" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value14") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value15" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value15") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value16" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value16") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value17" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value17") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value18" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value18") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value19" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value19") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value20" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value20") %>'></asp:Label>                           
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value21" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value21") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value22" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value22") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value23" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value23") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value24" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value24") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value25" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value25") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField>                            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value26" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value26") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value27" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value27") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value28" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value28") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value29" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value29") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value30" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value30") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value31" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value31") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value32" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value32") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value33" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value33") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value34" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value34") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value35" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value35") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value36" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value36") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value37" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value37") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value38" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value38") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value39" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value39") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="Value40" Width="90%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value40") %>'></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>


    <br />
        <script type="text/javascript" >
            var counter = 0;
            var masoftables = [];
            function parseGW() {
                // document.body.style.backgroundColor = gw_name;
                windowheighter = window.screen.availWidth;
                $(window).load(function () {

                    var texts = "def";
                    var i = 0;
                    var ownerdiv = [];
                    var cthead = "ControlHead";

                    var gridHeader;
                    var s;
                    for (i = 0; i < document.all.length; i++) {
                        if (((document.all[i].tagName.toString()).toLowerCase()) == 'table') {
                            masoftables[counter] = document.all[i].getAttribute("id").toString();
                            counter++;
                        }
                    }
                    $('#cdf').text(masoftables[1]);
                    for (i = 0; i < counter; i++) {

                        ownerdiv[i] = "diva"; cthead = "ControlHead";
                        ownerdiv[i] += '_' + i.toString();
                        cthead += '_' + i.toString();
                        $('#' + masoftables[i]).wrap(("<div id='" + ownerdiv[i] + "' style='position:absolute;'></div>"));
                        $(('#' + ownerdiv[i])).prepend(("<div id='" + cthead + "'></div>"));

                        $('#' + ownerdiv[i]).wrap(("<div id='" + "pre_" + ownerdiv[i] + "' style='position:absolute;'></div>"));
                        $("#pre_" + ownerdiv[i]).css('position', 'relative');
                        $("#pre_" + ownerdiv[i]).css('height', (document.getElementById("diva_" + (i).toString()).getBoundingClientRect().bottom - document.getElementById("diva_" + (i).toString()).getBoundingClientRect().top).toString() + "px");

                        // if (i == 0) { $('#' + ownerdiv).before("<div id ='SHEET' style='position:relative;'>"); }
                        // if ((i + 1) == counter) { $('#' + ownerdiv).after("</div>"); }
                        //  if (i > 0) { $(("#" + ownerdiv[i])).css('top', (document.getElementById(ownerdiv[i - 1]).getBoundingClientRect().bottom + window.pageYOffset + (window.pageYOffset - document.getElementById(ownerdiv[i - 1]).getBoundingClientRect().top)).toString() + "px"); }


                        gridHeader = $('#' + masoftables[i]).clone(true);
                        // document.getElementById(masoftables[i]).id = "FF_" + i.toString();
                        $(gridHeader).find("tr:gt(0)").remove();
                        // $(gridHeader).id = "FFF";

                        $('#' + masoftables[i] + ' tr th').each(function (s) {

                            $("th:nth-child(" + (s + 1) + ")", gridHeader).css('width', ($(this).width() + 1).toString() + "px");
                        });


                        $(("#" + cthead)).append(gridHeader);
                        document.getElementById(masoftables[i]).id = "ffg" + i.toString();
                        $(("#" + cthead)).css('position', 'absolute');

                        $(("#" + cthead)).css('z-index', '222');
                        //  $(("#" + cthead + " tr")).css('left', ($('#' + 'ffg' + i.toString()).offset().left + 1).toString() + "px");



                        // $('#cdf').text(i.toString());
                    }
                    //$('#cdf').text((((document.getElementById("diva_" + (0).toString()).getBoundingClientRect().bottom - document.getElementById("diva_" + (0).toString()).getBoundingClientRect().top).toString() + "px")));
                    //$('#cdf').text(gridHeader.id.toString());
                    $("#aroundgwsdivALL").css('position', 'relative');


                });

            }

            parseGW();
            mtb();
            function rth() {
                var cthead = "ControlHead";


                for (i = 0; i < counter; i++) {
                    cthead += '_' + i.toString();
                    $('#' + masoftables[i] + ' tr th').each(function (f) {
                        cthead = "ControlHead";
                        cthead += '_' + i.toString();
                        //$("th:nth-child(" + (s + 1) + ")", $("#" + cthead)).css('width', ($(this).width() + 3).toString() + "px");
                        // $("th:nth-child(" + (s + 1) + ")", $("#" + cthead)).css('color', "#ff0000");
                        if (($('#' + masoftables[i] + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width()) != ($('#' + cthead + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width()))
                        { $("#" + cthead + " tr th:nth-child(" + ((f + 1).toString()) + ")").css('width', (($('#' + masoftables[i] + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width() + 3).toString()) + "px"); }
                        //$('#cdf').text("ws= "+window.screen.availWidth.toString() + "\nwh= " + windowheighter.toString());

                    });
                    // if (i == 0) break;
                }
                //document.body.style.backgroundColor = "#80ff00";
                //  windowheighter = window.screen.availWidth;
                $('#cdf').text(($('#' + masoftables[0] + ' tr th:nth-child(' + ((2).toString()) + ')').width().toString()));



            }

            var windowheighter;
            function mtb() {
                window.setTimeout("mtb()", 50);

                // if (window.screen.availWidth != windowheighter) rth();
                rth();

                //$('#cdf').text("WW " + window.screen.availWidth + "\n\n"+"SW " + screen.width);
                //$('#cdf').text(((document.getElementById("diva_0").getBoundingClientRect().right - document.getElementById("diva_0").getBoundingClientRect().left).toString()));
                for (i = 0; i < counter; i++) {
                    ownerdiv = "diva"; cthead = "ControlHead";
                    ownerdiv += '_' + i.toString();
                    cthead += '_' + i.toString();

                    if ((document.getElementById(ownerdiv).getBoundingClientRect().top) < 50) {


                        if ((document.getElementById(ownerdiv).getBoundingClientRect().bottom) < 100) {
                            $('#' + cthead).css('position', 'absolute');
                        }
                        else {
                            $('#' + cthead).css('position', 'fixed');
                            $('#' + cthead).css('width', (((document.getElementById(ownerdiv).getBoundingClientRect().right - document.getElementById(ownerdiv).getBoundingClientRect().left).toString()) + "px"));
                            $('#' + cthead).css('top', '48px');
                            $('#' + cthead).css('left', ((document.getElementById(ownerdiv).getBoundingClientRect().left + 1).toString()) + "px");
                        }
                    }
                    else {
                        $('#' + cthead).css('position', 'absolute');
                        $('#' + cthead).css('width', (((document.getElementById(ownerdiv).getBoundingClientRect().right - document.getElementById(ownerdiv).getBoundingClientRect().left).toString()) + "px"));
                        $('#' + cthead).css('left', ((0).toString()) + "px");
                        $('#' + cthead).css('top', '0');
                    }


                }
            }

       </script>

</asp:Content>