import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { category } from '../_model/category';
import { Observable } from 'rxjs';
import { customer } from '../_model/customer';

@Injectable({
    providedIn: 'root'
})

export class CustomerService{
    private endPoint = 'https://localhost:7235/api/Customer';
    private httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
      constructor(private http: HttpClient) { }

      // GET all
      getAll() : Observable<customer[]> {
        return this.http.get<customer[]>(this.endPoint, this.httpOptions);
      }
    
    
      getById(id : number) : Observable<customer> {
        console.log("hello from getbyid " + `${this.endPoint}/${id}`);
        
        return this.http.get<customer>(`${this.endPoint}/${id}`, this.httpOptions)
      }
    
      create(Customer : any) : Observable<customer> {
        return this.http.post(`${this.endPoint}`, Customer, this.httpOptions)
      }
    
        
      update(id : number, Customer : any) : Observable<customer> {
        return this.http.put(`${this.endPoint}/${id}`, Customer, this.httpOptions)
      }
      
    
      delete(id : number) : Observable<any> {
        return this.http.delete(`${this.endPoint}/${id}`, this.httpOptions)
      }
}