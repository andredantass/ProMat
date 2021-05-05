import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../services/form.service';
import { QualifiedQueue } from '../qualification-form/model/qualifiedqueue';
import { Router } from '@angular/router';
import { compareAsc, format } from 'date-fns';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-qualification-form-born',
  templateUrl: './qualification-form-born.component.html',
  styleUrls: ['./qualification-form-born.component.css']
})
export class QualificationFormBornComponent implements OnInit {
  qualifiedForm: QualifiedQueue = new QualifiedQueue();
  registerForm: FormGroup;
  submitted = false;
  meiPagou: boolean = false;
  workRegistered: boolean = false;
  WorkRegisteredBeforeBorn: boolean = false;
  disqualified: boolean = false;
  notWorked: boolean = false;
  setDefault: boolean = false;
  IsWait: boolean = true;
  workOnBirth: boolean = false;
  today = format(new Date(), 'yyyy-MM-dd');
  datemin: any = new Date();
  datemax = format(new Date(), 'yyyy-MM-dd');
  comparaMax: boolean = true;
  comparaMin: boolean = true;
  comparaMaxJob: boolean = true;
  dateBornError: boolean = false;
  dateJobError: boolean = false;
  cssValidate;
  constructor(private formBuilder: FormBuilder, private formService: FormService, private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      situation: ['', Validators.required],
      prevSituation: [''],
      dateBorn: [''],
      dateWillBorn: [''],
      dateJobEnd: [''],
      segJobReceive: [''],
      paidTen: ['']
    });
    this.datemin.setFullYear(this.datemin.getFullYear() - 5)
    this.datemin = format(this.datemin, 'yyyy-MM-dd');
  }
  get f() { return this.registerForm.controls; }

  ddlHasYouWorkedSelectedChangeHandler(event: any) {
    if (event != undefined) {
      let selectedOptions = event.target["options"];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;
      if (selectElementValue == "Não Trabalhei Registrada") {
        this.notWorked = true;
        this.WorkRegisteredBeforeBorn = false;
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
      }
      if (selectElementValue == "Trabalhei Registrada") {
        this.meiPagou = false;
        this.notWorked = false;
        this.WorkRegisteredBeforeBorn = true;
        this.registerForm.get('dateJobEnd').setValidators(Validators.compose([Validators.required, Validators.minLength(4)])); // 5.Set Required Validator
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('segJobReceive').setValidators([Validators.required]);
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('paidTen').clearValidators();
        this.registerForm.get('paidTen').updateValueAndValidity();
      }
      if (selectElementValue == "Sou MEI" || selectElementValue == "Paguei por conta") {
        this.WorkRegisteredBeforeBorn = false;
        this.notWorked = false;
        this.meiPagou = true;
        this.registerForm.get('paidTen').setValidators([Validators.required]);
        this.registerForm.get('paidTen').updateValueAndValidity();
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
      }
    }
  }
  validateDateBorn() {
    this.comparaMax = this.registerForm.get('dateBorn').value <= this.datemax;
    this.comparaMin = this.registerForm.get('dateBorn').value >= this.datemin;
  }
  validateDateJob() {
    if (this.registerForm.get('dateJobEnd').value <= this.today && this.registerForm.get('dateJobEnd').value != '') {
      this.comparaMaxJob = true;
    }
    else
      this.comparaMaxJob = false;
  }
  validateBorn() {
    if (this.comparaMax == false || this.comparaMin == false) {
      this.dateBornError = true
    }
    else
      this.dateBornError = false
  }
  validateJob() {
    if (this.comparaMaxJob == false) {
      this.dateJobError = true
    }
    else
      this.dateJobError = false
  }
  ddlPaidByOwnChangeHandler(event: any) {
    if (event != undefined) {
      let selectedOptions = event.target['options'];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "Não Contribuí") {
        this.disqualified = true;
      }
      if (selectElementValue == "Contribuí Individualmente") {
        this.meiPagou = true;
      }
    }
  }
  ddlOnBirthSituationChangeHandler(event: any) {
    if (event != undefined) {
      let selectedOptions = event.target['options'];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "trabalhando") {
        this.workOnBirth = false;
      }
      if (selectElementValue == "desempregada") {
        this.workOnBirth = true;
      }
    }
  }
  ddlYouAreSelectedChangeHandler(event: any) {

    if (event != undefined) {
      let selectedOptions = event.target['options'];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "Mãe de filho maior de 5 Anos" || selectElementValue == "") {
        this.workRegistered = false;
        this.registerForm.get('prevSituation').clearValidators();
        this.registerForm.get('dateBorn').clearValidators();
        this.registerForm.get('prevSituation').updateValueAndValidity();
        this.registerForm.get('dateBorn').updateValueAndValidity();
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('paidTen').clearValidators();
        this.registerForm.get('paidTen').updateValueAndValidity();
      }
      else {
        this.workRegistered = true;

        this.registerForm.get('prevSituation').setValidators([Validators.required]); // 5.Set Required Validator
        this.registerForm.get('prevSituation').updateValueAndValidity();
        this.registerForm.get('dateBorn').setValidators(Validators.compose([Validators.required, Validators.minLength(4)]));
        this.registerForm.get('dateBorn').updateValueAndValidity();
        this.registerForm.get('paidTen').clearValidators();
        this.registerForm.get('paidTen').updateValueAndValidity();
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
    this.qualifiedForm.Email = this.registerForm.get('email').value;
    this.qualifiedForm.Phone = this.registerForm.get('phone').value.toString();
    this.qualifiedForm.Situation = this.registerForm.get('situation').value;
    this.qualifiedForm.PrevSituation = this.registerForm.get('prevSituation').value;
    this.qualifiedForm.PaidTen = this.registerForm.get('paidTen').value;
    this.qualifiedForm.SegJobReceive = this.registerForm.get('segJobReceive').value;
    this.qualifiedForm.DateBorn = this.registerForm.get('dateBorn').value.replace(/^(\d{2})(\d{2}).*/, '$1/$2');
    this.qualifiedForm.DateWillBorn = this.registerForm.get('dateWillBorn').value.replace(/^(\d{2})(\d{2}).*/, '$1/$2');
    this.qualifiedForm.DateJobEnd = this.registerForm.get('dateJobEnd').value.replace(/^(\d{2})(\d{2}).*/, '$1/$2');

    this.formService.insertFormBorn(this.qualifiedForm).subscribe(
      response => {
        let timerInterval;
        let text: string;
        if (this.registerForm.get('situation').value == "Mãe de filho maior de 5 Anos") {
          this.router.navigate(['/obrigado']);
        }
        if (this.meiPagou) {
          text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Email%3A%20${this.qualifiedForm.Email} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | ` +
            `%20Data%20Nascimento%3A%20${this.qualifiedForm.DateBorn} |` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PaidTen} | `

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
            `%20Data%20Nascimento%3A%20${this.qualifiedForm.DateBorn} | ` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
            `%20Data%20Saída%20Trabalho%3A%20${this.qualifiedForm.DateJobEnd} |` +
            `%20Seguro%20Desemprego%3A%20${this.qualifiedForm.SegJobReceive} | `

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
        if (this.disqualified)
          this.router.navigate(['/obrigado']);

        this.IsWait = true;
        this.onReset();
      },
      error => {
        console.log(error);
        let timerInterval;
        let text: string;
        if (this.registerForm.get('situation').value == "Mãe de filho maior de 5 Anos") {
          this.router.navigate(['/obrigado']);
        }
        if (this.meiPagou) {
          text = "Olá%2C%20respondi%20o%20formulário%20e%20gostaria%20de%20saber%20se%20tenho%20direito%20ao%20benefício.%20Esses%20são%20meus%20dados%3A%20" +
            `%20Nome%3A%20${this.qualifiedForm.Name} | ` +
            `%20Email%3A%20${this.qualifiedForm.Email} | ` +
            `%20Situação%3A%20${this.qualifiedForm.Situation} | ` +
            `%20Data%20Nascimento%3A%20${this.qualifiedForm.DateBorn} |` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PaidTen} | `

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
            `%20Data%20Nascimento%3A%20${this.qualifiedForm.DateBorn} | ` +
            `%20Situação%20Trabalho%3A%20${this.qualifiedForm.PrevSituation} | ` +
            `%20Data%20Saída%20Trabalho%3A%20${this.qualifiedForm.DateJobEnd} |` +
            `%20Seguro%20Desemprego%3A%20${this.qualifiedForm.SegJobReceive} | `

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
        if (this.disqualified)
          this.router.navigate(['/obrigado']);

        this.IsWait = true;
        this.onReset();
      }

    );

    // display form values on success
    return;
  }

  onReset() {
    this.meiPagou = false;
    this.workRegistered = false;
    this.WorkRegisteredBeforeBorn = false;
    this.setDefault = false;
    this.IsWait = true;
    this.comparaMax = true;
    this.comparaMin = true;
    this.comparaMaxJob = true;
    this.dateBornError = false;
    this.dateJobError = false;
    this.submitted = false;
    this.registerForm.reset();
  }

}
