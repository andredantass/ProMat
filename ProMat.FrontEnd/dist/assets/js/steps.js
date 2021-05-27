
var step		= parseInt(localStorage.getItem('step')),
	index		= localStorage.getItem('index'),
	urls		= ['/index.html', '/one_gravida.html', '/one_nasceu.html', 
					'/two_gravida.html', '/two_nasceu.html'];
	intUrl		= 0,
	urlDir		= window.location;

if (step) {
	if(step === 3) {
		if(!(urlDir.pathname === '/three_step.html')) {
			urlDir.pathname = '/three_step.html';
		}
	} else if(step === 4) {
		if(!(urlDir.pathname === '/four_step.html')) {
			urlDir.pathname = '/four_step.html';
		}
	} else if(step === 5) {
		if(!(urlDir.pathname === '/parabens.html')) {
			urlDir.pathname = '/parabens.html';
		}
	} else if(step === 6) {
		if(!(urlDir.pathname === '/indicar.html')) {
			urlDir.pathname = '/indicar.html';
		}
	}
} else {
	urls.forEach(item => {
		if(item === urlDir.pathname) {
			intUrl = 1;
		}
	});

	if (intUrl === 0) {
		urlDir.pathname = '/index.html';
	}
}

// Index Step //

function indexValide(form) {
	if(form === 'gravida') {
		localStorage.setItem('index', 'gravida');
		window.location.pathname = "/one_gravida.html";
	} else if (form === 'nasceu') {
		localStorage.setItem('index', 'nasceu');
		window.location.pathname = "/one_nasceu.html";
	}
}

// Primeiro Step //
function oneValide() {
	var inputSeguro		= document.querySelectorAll("input[name='seguro']"),
		inputCarteira	= document.querySelectorAll("input[name='carteira']"),
		selecao			= document.querySelectorAll("select[name='motivo'] option"),
		erro			= document.querySelector('#erro'),
		index			= localStorage.getItem('index');
		valida			= 0,
		dados			= {};

	inputSeguro.forEach(item => {
		if(item.checked === true) {
			dados.seguro =  item.value;
			valida	= ++valida;
		}
	})

	inputCarteira.forEach(item => {
		if(item.checked === true) {
			dados.carteira = item.value;
			valida = ++valida;
		}
	})

	selecao.forEach(item => {
		if(item.selected === true && item.value !== 'nulo') {
			dados.motivo = item.value;
			valida = ++valida;
		}
	})

	if(valida === 3) {
		localStorage.setItem('one',  JSON.stringify(dados));
		if(index === 'gravida') {
			window.location.pathname = "/two_gravida.html";
		} else {
			window.location.pathname = "/two_nasceu.html";
		}
	} else {
		erro.style.opacity = '1';
		erro.style.animation = '';
		setTimeout(() => erro.style.animation = "vibra .5s", 5);
	}
}

// Segundo Step //

function twoValide() {
	var saida	= document.querySelector("input[name='saida']"),
		parto	= document.querySelector("input[name='parto']"),
		recebeu	= document.querySelectorAll("input[name='recebeu']"),
		oneData	= JSON.parse(localStorage.getItem('one')),
		erro	= document.querySelector('#erro'),
		valida	= 0,
		dados	= {};

	function calcData(dataOne, dataTwo) {
		const count		= 1000 * 60 * 60 * 24;
		var	datO, datT;

		if(platform.name === 'Safari') {
			datO	= dataOne.split('/');
			datT	= dataTwo.split('/');
			datO	= new Date(`${datO[2]}-${datO[1]}-${datO[0]}`);
			datT	= new Date(`${datT[2]}-${datT[1]}-${datT[0]}`);
		} else {
			datO	= new Date(dataOne),
			datT	= new Date(dataTwo);
		}
	
		function calculaData(a, b) {
			const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
			const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());
			return Math.floor((utc2 - utc1) / count);
		}
	
		return (parseInt(calculaData(datO, datT)))
	}

	function dataToBr(data) {
		arrayData	= data.split('-');
		return (`${arrayData[2]}/${arrayData[1]}/${arrayData[0]}`);	
	}

	function dataAtual () {
		let dia, mes, ano;
		dateAt	= new Date().toString();
		dateAt	= dateAt.split(' ');
		dia		= dateAt[2];
		ano		= dateAt[3];

		switch (dateAt[1]) {
			case "Jan": mes = "01"; break; case "Feb": mes = "02"; break;
			case "Mar": mes = "03"; break; case "Apr": mes = "04"; break;
			case "May": mes = "05"; break; case "Jun": mes = "06"; break;
			case "Jul": mes = "07"; break; case "Aug": mes = "08"; break;
			case "Sep": mes = "09"; break; case "Oct": mes = "10"; break;
			case "Nov": mes = "11"; break; case "Dec": mes = "12"; break;
		}
		return (`${ano}-${mes}-${dia}`);
	}

	if(saida.value !== '' && parto.value !== '') {
		valida	= ++valida;

		if (calcData(saida.value, parto.value) < 0) {
			dados.validaData = JSON.stringify({'erro' : 1});
		} else {
			if (oneData.seguro === 'nao' && calcData(saida.value, parto.value) <= 460 ||
				oneData.seguro === 'sim' && calcData(saida.value, parto.value) <= 820) {

				if(localStorage.getItem('index') === 'nasceu') {
					dados.validaData = JSON.stringify({
						'erro' : 0,
						'saida' : `${dataToBr(saida.value)}`,
						'parto' : `${dataToBr(parto.value)}`
					});
				} else {
					if (calcData(dataAtual(), parto.value) > 0) {
						dados.validaData = JSON.stringify({
							'erro' : 0,
							'saida' : `${dataToBr(saida.value)}`,
							'parto' : `${dataToBr(parto.value)}`
						});
					} else {
						dados.validaData = JSON.stringify({'erro' : 1});
					}
				}
			} else {
				dados.validaData = JSON.stringify({'erro' : 1});
			}
		}
	} else if ((oneData.motivo === 'trabalha'|| oneData.motivo === 'nunca-trabalhou') &&
				saida.value === '' && parto.value !== '') {

		valida	= ++valida;
		
		
		if (oneData.motivo === 'trabalha') {

			if (calcData(dataAtual(), parto.value) > 0) {
				dados.validaData = JSON.stringify({
					'erro' : 0,
					'saida' : 'Ainda Trabalhando',
					'parto' : `${dataToBr(parto.value)}`
				});
			} else {
				dados.validaData = JSON.stringify({'erro' : 1});
			}
		} else if (oneData.motivo === 'nunca-trabalhou') {
			dados.validaData = JSON.stringify({'erro' : 1});
		}
	}

	recebeu.forEach(item => {
		if(item.checked === true) {
			valida	= ++valida;
			dados.recebeu = item.value;
		}
	})

	if(valida === 2) {
		localStorage.setItem('two', JSON.stringify(dados));
		localStorage.setItem('step', 3);
		window.location.pathname = "/three_step.html";
	} else {
		erro.style.opacity = '1';
		erro.style.animation = '';
		setTimeout(() => erro.style.animation = "vibra .5s", 5);
	}
}