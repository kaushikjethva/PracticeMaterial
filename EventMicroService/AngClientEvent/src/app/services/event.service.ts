import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EventData } from '../models/event-data';

@Injectable({
  providedIn: 'root'
})
export class EventService {
private readonly APIURL:string="http://localhost:61227/api/events";

  constructor(private http:HttpClient) { }

  public getEvents():Observable<EventData[]>{
    
    let authToken = localStorage.getItem("auth-token") || undefined;
    // if(authToken){
    //   let options={
    //     headers:{
    //       "Authorization":`Bearer ${authToken}`,
    //       "Accept"        : "application/json"
    //     }
    //   }
      return this.http.get<EventData[]>(this.APIURL);
      // debugger
      // console.log(data);
      // return data;
    // }    
    // else{
    //   return undefined;
    // }
  }
}
