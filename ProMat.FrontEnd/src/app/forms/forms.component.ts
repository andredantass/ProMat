import { Component, OnInit,AfterViewChecked, AfterViewInit } from '@angular/core';
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
  constructor(private router: Router, private formService: FormService) { }

  RedirectQualification(status)
  {
    this.router.navigate(['/qualification']);
  }
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
  countAccessYes()
  {
    this.formService.accessCount("Sim", this.ipAddress).subscribe();
  }
  countAccessNo()
  {
    this.formService.accessCount("Não", this.ipAddress).subscribe();
  }

}
