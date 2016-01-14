<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Spo1och.aspx.cs" Inherits="KPIWeb.Rector.Stats.Spo1och" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div>
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" OnClick="Button222_Click" />
            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
     
           </div>
    
    <html> 
<style>

img {display:none;}


#oglav div{
margin-left:50px;
margin-top: 12px;
}

#oglav div div{
margin-left:50px;
margin-top: 12px;
}

#oglav div div div {
margin-left:50px;
margin-top: 12px;
}
#oglav div div div div{
margin-left:50px;
margin-top: 12px;
}
#oglav div div div div div{
margin-left:50px;
margin-top: 12px;
}
</style>
<body>
<div id="oglav">
<!--11111111 -->
<div><a href="#">Раздел 1. Общие сведения об образовательной организации</a>
	<!--22222222 -->
	<div><a href="1.1.png">1.1. Сведения о наличии лицензии на осуществление образовательной деятельности, свидетельства о государственной аккредитации и коллегиальных органах управления</a></div>
	<!--22222222 -->
	<div><a href="1.2.png">1.2. Сведения об обособленных структурных подразделениях (филиалах) и представительствах</a></div>
	<!--22222222 -->
	<div><a href="1.3.png">1.3. Сведения об образовательных программах, реализуемых организацией</a>
		<!--33333333 -->
		<div><a href="#"></a></div>  
	</div>
	
	
</div>
<!--11111111 -->
<div><a href="">Раздел 2. Сведения о приеме, численности студентов и выпускников</a>
	<!--22222222 -->
	<div><a href="#"></a>
		<!--33333333 -->
		<div><a href="spooch\2.1.1.png"> 2.1.1. Распределение приема по специальностя</a>
			<!--444444444 -->
			<div><a href="spooch\2.1.1.png">-Очное (Подробно)</a></div>
			<div><a href="spooch\2.1.1_itog.png">-Очное (Итог)</a></div>
			<div><a href="spozaoch\2.1.1.png">-Заочное (Подробно)</a></div>
			<div><a href="spozaoch\2.1.1itog.png">-Заочное (Итог)</a></div>
		</div>  
		<!--33333333 -->
		<div><a href="#"> 2.1.2. Распределение численности студентов и выпуска по специальностям</a>
			<!--444444444 -->
			<div><a href="spooch\2.1.2.png">-Очное (Подробно)</a></div>
			<div><a href="spooch\2.1.2_itog.png">-Очное (Итог)</a></div>
			<div><a href="spozaoch\2.1.2.png">-Заочное (Подробно)</a></div>
			<div><a href="spozaoch\2.1.2itog.png">-Заочное (Итог)</a></div>
		</div>  		
	</div>
	<!--22222222 -->
	<div><a href="#">2.2. Движение численности студентов </a>
		<!--33333333 -->
		<div><a href="spooch\2.2.png">-Очное</a></div>
		<div><a href="spozaoch\2.2.png">-Заочное</a></div>
		
		<!--33333333 -->
		<div><a href="#">2.2.1. Сведения о выбытии студентов по специальностям по неуспеваемости и другим причинам </a>
			<!--444444444 -->
			<div><a href="pooch\2.2.1_1cur.png">-Очное 1 курс</a></div>
			<div><a href="vpooch\2.2.1_2cur.png">-Очное 2 курс</a></div>
			<div><a href="vpooch\2.2.1_3cur.png">-Очное 3 курс</a></div>
			<div><a href="vpooch\2.2.1_4cur.png">-Очное 4 курс</a></div>
			<div>
				<!--5555555555 -->
				<div>
				<a href="spooch\2.2.1_itog.png">-Очное ИТОГ</a>
				</div>
			</div>
			<!--444444444 -->
			<div><a href="spozaoch\2.2.11kurs.png">-Заочное 1 курс</a></div>
			<div><a href="spozaoch\2.2.12kurs.png">-Заочное 2 курс</a></div>
			<div><a href="spozaoch\2.2.13kurs.png">-Заочное 3 курс</a></div>
			<div><a href="spozaoch\2.2.14kurs.png">-Заочное 4 курс</a></div>
			<div>
				<!--5555555555 -->
				<div>
				<a href="spozaoch\2.2.1itog.png">-Заочное ИТОГ</a>
				</div>
			</div>
		</div>
		
		 
  
   
   
   
       
   
   
   
   
      
		
		
	</div>	  
	<!--22222222 -->
	<div><a href="#">2.3. Распределение численности студентов, приема и выпуска по источникам финансирования обучения. 
   Сведения о студентах с ограниченными возможностями здоровья и инвалидах</a>
		<!--33333333 -->
		<div><a href="spooch\2.3.png">-Очное</a></div>
		<div><a href="spozaoch\2.3.png">-Заочное</a></div>
	</div>
	<!--22222222 -->
	<div><a href="#">2.4. Численность студентов очной формы обучения, получающих стипендии и другие формы материальной поддержки</a>
		<!--33333333 -->
		<div><a href="spooch\2.4.png">-Очное</a></div>
	</div>
	<!--22222222 -->
	<div><a href="#">2.5. Численность студентов, прием и выпуск по категориям льготного обеспечения очной формы обучения</a>
		<!--33333333 -->
		<div><a href="spooch\2.5.png">-Очное</a></div>
	</div>
	<!--22222222 -->
	<div><a href="#">2.6. Результаты приема по уровню образования абитуриентов </a>
		<!--33333333 -->
		<div><a href="spooch\2.6.png">-Очное</a></div>
		<div><a href="spozaoch\2.6.png">-Заочное</a></div>
	</div>  
	<!--22222222 -->
	<div><a href="#">   2.7. Распределение численности студентов, приема и выпуска по гражданству</a>
		<!--33333333 -->
		<div><a href="spooch\2.7.png">-Очное</a></div>
		<div><a href="spozaoch\2.7.png">-Заочное</a></div>
	</div> 
	<!--22222222 -->
	<div><a href="#">   2.8. Распределение численности студентов, приема и выпуска по возрасту и полу</a>
		<!--33333333 -->
		<div><a href="spooch\2.8.png">-Очное</a></div>
		<div><a href="spozaoch\2.8.png">-Заочное</a></div>
	</div> 
 

</div>
<!--11111111 -->
<div><a href="#">Раздел 3. Сведения о персонале организации </a>
	<!--22222222 -->
	<div><a href="#"></a>
		<!--33333333 -->
		<div><a href="3.1.1.png">3.1.1. Распределение численности основного персонала по уровню образования</a></div>
		<div><a href="3.1.2.png">3.1.2. Распределение численности внешних совместителей по уровню образования</a></div>
		<div><a href="3.1.3.png">3.1.3. Сведения об ученых степенях профессорско-преподавательского состава и научных работников</a></div>
	</div> 
	<!--22222222 -->
	<div><a href="3.2.png"> 3.2. Распределение персонала по стажу работы</a></div> 
	<div><a href="3.3.png"> 3.3. Распределение персонала по полу и возрасту</a></div> 
</div>

 



 
</div>


</body>

</html>


</asp:Content>
