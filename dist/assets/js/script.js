
window.onload = function() {
	var msg1	= `Parabéns pelo bebê. Desejamos muita saúde!
					Para continuar, precisamos que nos dê algumas informações sobre seu último trabalho.
					Depois que responder todas as perguntas, clique no botão.`,
		msg2	= `Já estamos terminando! Agora precisamos de algumas
					informações sobre seu bebê. Preencha todos os campos
					e clique no botão “Próximo”.`,
		msg3	= `Você está bem perto de saber se vai ganhar os R$ 4.100,00. 
					Só falta mais uma etapa para saber o resultado!`,
		msg4	= `Chegamos na última etapa! Logo que terminar, aperte o botão 
					“SABER SE TENHO DIREITO” e saiba se tem direito ao salário-maternidade.`,
		rota	= document.querySelector(".robot h3"),
		steps	= document.querySelector(".steps-one"),
		forms	= document.querySelector(".form-one"),
		url		= window.location.pathname.split('_')[0].substring(1),
		step1	= document.querySelector('.step1 div'),
		step2	= document.querySelector('.step2 div'),
		step3	= document.querySelector('.step3 div'),
		step4 	= document.querySelector('.step4 div'),
		line1	= document.querySelector('.line1'),
		line2	= document.querySelector('.line2'),
		line3	= document.querySelector('.line3');

	function digitando(texto, rota, steps, form) {
		var char = texto.split('').reverse(),
			intervalo = setInterval(() => {
	        	if (!char.length) {
					if(steps) {
						steps.style.transform = 'translateX(0)';
						form.style.transform = 'translateX(0)';
					}
					return clearInterval(intervalo);
				}
	        var prox = char.pop();
	        rota.innerHTML += prox;
		}, 50);
	}

	function setaStyle(rota, backCor, textCor, bordColor) {
		if(bordColor) {
			rota.style.border = `solid 1px ${bordColor}`;
		} else {
			rota.style.backgroundColor = backCor;
			rota.style.color = textCor;
		}
	}

	if(url === "one") {
		if(!localStorage.getItem('one')) {
			digitando(msg1, rota, steps, forms);
		} else {
			document.querySelector('.robot').style.display = 'none';
			steps.style.transform = 'translateX(0)';
			forms.style.transform = 'translateX(0)';
		}
		setaStyle(step1, "#27AE60", "white");
	} else if (url === "two") {
		digitando(msg2, rota, steps, forms);
		setaStyle(step1, "#27AE60", "white");
		setaStyle(step2, "#27AE60", "white");
		setaStyle(line1, null, null, "#27AE60");
	} else if (url === "three") {
		digitando(msg3 , rota, steps, forms);
		setaStyle(step1, "#27AE60", "white");
		setaStyle(step2, "#27AE60", "white");
		setaStyle(step3, "#27AE60", "white");
		setaStyle(line1, null, null, "#27AE60");
		setaStyle(line2, null, null, "#27AE60");
	} else if (url === "four") {
		digitando(msg4, rota, steps, forms);
		setaStyle(step1, "#27AE60", "white");
		setaStyle(step2, "#27AE60", "white");
		setaStyle(step3, "#27AE60", "white");
		setaStyle(step4, "#27AE60", "white");
		setaStyle(line1, null, null, "#27AE60");
		setaStyle(line2, null, null, "#27AE60");
		setaStyle(line3, null, null, "#27AE60");
	}
}
