
var urlData = window.location.pathname,
	tempo	= 0;
var txh2;
var txh1;
function startInitial()
{

	if (urlData === '/index') {
		var h1	= document.querySelector('.copy h1'),
		h2		= document.querySelector('.copy h2'),
		txh1	= 'Você pode ganhar até  R$ 4.500,00.';
		txh2	= 'Responda as perguntas abaixo que te diremos se você tem direito.';

		tempo	= 5500;
	
			var char1 = txh1.split('').reverse(),
				char2 = txh2.split('').reverse();
		
			var interval1 = setInterval(() => {
					if (!char1.length) {
						var interval2 = setInterval(() => {
							if (!char2.length) {
								return clearInterval(interval2);
							}
							var prox2 = char2.pop();
							h2.innerHTML += prox2;
						}, 50);
			
						return clearInterval(interval1);
					}
				var prox1 = char1.pop();
				h1.innerHTML += prox1;
			}, 50);
		
	} else if(urlData === '/parabens.html') {
		var h1	= document.querySelector('.copy h1'),
		h2		= document.querySelector('.copy h2'),
		txh1	= 'Parabéns!';
		txh2	= `Nossa primeira análise mostra que você está 
					dentro dos parâmetros para receber o benefício, 
					porém, ainda é necessário que nossos advogados 
					façam seu procedimento.`;
	
		tempo	= 10000;
		function inicioDig(texto1, rota1, texto2, rota2) {
			var char1 = texto1.split('').reverse(),
				char2 = texto2.split('').reverse();
	
			var interval1 = setInterval(() => {
					if (!char1.length) {
						var interval2 = setInterval(() => {
							if (!char2.length) {
								return clearInterval(interval2);
							}
							var prox2 = char2.pop();
							rota2.innerHTML += prox2;
						}, 50);
						return clearInterval(interval1);
					}
				var prox1 = char1.pop();
				rota1.innerHTML += prox1;
			}, 50);
		}
	} else if(urlData === '/indicar.html') {
		var h1	= document.querySelector('.copy h1'),
		h2		= document.querySelector('.copy h2'),
		txh1	= 'Que pena! Infelizmente você não tem direito ao salário-maternidade!';
		txh2	= `Mas não fique triste, a G7 pensou em uma maneira de te ajudar a 
					ganhar uma renda extra, basta que você indique mulheres 
					que estão grávidas ou que tenham filhos com menos de 5 anos.`;
	
		tempo	= 13000;
		function inicioDig(texto1, rota1, texto2, rota2) {
			var char1 = texto1.split('').reverse(),
				char2 = texto2.split('').reverse();
	
			var interval1 = setInterval(() => {
					if (!char1.length) {
						var interval2 = setInterval(() => {
							if (!char2.length) {
								return clearInterval(interval2);
							}
							var prox2 = char2.pop();
							rota2.innerHTML += prox2;
						}, 50);
						return clearInterval(interval1);
					}
				var prox1 = char1.pop();
				rota1.innerHTML += prox1;
			}, 50);
		}
	}
	setTimeout(() => {
		document.querySelector('.mymodal .box').style.display = 'block';
	}, tempo)
}

function teste()
{
	alert('passou');
}
(function ($) {
    'use strict';
    /*==================================================================
        [ Daterangepicker ]*/
    try {
        $('.js-datepicker').daterangepicker({
            "singleDatePicker": true,
            "showDropdowns": true,
            "autoUpdateInput": false,
            locale: {
                format: 'DD/MM/YYYY'
            },
        });
    
        var myCalendar = $('.js-datepicker');
        var isClick = 0;
    
        $(window).on('click',function(){
            isClick = 0;
        });
    
        $(myCalendar).on('apply.daterangepicker',function(ev, picker){
            isClick = 0;
            $(this).val(picker.startDate.format('DD/MM/YYYY'));
    
        });
    
        $('.js-btn-calendar').on('click',function(e){
            e.stopPropagation();
    
            if(isClick === 1) isClick = 0;
            else if(isClick === 0) isClick = 1;
    
            if (isClick === 1) {
                myCalendar.focus();
            }
        });
    
        $(myCalendar).on('click',function(e){
            e.stopPropagation();
            isClick = 1;
        });
    
        $('.daterangepicker').on('click',function(e){
            e.stopPropagation();
        });
    
    
    } catch(er) {console.log(er);}
    /*[ Select 2 Config ]
        ===========================================================*/
    
    try {
        var selectSimple = $('.js-select-simple');
    
        selectSimple.each(function () {
            var that = $(this);
            var selectBox = that.find('select');
            var selectDropdown = that.find('.select-dropdown');
            selectBox.select2({
                dropdownParent: selectDropdown
            });
        });
    
    } catch (err) {
        console.log(err);
    }
    

})(jQuery);
/* window.onload = () => {
	inicioDig(txh1, h1, txh2, h2);
	setTimeout(() => {
		document.querySelector('.mymodal .box').style.display = 'block';
	}, tempo)
}   */
		