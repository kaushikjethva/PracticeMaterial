import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
private readonly APIURL:string="http://localhost:58145/api/Identity";
  
  public getToken(login:any):Observable<string> {
    return this.http.post<string>(`${this.APIURL}/token`,login,{
      headers:{
        "Content-type":"application/json"
        // "Accept"        : "application/json"
      }
    })
    
  }
}
