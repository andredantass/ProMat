import { Component, OnInit,AfterViewChecked, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

declare var startInitial: Function;
@Component({
  selector: 'app-forms',
  templateUrl: './forms.component.html',
  styleUrls: ['./forms.component.scss']
})
export class FormsComponent implements OnInit,  AfterViewChecked, AfterViewInit {

  constructor(private router: Router) { }

  RedirectQualification(status)
  {
    this.router.navigate(['/qualification']);
  }
  ngOnInit() {

    startInitial();
  }
  ngAfterViewInit() {

    // contentChild is set after the content has been initialized
   
  }

  ngAfterViewChecked() {

  }

}
