import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsComponent } from './forms/forms.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QualificationFormComponent } from './qualification-form/qualification-form.component';
import { QuestionFormComponent } from './question-form/question-form.component';
import { QualificationFormNobornComponent } from './qualification-form-noborn/qualification-form-noborn.component';
import { QualificationFormBornComponent } from './qualification-form-born/qualification-form-born.component';


@NgModule({
  declarations: [
    AppComponent,
    FormsComponent,
    DashboardComponent,
    QualificationFormComponent,
    QuestionFormComponent,
    QualificationFormNobornComponent,
    QualificationFormBornComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
