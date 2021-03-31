import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-thankyou-page',
  templateUrl: './thankyou-page.component.html',
  styleUrls: ['./thankyou-page.component.css']
})
export class ThankyouPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    Swal.fire('Formulário Enviado!', 'PARABÉNS PELA SUA DECISÃO. <br> AGRADECEMOS A CONFIANÇA EM NOSSOS SERVIÇOS E MUITO EM BREVE RETORNAREMOS COM O RESULTADO DA ANÁLISE DIRETAMENTE EM SEU WHATSAPP!', 'success');
  }

}
