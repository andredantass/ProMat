import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsComponent } from './forms/forms.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QualificationFormComponent } from './qualification-form/qualification-form.component';
import { QuestionFormComponent } from './question-form/question-form.component';
import { QualificationFormNobornComponent } from './qualification-form-noborn/qualification-form-noborn.component';
import { QualificationFormBornComponent } from './qualification-form-born/qualification-form-born.component';
import { FormService } from './services/form.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [FormService],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
