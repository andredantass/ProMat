import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../services/form.service';
import { QualifiedQueue } from '../qualification-form/model/qualifiedqueue';
import { Router } from '@angular/router';
import { compareAsc, format } from 'date-fns';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-qualification-form-noborn',
  templateUrl: './qualification-form-noborn.component.html',
  styleUrls: ['./qualification-form-noborn.component.css']
})

export class QualificationFormNobornComponent implements OnInit {
  qualifiedForm: QualifiedQueue = new QualifiedQueue();
  registerForm: FormGroup;
  submitted = false;
  workRegistered: boolean = false;
  WorkRegisteredBeforeBorn: boolean = false;
  meiPagou: boolean = false;
  notWorked: boolean = false;
  setDefault: boolean = false;
  IsWait: boolean = true;
  childLessFive: boolean = false;
  today = format(new Date(), 'yyyy-MM-dd');
  datemin: any = new Date();
  datemax = format(new Date(), 'yyyy-MM-dd');
  dateminWill = format(new Date(), 'yyyy-MM-dd');
  datemaxWill: any = new Date();
  comparaMaxWill: boolean = true;
  comparaMinWill: boolean = true;
  comparaMax: boolean = true;
  comparaMin: boolean = true;
  comparaMaxJob: boolean = true;
  dateWillBornError: boolean = false;
  dateBornError: boolean = false;
  dateJobError: boolean = false;

  constructor(private formBuilder: FormBuilder, private formService: FormService, private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      situation: ['', Validators.required],
      prevSituation: [''],
      dateWillBorn: ['', [Validators.required, Validators.minLength(4)]],
      dateBorn: [''],
      dateJobEnd: [''],
      segJobReceive: [''],
      paidTen: ['']
    });
    this.datemin.setFullYear(this.datemin.getFullYear() - 5);
    this.datemin = format(this.datemin, 'yyyy-MM-dd');
    this.datemaxWill.setMonth(this.datemaxWill.getMonth() + 9);
    this.datemaxWill = format(this.datemaxWill, 'yyyy-MM-dd');
  }
  get f() { return this.registerForm.controls; }

  validateDateBorn() {
    this.comparaMax = this.registerForm.get('dateBorn').value <= this.datemax;
    this.comparaMin = this.registerForm.get('dateBorn').value >= this.datemin;
  }
  validateDateWillBorn() {
    this.comparaMaxWill = this.registerForm.get('dateWillBorn').value <= this.datemaxWill;
    this.comparaMinWill = this.registerForm.get('dateWillBorn').value >= this.dateminWill;
  }
  validateDateJob() {
    if (this.registerForm.get('dateJobEnd').value <= this.today && this.registerForm.get('dateJobEnd').value != '') {
      this.comparaMaxJob = true;
    }
    else
      this.comparaMaxJob = false;
  }
  validateJob() {
    if (this.comparaMaxJob == false) {
      this.dateJobError = true
    }
    else
      this.dateJobError = false
  }
  validateBorn() {
    if (this.comparaMax == false || this.comparaMin == false) {
      this.dateBornError = true
    }
    else
      this.dateBornError = false
  }
  validateWillBorn() {
    if (this.comparaMaxWill == false || this.comparaMinWill == false) {
      this.dateWillBornError = true
    }
    else
      this.dateWillBornError = false
  }

  ddlHasYouWorkedSelectedChangeHandler(event: any) {
    if (event != undefined) {
      let selectedOptions = event.target["options"];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "Trabalhei Registrada" && selectElementValue != "") {
        this.meiPagou = false;
        this.notWorked = false;
        this.WorkRegisteredBeforeBorn = true;
        this.registerForm.get('dateJobEnd').setValidators(Validators.compose([Validators.required, Validators.minLength(4)])); // 5.Set Required Validator
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('segJobReceive').setValidators([Validators.required]);
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('paidTen').clearValidators();
        this.registerForm.get('paidTen').updateValueAndValidity();
        this.registerForm.get('dateWillBorn').setValidators(Validators.compose([Validators.required, Validators.minLength(4)]));
        this.registerForm.get('dateWillBorn').updateValueAndValidity();
      }
      if (selectElementValue == "Sou MEI" || selectElementValue == "Paguei por conta") {
        this.WorkRegisteredBeforeBorn = false;
        this.notWorked = false;
        this.meiPagou = true;
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('dateWillBorn').setValidators([Validators.required]);
        this.registerForm.get('dateWillBorn').updateValueAndValidity();
      }
      if (selectElementValue == "Não trabalhei registrada") {
        this.notWorked = true;
        this.meiPagou = false;
        this.WorkRegisteredBeforeBorn = false;
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('paidTen').clearValidators();
        this.registerForm.get('paidTen').updateValueAndValidity();
        this.registerForm.get('dateWillBorn').setValidators(Validators.compose([Validators.required, Validators.minLength(4)]));
        this.registerForm.get('dateWillBorn').updateValueAndValidity();
      }
    }
  }
  ddlYouAreSelectedChangeHandler(event: any) {

    if (event != undefined) {
      let selectedOptions = event.target['options'];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "Grávida filho menor de 5") {
        this.workRegistered = true;
        this.childLessFive = true;
        this.registerForm.get('dateBorn').setValidators(Validators.compose([Validators.required, Validators.minLength(4)]));
        this.registerForm.get('dateBorn').updateValueAndValidity();
        this.registerForm.get('dateWillBorn').setValidators([Validators.required]);
        this.registerForm.get('dateWillBorn').updateValueAndValidity();
        this.registerForm.get('prevSituation').setValidators([Validators.required]); // 5.Set Required Validator
        this.registerForm.get('prevSituation').updateValueAndValidity();
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
      }
      else {
        this.childLessFive = false;
        this.workRegistered = true;
        this.registerForm.get('prevSituation').setValidators([Validators.required]); // 5.Set Required Validator
        this.registerForm.get('prevSituation').updateValueAndValidity();
        this.registerForm.get('dateWillBorn').setValidators([Validators.required]);
        this.registerForm.get('dateWillBorn').updateValueAndValidity();
      }
      this.setDefault = true;
      this.setSelectedIndex(document.getElementById("ddlYouWorked"), "");
      this.WorkRegisteredBeforeBorn = false;
    }

  }

  setSelectedIndex(s, valsearch) {
    for (var _i = 0; _i < s.options.length; _i++) {
      if (s.options[_i].value == valsearch) {
        s.options[_i].selected = true;
        break;
      }
      return;
    }
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.registerForm.invalid) {
      Swal.fire('Aviso!', 'Todos os campos são obrigatórios!', 'error');
      return;
    }
    this.IsWait = false;
    this.qualifiedForm.Name = this.registerForm.get('firstName').value;
    this.qualifiedForm.Phone = this.registerForm.get('phone').value.toString();
    this.qualifiedForm.Email = this.registerForm.get('email').value;
    this.qualifiedForm.Situation = this.registerForm.get('situation').value;
    this.qualifiedForm.PrevSituation = this.registerForm.get('prevSituation').value;
    this.qualifiedForm.DateWillBorn = this.registerForm.get('dateWillBorn').value.replace(/^(\d{2})(\d{2}).*/, '$1/$2');
    this.qualifiedForm.DateBorn = this.registerForm.get('dateBorn').value.replace(/^(\d{2})(\d{2}).*/, '$1/$2');
    this.qualifiedForm.PaidTen = this.registerForm.get('paidTen').value;
    if (this.registerForm.get('dateJobEnd').value != '') {
      this.qualifiedForm.DateJobEnd = this.registerForm.get('dateJobEnd').value.replace(/^(\d{2})(\d{2}).*/, '$1/$2');
    }

    this.qualifiedForm.SegJobReceive = this.registerForm.get('segJobReceive').value;

    this.formService.insertFormNoBorn(this.qualifiedForm).subscribe(
      response => {
        let timerInterval;
        let text: string;
        if (this.meiPagou) {
          text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Email%3A%20${this.qualifiedForm.Email} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | ` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
            `%20Data%20Prevista%20Nascimento%3A%20${this.qualifiedForm.DateWillBorn} | `
          if (this.childLessFive) {
            text += `%20Data%20Nascimento%20Mais%20Velho%3A%20${this.qualifiedForm.DateBorn} | `
          }

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
              window.location.href = "https://api.whatsapp.com/send?phone=5512991228415&text=" + text;
            }
          });
        }
        if (this.WorkRegisteredBeforeBorn) {
          text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Email%3A%20${this.qualifiedForm.Email} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | ` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
            `%20Data%20Saída%20Trabalho%3A%20${this.qualifiedForm.DateJobEnd} | ` +
            `%20Seguro%20Desemprego%3A%20${this.qualifiedForm.SegJobReceive} | ` +
            `%20Data%20Prevista%20Nascimento%3A%20${this.qualifiedForm.DateWillBorn} | `
          if (this.childLessFive) {
            text += `%20Data%20Nascimento%20Mais%20Velho%3A%20${this.qualifiedForm.DateBorn} | `
          }
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
              window.location.href = "https://api.whatsapp.com/send?phone=5512991228415&text=" + text;
            }
          });
        }
        if (this.notWorked)
          this.router.navigate(['/obrigado']);

        this.IsWait = true;
        this.onReset();
      },
      error => {
        console.log(error);
        let timerInterval;
        let text: string;
        if (this.meiPagou) {
          text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Email%3A%20${this.qualifiedForm.Email} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | ` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
            `%20Data%20Prevista%20Nascimento%3A%20${this.qualifiedForm.DateWillBorn} | `
          if (this.childLessFive) {
            text += `%20Data%20Nascimento%20Mais%20Velho%3A%20${this.qualifiedForm.DateBorn} | `
          }

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
              window.location.href = "https://api.whatsapp.com/send?phone=5512991228415&text=" + text;
            }
          });
        }
        if (this.WorkRegisteredBeforeBorn) {
          text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Email%3A%20${this.qualifiedForm.Email} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | ` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
            `%20Data%20Saída%20Trabalho%3A%20${this.qualifiedForm.DateJobEnd} | ` +
            `%20Seguro%20Desemprego%3A%20${this.qualifiedForm.SegJobReceive} | ` +
            `%20Data%20Prevista%20Nascimento%3A%20${this.qualifiedForm.DateWillBorn} | `
          if (this.childLessFive) {
            text += `%20Data%20Nascimento%20Mais%20Velho%3A%20${this.qualifiedForm.DateBorn} | `
          }
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
              window.location.href = "https://api.whatsapp.com/send?phone=5512991228415&text=" + text;
            }
          });
        }
        if (this.notWorked)
          this.router.navigate(['/obrigado']);

        this.IsWait = true;
        this.onReset();
      }

    );



    // display form values on success
    return;
  }

  onReset() {
    this.workRegistered = false;
    this.WorkRegisteredBeforeBorn = false;
    this.meiPagou = false;
    this.setDefault = false;
    this.IsWait = true;
    this.childLessFive = false;
    this.comparaMaxWill = true;
    this.comparaMinWill = true;
    this.comparaMax = true;
    this.comparaMin = true;
    this.comparaMaxJob = true;
    this.dateWillBornError = false;
    this.dateBornError = false;
    this.dateJobError = false;
    this.submitted = false;
    this.registerForm.reset();
  }
}
