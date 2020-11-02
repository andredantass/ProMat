import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { QualifiedQueue } from '../qualification-form/model/qualifiedqueue';

@Injectable()

export class FormService {
  api = environment.apiURL;
  return;
constructor(private http: HttpClient) {
}

validateForm(registerForm: QualifiedQueue) {
  alert('passou');
  return this.http.post<QualifiedQueue>(`${this.api}/Form/ValidateForm`, registerForm);
}
getTeste()
{
  alert(`${this.api}/Form/Authenticated`);

  this.http.get(`${this.api}/Form/Authenticated`).subscribe(response => {
    this.return = response;
  }, error => {
    console.log(error);
  });
}
}