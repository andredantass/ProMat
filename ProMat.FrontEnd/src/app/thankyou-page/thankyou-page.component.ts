import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../services/form.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { QualificationFormBornComponent } from '../qualification-form-born/qualification-form-born.component';

@Component({
  selector: 'app-thankyou-page',
  templateUrl: './thankyou-page.component.html',
  styleUrls: ['./thankyou-page.component.css']
})
export class ThankyouPageComponent implements OnInit {
  registerForm: FormGroup;
  submitted: boolean = false;

  constructor(private formBuilder: FormBuilder, private formService: FormService, private router: Router) { }

  ngOnInit(): void {
      this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      phone: ['', Validators.required]
      });
  }

  onSubmit() {
        this.submitted = true;
        if (this.registerForm.invalid) {
          return;
        }
        let timerInterval;
        let text: string;

        text = "Olá%2C%20respondi%20o%20formulário%20indicação.%20Esses%20são%20os%20dados%3A%20" +
               `Nome: ${this.registerForm.get('firstName').value.toString()} | ` +
               `WhatsApp: ${this.registerForm.get('phone').value.toString()}`
        
        Swal.fire({
          title: 'Obrigado por sua indicação!',
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
}
