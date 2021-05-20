import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsComponent } from './forms/forms.component';
import { QuestionFormComponent } from './question-form/question-form.component';
import { QualificationFormComponent } from './qualification-form/qualification-form.component';
import { QualificationFormNobornComponent } from './qualification-form-noborn/qualification-form-noborn.component';
import { QualificationFormBornComponent } from './qualification-form-born/qualification-form-born.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ThankyouPageComponent } from './thankyou-page/thankyou-page.component';
import { FacebookFormsComponent } from './forms/facebookForms.component';
import { InstagramFormsComponent } from './forms/instagramForms.component';

const routes: Routes = [
  { path: '', component: FormsComponent },
  { path: 'facebook', component: FacebookFormsComponent },
  { path: 'instagram', component: InstagramFormsComponent},
  { path: 'question', component: QuestionFormComponent },
  { path: 'thankyou', component: DashboardComponent },
  { path: 'qualificationnoborn/:source', component: QualificationFormNobornComponent },
  { path: 'qualificationborn', component: QualificationFormBornComponent },
  { path: 'qualification', component: QualificationFormComponent },
  { path: 'obrigado', component: ThankyouPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
