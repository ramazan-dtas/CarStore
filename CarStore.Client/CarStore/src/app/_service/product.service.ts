import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { product } from '../_model/product';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7235/api/Product';
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'})};
  constructor(private http:HttpClient) { }

  getProductsByCategoryId(categoryId: number): Observable<product[]> {
    return this.http.get<product[]>(`${this.apiUrl}/Products/by_category/${categoryId}`).pipe(
      catchError(this.handleError<product[]>("getProductsByCategoryId"))
    )
  }
  //Get All Products
  getAllProduct(): Observable<product[]>{
    return this.http.get<product[]>(this.apiUrl);
  }

  getProduct(productId: number): Observable<product>{
    return this.http.get<product>(`${this.apiUrl}/${productId}`);
   }

  

   // GET user by ID
  getById(id : number) : Observable<product> {
    let x = this.http.get<product>(`${this.apiUrl}/${id}`, this.httpOptions)
    console.log(x)

    return this.http.get<product>(`${this.apiUrl}/${id}`, this.httpOptions)
  }

  // POST https://localhost:5001/api/User/Create
  create(product : product) : Observable<any> {
    let x = {
      categoryId: product.id,
      productName: product.productName, 
      price: product.price,
      productionYear: product.productionYear,
      km: product.km,
      description: product.description
    }

  
    return this.http.post(`${this.apiUrl}`, x, this.httpOptions)
  }


  update(id : number, product : product) : Observable<any> {

    let x = {
      categoryId: product.id,
      productName: product.productName, 
      price: product.price,
      productionYear: product.productionYear,
      km: product.km,
      description: product.description
    }


    return this.http.put(`${this.apiUrl}/${id}`, x, this.httpOptions)
  }

  delete(id : number) : Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, this.httpOptions)
  }

  /**
    * Handle Http operation that failed.
    * Let the app continue.
    * @param operation - name of the operation that failed
    * @param result - optional value to return as the observable result
    */
 private handleError<T>(operation = 'operation', result?: T) {
  return (error: any): Observable<T> => {
    // TODO: send the error to remote logging infrastructure
    console.error(error); // log to console instead

    // TODO: better job of transforming error for user consumption
    console.log(`${operation} failed: ${error.message}`);

    // Let the app keep running by returning an empty result.
    return of(result as T);
  };
}
}