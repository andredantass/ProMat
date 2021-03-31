import { Component, OnInit, AfterViewChecked, AfterViewInit } from '@angular/core';

declare var startInitial: Function;

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    startInitial();
  }
  ngAfterViewInit() {

    // contentChild is set after the content has been initialized

  }

  ngAfterViewChecked() {

  }
}
