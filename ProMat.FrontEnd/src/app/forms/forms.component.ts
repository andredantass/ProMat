import { Component, ElementRef, OnInit, AfterViewChecked, AfterViewInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormService } from '../services/form.service';
import { QualifiedQueue } from '../qualification-form/model/qualifiedqueue';
import Swal from 'sweetalert2';

declare var startInitial: Function;
@Component({
  selector: 'app-forms',
  templateUrl: './forms.component.html',
  styleUrls: ['./forms.component.scss']
})
export class FormsComponent implements OnInit, AfterViewChecked, AfterViewInit {
  qualifiedForm: QualifiedQueue = new QualifiedQueue();
  ipAddress: string;
  location: string;
  username: string;
  text: string;
  jobText: string;
  autoSaveInterval;
  @ViewChild('devQuestionChildWasBorn', { static: true }) dvQChildWasBorn: ElementRef;
  @ViewChild('dvQuestionChildLess5Years', { static: true }) dvQuestionChildLess5Years: ElementRef;
  @ViewChild('dvQuestionWorkBeforeBorn', { static: true }) dvQuestionWorkBeforeBorn: ElementRef;
  @ViewChild('dvQuestionInsurances', { static: true }) dvQuestionInsurances: ElementRef;
  @ViewChild('dvQuestionContributed', { static: true }) dvQuestionContributed: ElementRef;
  @ViewChild('dvQuestionWorkOnBirth', { static: true }) dvQuestionWorkOnBirth: ElementRef;
  @ViewChild('dvCongratulation', { static: true }) dvCongratulation: ElementRef;
  @ViewChild('checkMarkAnswer', { static: true }) checkMarkAnswer: ElementRef;


  constructor(private router: Router, private formService: FormService) { }

  RedirectQualification(status) {
    this.router.navigate(['/qualification']);
  }


  /* setTimeout(() => {
  document.getElementById('devQuestionChildWasBorn').style.display = 'block';
}, tempo) */

  ngOnInit() {
    startInitial();
    this.getIP();
    this.qualifiedForm.DateBorn = "";
    this.qualifiedForm.DateJobEnd = "";
    this.qualifiedForm.DateWillBorn = "";
    this.qualifiedForm.Email = "";
    this.qualifiedForm.Name = "";
    this.qualifiedForm.PaidTen = "";
    this.qualifiedForm.Phone = "";
    this.qualifiedForm.PrevSituation = "";
    this.qualifiedForm.SegJobReceive = "";
    this.qualifiedForm.Situation = "";
    this.qualifiedForm.WorkOnBirth = "";
  }
  ngAfterViewInit() {

    // contentChild is set after the content has been initialized

  }
  responseIp(response) {
    this.ipAddress = response.ip;
  }
  /* responseLocation(response) {
    this.location = response.location.city + "/" + response.location.region;
  } */
  getIP() {
    this.formService.getIPAddress().subscribe((response) => {
      this.responseIp(response);
    });
  }
  /* getLocation() {
    this.formService.getLocation(this.ipAddress).subscribe((response)=>{
      this.responseLocation(response);
    })
  }   */
  ngAfterViewChecked() {

  }
  answerChildWasBorn() {
    this.dvQChildWasBorn.nativeElement.style.display = 'none';

    this.checkMarkAnswer.nativeElement.style.display = 'block';
    this.autoSaveInterval = setInterval(() => {
      this.checkMarkAnswer.nativeElement.style.display = 'none';
      this.dvQuestionChildLess5Years.nativeElement.style.display = "block";
    }, 2000);
    this.countAccessYes();
  }

  answerChildLess5Years(option) {
    this.ngOnDestroy();
    this.dvQuestionChildLess5Years.nativeElement.style.display = "none";

    if (option == 1) {
      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvQuestionWorkBeforeBorn.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.Situation = "Mãe de filho menor de 5 Anos"
    }
    else {
      this.qualifiedForm.Situation = "Mãe de filho maior de 5 Anos"
      this.router.navigate(['/obrigado']);
    }
  }
  answerWorkBeforeBorn(option) {
    this.ngOnDestroy();

    this.dvQuestionWorkBeforeBorn.nativeElement.style.display = "none";

    if (option == 1) {
      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvQuestionWorkOnBirth.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.PrevSituation = "Trabalhei Registrada";
    }
    else {
      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvQuestionContributed.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.PrevSituation = "Não Trabalhei Registrada";
    }
  }
  answerContributed(option) {
    this.ngOnDestroy();

    this.dvQuestionContributed.nativeElement.style.display = "none";

    if (option == 1) {
      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvCongratulation.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.PaidTen = "Contribuí Individualmente";
    }
    else {
      this.qualifiedForm.PaidTen = "Não Contribuí";
      this.router.navigate(['/obrigado']);
    }
  }
  answerWorkOnBirth(option) {
    this.ngOnDestroy();
    if (option == 1) {
      this.dvQuestionWorkOnBirth.nativeElement.style.display = "none";

      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvQuestionInsurances.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.WorkOnBirth = "Sim";
    }
    else {
      this.qualifiedForm.WorkOnBirth = "Não";
      this.router.navigate(['/obrigado']);
    }
  }
  answerWorkInsurances(response) {
    this.ngOnDestroy();
    if (response == 1) {
      this.dvQuestionInsurances.nativeElement.style.display = "none";

      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvCongratulation.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.SegJobReceive = "Sim";
    }
    else {
      this.dvQuestionInsurances.nativeElement.style.display = "none";

      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvCongratulation.nativeElement.style.display = "block";
      }, 2000);
      this.qualifiedForm.SegJobReceive = "Não";
    }
  }
  sendToComercial() {
    Swal.fire({
      title: "Quase lá...",
      html: '<span style="font-size: 24px">Insira seu <strong>nome completo</strong> e clique no botão abaixo para entrar em contato com nossa equipe pelo WhatsApp! <img src="../../assets/img/whatsapp-logo-5.png"><span>',
      input: 'text',
      inputPlaceholder: 'Nome Completo',
      confirmButtonText: 'QUERO O BENEFÍCIO!',
      focusConfirm: true,
      showCancelButton: false,
      showLoaderOnConfirm: true,
      inputValidator: (value) => {
        if (!value) {
          return 'Por favor insira seu nome!'
        }
        else {
          this.qualifiedForm.Name = value;
          this.text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | `
          if (this.qualifiedForm.PrevSituation == "Trabalhei Registrada") {
            this.jobText = `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
              `%20Seguro%20Desemprego%3A%20${this.qualifiedForm.SegJobReceive} | `
          }
          else {
            this.jobText = `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PaidTen} | `
          }
          let timerInterval;
          this.formService.insertFormBorn(this.qualifiedForm).subscribe();
          Swal.fire({
            title: 'Obrigado por suas respostas!',
            html:
              'Você será redirecionada ao WhatsApp em <strong></strong> segundos.<br/><br/>',
            timer: 3000,
            willOpen: () => {
              const content = Swal.getContent()
              const $ = content.querySelector.bind(content)

              Swal.showLoading()

              timerInterval = setInterval(() => {
                Swal.getContent().querySelector('strong')
                  .textContent = (Swal.getTimerLeft() / 1000)
                    .toFixed(0)
              }, 100)
            },
            willClose: () => {
              window.location.href = "https://api.whatsapp.com/send?phone=5512991228415&text=" + this.text + this.jobText;
            }
          });
        }
      }
    });
  }
  ngOnDestroy() {
    if (this.autoSaveInterval) {
      clearInterval(this.autoSaveInterval);
    }
  }
  countAccessYes() {
    this.formService.accessCount("Sim", this.ipAddress).subscribe();
  }
  countAccessNo() {
    this.formService.accessCount("Não", this.ipAddress).subscribe();
  }

}
