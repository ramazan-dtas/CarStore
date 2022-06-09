import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { category } from '../_model/category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = "https://localhost:7235/api/Category";
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'})};
  constructor(private http:HttpClient) { }

  getAllCategory(): Observable<category[]>{
    return this.http.get<category[]>(this.apiUrl);
  }
  createCategory(category: category): Observable<category>{
    return this.http.post<category>(this.apiUrl, category, this.httpOptions);
  }
  updateCategory(categoryId: number, category:category):Observable<category>{
    return this.http.put<category>(`${this.apiUrl}/${categoryId}`, category, this.httpOptions);
  }  
  deleteCategory(categoryId:number): Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}/${categoryId}`, this.httpOptions)
  }
}