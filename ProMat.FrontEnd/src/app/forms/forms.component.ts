import { Component,ElementRef, OnInit,AfterViewChecked, AfterViewInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormService } from '../services/form.service';

declare var startInitial: Function;
@Component({
  selector: 'app-forms',
  templateUrl: './forms.component.html',
  styleUrls: ['./forms.component.scss']
})
export class FormsComponent implements OnInit,  AfterViewChecked, AfterViewInit {
  ipAddress: string;
  location: string;
  autoSaveInterval;
  @ViewChild('devQuestionChildWasBorn', { static: true }) dvQChildWasBorn: ElementRef;
  @ViewChild('dvQuestionChildLess5Years',{ static:true }) dvQuestionChildLess5Years : ElementRef;
  @ViewChild('dvQuestionWorkBeforeBorn', {static: true}) dvQuestionWorkBeforeBorn :ElementRef; 
  @ViewChild('dvQuestionInsurances', {static: true}) dvQuestionInsurances :ElementRef; 
  @ViewChild('dvCongratulation', {static: true}) dvCongratulation :ElementRef; 
  @ViewChild('checkMarkAnswer',{static:true}) checkMarkAnswer :ElementRef;

  
  
  constructor(private router: Router, private formService: FormService) { }

  RedirectQualification(status)
  {
    this.router.navigate(['/qualification']);
  }


  	/* setTimeout(() => {
		document.getElementById('devQuestionChildWasBorn').style.display = 'block';
	}, tempo) */

  ngOnInit() {

    startInitial();
    this.getIP();
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
  getIP()  
  {  
    this.formService.getIPAddress().subscribe((response)=>{ 
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
  answerChildWasBorn()
  {
    this.dvQChildWasBorn.nativeElement.style.display = 'none';
    
    this.checkMarkAnswer.nativeElement.style.display = 'block';
    this.autoSaveInterval = setInterval(() => {
      this.checkMarkAnswer.nativeElement.style.display = 'none';
      this.dvQuestionChildLess5Years.nativeElement.style.display = "block";
    }, 2000);

  }

  answerChildLess5Years()
  {
    this.ngOnDestroy();
    this.dvQuestionChildLess5Years.nativeElement.style.display = "none";

      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvQuestionWorkBeforeBorn.nativeElement.style.display = "block";
        
      }, 2000);

  }
  answerWorkBeforeBorn()
  {
    this.ngOnDestroy();

    this.dvQuestionWorkBeforeBorn.nativeElement.style.display = "none";

    this.checkMarkAnswer.nativeElement.style.display = 'block';
    this.autoSaveInterval = setInterval(() => {
      this.checkMarkAnswer.nativeElement.style.display = 'none';
      this.dvQuestionInsurances.nativeElement.style.display = "block";
      
    }, 2000);

  }
  answerWorkInsurances(response)
  {
    this.ngOnDestroy();
    if(response)
    {
      this.dvQuestionInsurances.nativeElement.style.display = "none";

      this.checkMarkAnswer.nativeElement.style.display = 'block';
      this.autoSaveInterval = setInterval(() => {
        this.checkMarkAnswer.nativeElement.style.display = 'none';
        this.dvCongratulation.nativeElement.style.display = "block";
        
      }, 2000);
    }
    else
    {}
  }
  sendToComercial()
  {

  }
  ngOnDestroy() {
    if (this.autoSaveInterval) {
      clearInterval(this.autoSaveInterval);
    }
  }
  countAccessYes()
  {
    this.dvQChildWasBorn.nativeElement.style.display = 'none';
    this.dvQuestionChildLess5Years.nativeElement.style.display = "block";
    // this.formService.accessCount("Sim", this.ipAddress).subscribe();
  }
  countAccessNo()
  {
    this.formService.accessCount("Não", this.ipAddress).subscribe();
  }

}
