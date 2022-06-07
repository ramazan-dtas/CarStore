import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { order } from '../_model/order';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private endPoint = 'https://localhost:7235/api/Order';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }

  getAll(): Observable<order[]> {
    return this.http.get<order[]>(this.endPoint, this.httpOptions);
  }
}