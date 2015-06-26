﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RMain.aspx.cs" Inherits="KPIWeb.Rector.RMain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  

<style>
    body {
        top: 50px;
    }
    input[id$="Button1"] {
        width: 360px;
        height: 250px;
        background-image: none;
        position: absolute;
        top: 0;
        left: 0;
        opacity: 0.3;
        z-index: 3;   
        background-color: aliceblue;
    }
    
   input[id$="Button1"]:hover {

       background-image: none;
       opacity: 0;

     }
     
       input[id$="Button2"] {   
        background-image: none;
        top: 0;
        left: 0;
           opacity: 0;
        width: 360px;
        height: 250px;
     
        position: absolute;
        z-index: 3;
    }
       
        input[id$="Button2"]:hover{

       background-image: none;
       opacity: 0;

     }
        input[id$="Button3"] {
        width: 700px;
        height: 100px;
        background-image: none;
        position: absolute;
        top: 0;
        left: 0;
        opacity: 0.3;
        z-index: 3;   
        background-color: aliceblue;
    }
    
   input[id$="Button3"]:hover {

       background-image: none;
       opacity: 0;

     }

       #div_1 {
         top: 0;
            left: 9.75%;
           position: absolute;
              width: 360px;
        height: 250px;
       
        background-image: url('http://sosnovskij.ru/wp-content/uploads/2012/05/seo-analysis-sajta.jpg');
        background-repeat: no-repeat;
        background-size: 400px 300px;
        background-position: -40px 0px;
         
           }
    
        #div_2 {
            top: 0;
            left: 62.5%;
           position: absolute;
           
           width: 360px;
        height: 250px;
            background-image: url('http://www.firestock.ru/wp-content/uploads/2014/04/Fotolia_53649479_Subscription_XL-700x524.jpg');
        background-repeat: no-repeat;
        background-size: 400px 300px;
        background-position: -40px 0px;
            }

        #div_3 {
         top: 40%;
            left: 21%;
           position: absolute;
              width: 700px;
        height: 100px;
       
        background-image: url('http://www.wallsbox.ru/images/original/temnyj-les-%5B3266062%5D.jpg');
        background-repeat:repeat-x;
      
        background-size: 700px 100px;
        background-position: -40px 0px;
         
           }
        #span_1 {
              top: 0;
            position: relative;
            width: 100%;
            height: 800px;
            text-align: center;
              }
        #insp1,#insp2,#insp3 {
            z-index: 2;
            position: absolute;
            width: 360px;
            height: 100px;
            text-align: center;
            padding-bottom: 2px;
            background-color: #ffffff;
            border-radius: 0px;
            left: 0px;
            opacity: 0.7;

            font-weight: bold;
            font-size: 24px;
        
            left: 0px;
            color: #000000;
        }
        #insp2 { height: 140px;
                 width: 360px;
        }
         #insp3 { height: 34px;
                 width: 700px;
        }
        #ow {
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 5;
           
        }
       
        .styleantext {
            animation-name: anim_pad_text_ofbuttons;
            animation-duration: 0.3s;
            animation-direction: initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;
            }
        .styleantext2 {
            animation-name: anim_pad_text_ofbuttons2;
            animation-duration: 0.3s;
            animation-direction: initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;
            }
         .styleantext3 {
            animation-name: anim_pad_text_ofbuttons3;
            animation-duration: 0.3s;
            animation-direction: initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;
            }
         .styleantext_r {
            animation-name: anim_pad_text_ofbuttons_r;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;
            }
        .styleantext_r2 {
            animation-name: anim_pad_text_ofbuttons_r2;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;
            }
          .styleantext_r3 {
            animation-name: anim_pad_text_ofbuttons_r3;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;
            }


       
        .dark {
            
                      animation-name: darkbe;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;

        }
        .light {
            
                      animation-name: lightbe;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;

        }
        .clickgo {
             animation-name: clck;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;

        }
        .clickgo2 {
             animation-name: clck2;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;

        }
         .clickgo3 {
             animation-name: clck3;
            animation-duration: 0.3s;
            animation-direction:initial;
            animation-iteration-count:1; 
            animation-fill-mode:both;

        }
        .zindex4 {
            z-index: 4;
        }
        .zindex5 {
            z-index: 5;
        }
       .zindex6 {
            z-index: 6;
          
        }

    @keyframes clck {
      0%{box-shadow: 0 0px 10px 2px #0000ff; 
           opacity: 0;}
       100%{box-shadow: 0 0px 50px 11px #00ff00;
           opacity:0.7;color: #ffffff;}      
    
    }
    @keyframes clck2 {
      0%{box-shadow: 0 0px 10px 2px #00ff00; 
           opacity: 0;}
       100%{box-shadow: 0 0px 60px 11px #0000f0;
           opacity:0.7;color: #ffffff;}      
    
    }
       @keyframes clck3 {
      0%{box-shadow: 0 0px 10px 2px #00ff00; 
           opacity: 0;}
       100%{box-shadow: 0 0px 60px 11px #00ffff;
           opacity:0.7;color: #ffffff;}      
    
    }
        @keyframes darkbe{
                          0%{ background-color: #ffffff;opacity: 0;}
                          100%{ background-color: #000000;opacity: 0.8;}
                          }
          @keyframes lightbe{
                        100% { background-color: #ffffff;opacity: 0;}
                          0%{ background-color: #000000;opacity: 0.8;}
                          }
   

        
    @keyframes anim_pad_text_ofbuttons {
        0% {
            margin-top: 0px;
            opacity: 0.8;
         background-color: #c0c0c0;
        }

        100% {
           margin-top: 70px;
             opacity: 1;
            background-color: aliceblue;
             box-shadow: 0 0px 10px 4px #0000ff;
        }
    }
      @keyframes anim_pad_text_ofbuttons2 {
        0% {
            margin-top: 0px;
            opacity: 0.8;
         background-color: #c0c0c0;
        }

        100% {
           margin-top: 55px;
             opacity: 1;
            background-color: aliceblue;
             box-shadow: 0 0px 10px 4px #00ff00;
        }
    }
            @keyframes anim_pad_text_ofbuttons3 {
        0% {
            margin-top: 0px;
            opacity: 0.8;
         background-color: #c0c0c0;
        }

        100% {
           margin-top: 30px;
             opacity: 1;
            background-color: aliceblue;
             box-shadow: 0 0px 10px 4px #00ffff;
        }
    }
  @keyframes anim_pad_text_ofbuttons_r{

     100%  {
            margin-top: 0px;
            opacity: 0.8;
       
        }

        0% {
           margin-top: 85px;
           opacity: 1;
       
           }
    }
   @keyframes anim_pad_text_ofbuttons_r2{

     100%  {
            margin-top: 0px;
            opacity: 0.8;
       
        }

        0% {
           margin-top: 55px;
           opacity: 1;
       
           }
    }
      @keyframes anim_pad_text_ofbuttons_r3{

     100%  {
            margin-top: 0px;
            opacity: 0.8;
       
        }

        0% {
           margin-top: 30px;
           opacity: 1;
       
           }
    }
</style>
    <div id="ow"></div> 
    <div id="span_1">
    
        <br />
    <span id="div_1">
        <asp:Button ID="Button1" runat="server" Text="" OnClick="Button1_Click" CssClass="" />
        <span id="insp1" >Анализ достижения плановых значений целевых показателей</span>
        </span>
       
        <span id="div_2">
        <asp:Button ID="Button2" runat="server" Text=""  OnClick="Button2_Click"  CssClass=""/>
            <span id="insp2" >Вклады структурных подразделений в достигнутые целевые показатели</span>
    </span>

        <span id="div_3">
        <asp:Button ID="Button3" runat="server" Text=""  OnClick="Button2_Click"  CssClass=""/>
            <span id="insp3" >Третья кнопка снизу по центру</span>
    </span>
    </div>
 <script>
     $("#div_2,#div_1,#div_3").addClass("zindex6");

     //*******************************ПЕРВАЯ КНОПКА(ЛЕВАЯ(АНАЛИЗ ПОКАЗАТЕЛЕЙ))***
     $("#div_1").hover(

         function () {
             $("#div_2").addClass("zindex4");
             $("#div_3").addClass("zindex4");
             $("#div_2").removeClass("zindex6");
             $("#div_3").removeClass("zindex6");
       

             $("#ow").addClass("dark");
             $("#ow").removeClass("light");
             $("#insp1").addClass("styleantext");
             $("#insp1").removeClass("styleantext_r");
   

         }, function () {
             $("#div_2").addClass("zindex6");
             $("#div_3").addClass("zindex6");
             $("#div_2").removeClass("zindex4");
             $("#div_3").removeClass("zindex4");
             $("#ow").removeClass("dark");
             $("#ow").addClass("light");
        
             $("#insp1").removeClass("styleantext");
             $("#insp1").addClass("styleantext_r");
       
         });

     $("input[id$='Button1']").mousedown(function() {
             $("input[id$='Button1']").addClass("clickgo");
         }
     );
     $("input[id$='Button1']").mouseleave(function () {
             $("input[id$='Button1']").removeClass("clickgo");
         }
     );
     //*******************************ВТОРАЯ КНОПКА(ПРАВАЯ(РЕЙТИНГ СТРУКТУРНЫХ ПОДРАЗДЕЛЕНИЙ))***
     $("#div_2").hover(

         function () {
             $("#div_1").addClass("zindex4");
             $("#div_1").removeClass("zindex6");
             $("#div_3").addClass("zindex4");
             $("#div_3").removeClass("zindex6");


             $("#ow").addClass("dark");
             $("#ow").removeClass("light");
             $("#insp2").addClass("styleantext2");
             $("#insp2").removeClass("styleantext_r2");


         }, function () {
             $("#div_1").addClass("zindex6");
             $("#div_1").removeClass("zindex4");
             $("#div_3").addClass("zindex6");
             $("#div_3").removeClass("zindex4");
             $("#ow").removeClass("dark");
             $("#ow").addClass("light");

             $("#insp2").removeClass("styleantext2");
             $("#insp2").addClass("styleantext_r2");

         });
     $("input[id$='Button2']").mousedown(function () {
         $("input[id$='Button2']").addClass("clickgo2");
     }
    );
     $("input[id$='Button2']").mouseleave(function () {
         $("input[id$='Button2']").removeClass("clickgo2");
     }
     );
     //*******************************ТРЕТЬЯ КНОПКА(НИЖНЯЯ ЦЕНТРАЛЬНАЯ (БЕЗЫМЯННАЯ))***
     $("#div_3").hover(

         function () {
             $("#div_2").addClass("zindex4");
             $("#div_1").addClass("zindex4");
             $("#div_2").removeClass("zindex6");
             $("#div_1").removeClass("zindex6");


             $("#ow").addClass("dark");
             $("#ow").removeClass("light");
             $("#insp3").addClass("styleantext3");
             $("#insp3").removeClass("styleantext_r3");


         }, function () {
             $("#div_2").addClass("zindex6");
             $("#div_2").removeClass("zindex4");
             $("#div_1").addClass("zindex6");
             $("#div_1").removeClass("zindex4");

             $("#ow").removeClass("dark");
             $("#ow").addClass("light");

             $("#insp3").removeClass("styleantext3");
             $("#insp3").addClass("styleantext_r3");

         });

     $("input[id$='Button3']").mousedown(function () {
         $("input[id$='Button3']").addClass("clickgo3");
     }
     );
     $("input[id$='Button3']").mouseleave(function () {
         $("input[id$='Button3']").removeClass("clickgo3");
     }
     );
 </script>
</asp:Content>
