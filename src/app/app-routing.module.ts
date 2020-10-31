import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsComponent } from './forms/forms.component';
import { QuestionFormComponent } from './question-form/question-form.component';
import { QualificationFormComponent } from './qualification-form/qualification-form.component';
import { QualificationFormNobornComponent } from './qualification-form-noborn/qualification-form-noborn.component';
import { QualificationFormBornComponent } from './qualification-form-born/qualification-form-born.component';

const routes: Routes = [
  { path: 'index', component: FormsComponent },
  { path: 'question', component: QuestionFormComponent },
  { path: 'qualification', component: QualificationFormComponent },
  { path: 'qualificationnoborn', component: QualificationFormNobornComponent },
  { path: 'qualificationborn', component: QualificationFormBornComponent },
  { path: 'qualification', component: QualificationFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
