import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { user } from '../_model/user';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class UserService {
  private endPoint = 'https://localhost:7235/api/User';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  // GET all users
  getAll(): Observable<user[]> {
    return this.http.get<user[]>(this.endPoint, this.httpOptions);
  }

  // GET user by ID
  getById(id : number) : Observable<user> {
    let x = this.http.get<user>(`${this.endPoint}/${id}`, this.httpOptions)
    console.log(x)

    return this.http.get<user>(`${this.endPoint}/${id}`, this.httpOptions)
  }

  create(user : user) : Observable<user> {

    let x = {email: user.email, password: user.password}
    return this.http.post(`${this.endPoint}`, x, this.httpOptions)
  }


  update(id : number, user : user) : Observable<user> {

    let x = {
      email: user.email, 
      password: user.password,
      role: user.role
    }

    return this.http.put(`${this.endPoint}/${id}`, x, this.httpOptions)
  }

  delete(id : number) : Observable<any> {
    return this.http.delete(`${this.endPoint}/${id}`, this.httpOptions)
  }

}