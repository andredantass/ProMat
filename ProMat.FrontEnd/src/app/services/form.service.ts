import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { QualifiedQueue } from '../qualification-form/model/qualifiedqueue';
import { FormsComponent } from '../forms/forms.component'

@Injectable()

export class FormService {
  api = environment.apiURL;
  return;
constructor(private http: HttpClient) {
}
accessCount(path: string, ip: string, source: string){
  return this.http.get(`${this.api}/Form/AccessCount/${path}/${ip}/${source}`);
}
validateBornForm(registerForm: QualifiedQueue) {
  return this.http.post<QualifiedQueue>(`${this.api}/Form/ValidateFormBorn`, registerForm);
}
validateNoBornForm(registerForm: QualifiedQueue)
{
  return this.http.post<QualifiedQueue>(`${this.api}/Form/ValidateFormNoBorn`, registerForm);
}
insertFormBorn(registerForm: QualifiedQueue) {
  return this.http.post<QualifiedQueue>(`${this.api}/Form/InsertFormBorn`, registerForm);
}
insertFormNoBorn(registerForm: QualifiedQueue) {
  return this.http.post<QualifiedQueue>(`${this.api}/Form/InsertFormNoBorn`, registerForm);
}
public getIPAddress(){  
  return this.http.get("https://api.ipify.org/?format=json");  
}
/* getLocation(ip){
  return this.http.get("https://geo.ipify.org/api/v1?apiKey=at_Ry0Ft5Y66oWdwAbmwfGmoXmOCdvJH&ipAddress=" + ip);
}   */

getTeste()
{
  this.http.get(`${this.api}/Form/Authenticated`).subscribe(response => {
    this.return = response;
  }, error => {
    console.log(error);
  });
}
}