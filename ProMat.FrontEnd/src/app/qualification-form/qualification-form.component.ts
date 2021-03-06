import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../services/form.service';
import { QualifiedQueue } from '../qualification-form/model/qualifiedqueue';

@Component({
  selector: 'app-qualification-form',
  templateUrl: './qualification-form.component.html',
  styleUrls: ['./qualification-form.component.scss']
})
export class QualificationFormComponent implements OnInit {
  qualifiedForm: QualifiedQueue = new QualifiedQueue();
  registerForm: FormGroup;
  submitted = false;
  workRegistered: boolean = false;
  WorkRegisteredBeforeBorn: boolean = false;
  setDefault: boolean = false;

  constructor(private formBuilder: FormBuilder, private formService: FormService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      phone: ['', Validators.required],
      situation: ['', Validators.required],
      prevSituation: [''],
      dateBorn: [''],
      dateJobEnd: [''],
      segJobReceive: ['']
    });
  }
  get f() { return this.registerForm.controls; }

  ddlHasYouWorkedSelectedChangeHandler(event: any) {
    if (event != undefined) {
      let selectedOptions = event.target["options"];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "WorkRegisteredBeforeBorn" && selectElementValue != "") {
        this.WorkRegisteredBeforeBorn = true;
        this.registerForm.get('dateJobEnd').setValidators([Validators.required]); // 5.Set Required Validator
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
        this.registerForm.get('segJobReceive').setValidators([Validators.required]);
        this.registerForm.get('segJobReceive').updateValueAndValidity();
      }
      else {
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();

        this.WorkRegisteredBeforeBorn = false;
      }
    }
  }
  ddlYouAreSelectedChangeHandler(event: any) {

    if (event != undefined) {
      let selectedOptions = event.target['options'];
      let selectedIndex = selectedOptions.selectedIndex;
      let selectElementValue = selectedOptions[selectedIndex].value;

      if (selectElementValue == "MotherChildMoreFiveYears" || selectElementValue == "") {
        this.workRegistered = false;
        this.registerForm.get('prevSituation').clearValidators();
        this.registerForm.get('dateBorn').clearValidators();
        this.registerForm.get('prevSituation').updateValueAndValidity();
        this.registerForm.get('dateBorn').updateValueAndValidity();
        this.registerForm.get('segJobReceive').clearValidators();
        this.registerForm.get('dateJobEnd').clearValidators();
        this.registerForm.get('segJobReceive').updateValueAndValidity();
        this.registerForm.get('dateJobEnd').updateValueAndValidity();
      }
      else {
        this.workRegistered = true;

        this.registerForm.get('prevSituation').setValidators([Validators.required]); // 5.Set Required Validator
        this.registerForm.get('prevSituation').updateValueAndValidity();
        this.registerForm.get('dateBorn').setValidators([Validators.required]);
        this.registerForm.get('dateBorn').updateValueAndValidity();
      }
      this.setDefault = true;
      this.setSelectedIndex(document.getElementById("ddlYouWorked"),"");
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
      return;
    }

    this.qualifiedForm.FirstName = this.registerForm.get('firstName').value;
    this.qualifiedForm.Phone = this.registerForm.get('phone').value.tostring();
    this.qualifiedForm.Situation = this.registerForm.get('situation').value;
    this.qualifiedForm.PrevSituation = this.registerForm.get('prevSituation').value;
    this.qualifiedForm.DateBorn = this.registerForm.get('dateBorn').value;
    this.qualifiedForm.DateJobEnd = this.registerForm.get('dateJobEnd').value;

    this.formService.validateBornForm(this.qualifiedForm).subscribe(
      response => {
        //Swal.fire('Success', 'Password was changed successfully.', 'success');

      },
      error => {
        console.log(error);
        //Swal.fire('Error to change Password', error, 'error');
      }
    );



    // display form values on success
    return;
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }
}
